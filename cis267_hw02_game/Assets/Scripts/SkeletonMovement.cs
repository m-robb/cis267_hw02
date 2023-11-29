using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class SkeletonMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Vector3 directionDesired;
    private Vector3 startingPos;

    private bool canMove;
    //private bool attack;

    private float lastChange;
    public float waitTime;
    public float movementSpeed;
    public float movementDistance;
    private float directionX;
    private float directionY;
    public float nextAttackTime;
    public float attackRate;
    public float attackDamage;

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
        startingPos = transform.position;
        canMove = true;
        lastChange = -waitTime;
        attackRate = 1f;
        nextAttackTime = Time.time + attackRate;
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
        //Debug.Log("RandomMovement()");
        directionX = Random.Range(-1f,1f);
        directionY = Random.Range(-1f, 1f);
        directionDesired = new Vector3(directionX, directionY, 0).normalized;     //direction skeleton should go
        animator.SetBool("isWalking", true);
        //Debug.Log(directionDesired);
    }

    private void stopMoving()
    {
        //stop moving
        canMove = false;
        directionDesired = Vector3.zero;
        animator.SetBool("isWalking", false);
    }

    private void animate()
    {
        if(directionX != 0 && directionY != 0)
        {
            animator.SetBool("isWalking", true);
            animator.SetFloat(Horizontal, directionX);
            animator.SetFloat(Vertical, directionY);
        }
        if(!canMove)
        {
            animator.SetBool("isWalking", false);
        }
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        Vector3 facePlayer;

        if(collision.gameObject.CompareTag("Player"))
        {
            if (Vector3.Distance(collision.transform.position, transform.position) >= 1.9)
            {
                directionDesired = (collision.transform.position - transform.position).normalized;
                rb.velocity = directionDesired * movementSpeed;
                animator.SetFloat(Horizontal, directionX);
                animator.SetFloat(Vertical, -directionY);
            }
            else if (Vector3.Distance(collision.transform.position, transform.position) <= 1.8)
            {
                stopMoving();
                facePlayer = (collision.transform.position - transform.position).normalized;
                animator.SetFloat(Horizontal, directionX);
                animator.SetFloat(Vertical, -directionY);

                if (Time.time > nextAttackTime)
                {
                    animator.SetTrigger("swingSword");
                    Debug.Log("Entered Attack");
                    //attack = true;
                }
                else
                {
                    //attack = false;
                    nextAttackTime = Time.time + attackRate;
                    animator.ResetTrigger("swingSword");
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //canMove = false;

        if (collision.gameObject.CompareTag("Player")) //Just "Player" for now, but will change to the player's weapon
        {
            takeDamage(20);
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        canMove = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("SkeletonBoundary"))
        {
            directionDesired = (startingPos - transform.position).normalized;
            rb.velocity = directionDesired * movementSpeed;
            animator.SetFloat(Horizontal, -directionX);
            animator.SetFloat(Vertical, -directionY);
        }

        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Exit trigger");
            canMove = true; 
            randomMovement();
        }
    }

    private void takeDamage(float damage)
    {
        health -= damage;
        hb.updateHealthBar(health, maxHealth);

        if (health <= 0)
        {
            //Drop dagger(s)
            //Die
            Destroy(this.gameObject);
        }
    }

    public float getAttackDamage()
    {
        return attackDamage;
    }
}
