using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SkeletonMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float startPositionY;
    public float movementSpeed;
    public float movementDistance;
    private Animator animator;
    //private Vector2 curPos;
    private bool moveDown;
    private bool canMove;
    private Vector3 directionDesired;
    private float lastChange;
    public float waitTime;

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
        startPositionY = transform.position.y;

        animator = GetComponent<Animator>();
        moveDown = true;
        canMove = true;
        lastChange = -waitTime;
    }

    
    void Update()
    {
        rb.velocity = directionDesired * movementSpeed;
    }

	private void FixedUpdate() 
        {
            if(Time.time >= lastChange + waitTime) 
            {
                    lastChange = Time.time; //time since the program launched
                    randomMovement();
            }
	}


	private void walkSkeleton()
    {
        if (canMove)
        {
            if (!moveDown)
            {
                rb.transform.Translate(Vector2.up * movementSpeed * Time.deltaTime);
                animator.SetBool("isWalking", true);
                //Debug.Log("Moving Up");
            }
            else
            {
                rb.transform.Translate(Vector2.down * movementSpeed * Time.deltaTime);
                animator.SetBool("isWalking", true);
                //Debug.Log("Moving Down");
            }

            if (transform.position.y <= startPositionY - movementDistance)
            {
                moveDown = false;
            }

            if (transform.position.y >= startPositionY)
            {
                moveDown = true;
            }
        }

    }

    private void randomMovement()
    {
        float directionX = Random.Range(-1f,1f);
        float directionY = Random.Range(-1f, 1f);
        directionDesired = new Vector3(directionX, directionY, 0).normalized;     //direction skeleton should go
        Debug.Log(directionDesired);
    }

    private void stopMoving()
    {
        //stop moving
        directionDesired = Vector3.zero;


    }


    
    private void OnTriggerStay2D(Collider2D collision)
    {
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
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        canMove = false;

        //Example
        //if(collision.gameObject.CompareTag("Dagger")) //Dagger for example
        //{
            //Example health deduction
            //health = health - collision.gameObject.getDaggerDamage();
            //Update health bar
            //hb.updateHealthBar(health, maxHealth);
        //}

        //Testing health bar - WORKS
        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    health = health - 1;
        //    hb.updateHealthBar(health, maxHealth);
        //}
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        canMove = true;
    }
}
