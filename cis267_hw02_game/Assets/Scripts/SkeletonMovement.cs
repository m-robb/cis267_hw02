using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class SkeletonMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Vector3 directionDesired;

    private bool canMove;
    private bool attack;

    private float lastChange;
    public float waitTime;
    public float movementSpeed;
    public float movementDistance;
    private float directionX;
    private float directionY;

    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");

    //Health/HealthBar Stuff
    [SerializeField] EnemyHealthBars hb;
    private float health;
    public float maxHealth;

    // Start is called before the first frame update
    void Start()
    {   
        //Health Bar Stuff
        hb = GetComponentInChildren<EnemyHealthBars>();
        health = maxHealth;
        hb.updateHealthBar(health, maxHealth);

        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        canMove = true;
        lastChange = -waitTime;
    }

    
    void Update()
    {
        if(canMove)
        {
            rb.velocity = directionDesired * movementSpeed;
        }
        else
        {
            stopMoving();
        }
        animate();
    }

	private void FixedUpdate() 
    {
        if(canMove)
        {
            if (Time.time >= lastChange + waitTime)
            {
                lastChange = Time.time; //time since the program launched
                randomMovement();
            }
        }

	}

    private void randomMovement()
    {
        directionX = Random.Range(-1f,1f);
        directionY = Random.Range(-1f, 1f);
        directionDesired = new Vector3(directionX, directionY, 0).normalized;     //direction skeleton should go
        //Debug.Log(directionDesired);
    }

    private void stopMoving()
    {
        //stop moving
        canMove = false;
        directionDesired = Vector3.zero;
    }

    private void animate()
    {
        if(directionX != 0 && directionY != 0)
        {
            animator.SetBool("isWalking", true);
            animator.SetFloat(Horizontal, directionX);
            animator.SetFloat(Vertical, directionY);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        if(attack)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("attack", true);
        }

        //Skeleton Direction

    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        float wait = 2f;

        if(collision.gameObject.CompareTag("Player"))
        {
            if (Vector3.Distance(collision.transform.position, transform.position) >= 1.25)
            {
				directionDesired = (collision.transform.position - transform.position).normalized;
                rb.velocity = directionDesired * movementSpeed;
            }

            else
            {
                canMove = false;
                rb.velocity = Vector2.zero;
                attack = true;

                if(wait >= 0)
                {
                    attack = false;
                    wait -= Time.deltaTime;
                }
                else
                {
                    wait = 2f;
                    attack = true;
                }

            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //canMove = false;

        //Example
        //if(collision.gameObject.CompareTag("Sword")) //Sword for example
        //{
        //    //Example health deduction
        //    health = health - collision.gameObject.getDaggerDamage();
        //    //Update health bar
        //    hb.updateHealthBar(health, maxHealth);
        //}

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        canMove = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("SkeletonBoundary"))
        {
            directionDesired = (collision.transform.position - transform.position).normalized;
            rb.velocity = directionDesired * movementSpeed;
        }
    }
}
