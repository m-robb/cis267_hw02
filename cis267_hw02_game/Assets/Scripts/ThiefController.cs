using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class ThiefController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movementSpeed;
    private Animator animator;

    [SerializeField] EnemyHealthBars hb;
    public float maxHealth;
    public float health;

    public float attackDamage;

    private float movementHorizontal;
    private float movementVertical;
    private bool facingRight = false;

    private float moveTime = 2f;
    private float stopTime = 20f; //Start value

    private Transform target;
    private Vector2 moveDirection;
    private bool chasePlayer = false;

    public GameObject daggerToDrop;
    public Transform thiefLocation;

    void Start()
    {
        //Health Bar Stuff
        hb = GetComponentInChildren<EnemyHealthBars>();
        health = maxHealth;
        hb.updateHealthBar(health, maxHealth);

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        //Decide movementHorizontal&Vertical to start off with
        movementHorizontal = Random.Range(-1f, 1f);
        movementVertical = Random.Range(-1f, 1f);
    }

    void Update()
    {
        if (target) //If there is a player
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
        }

        //Move an animate thief
        moveThief();
        animate();
    }

    private void FixedUpdate()
    {
        if (target) //If player
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * movementSpeed; //Move towards it
        }
    }

    private void moveThief()
    {
        if (!chasePlayer) //If there is no player to chase, do my normal thief wandering movements
        {
            if (moveTime >= 0) //Wander for 2 seconds
            {
                rb.velocity = new Vector2(movementSpeed * movementHorizontal, movementSpeed * movementVertical);
                moveTime -= Time.deltaTime; //Decrement time
            }
            else
            {
                if (stopTime >= 0) //Stop for 2 seconds
                {
                    //Stop thief
                    movementHorizontal = 0;
                    movementVertical = 0;
                    rb.velocity = new Vector2(movementSpeed * movementHorizontal, movementSpeed * movementVertical);
                    stopTime -= Time.deltaTime; //Decrement time
                }
                else
                {
                    //Reset both timers
                    stopTime = 2f;
                    moveTime = 2f;
                    //Decide movementHorizontal&Vertical again
                    movementHorizontal = Random.Range(-1f, 1f);
                    movementVertical = Random.Range(-1f, 1f);
                }
            }
        }
    }

    private void animate()
    {
        if (movementHorizontal != 0 && movementVertical != 0) //If I'm moving (anything but 0 for movements)
        {
            animator.SetBool("isMoving", true); //Set the isMoving parameter in our animator to true. Starts walk animation
        }
        else //If I'm idle
        {
            animator.SetBool("isMoving", false); //Set to false. Starts idle animation
        }

        if (movementHorizontal > 0 && !facingRight) //Flip to the right if am going right/facing right
        {
            flip();
        }
        else if (movementHorizontal < 0 && facingRight) //Flip to the left if am going left/facing left
        {
            flip();
        }
    }

    private void flip()
    {
        //Get current scale
        Vector3 currentScale = gameObject.transform.localScale;
        //Opposite it (-1 becomes 1 and vice versa). This flips the sprite 
        currentScale.x *= -1;
        //Set it
        gameObject.transform.localScale = currentScale;
        //Flip boolean also
        facingRight = !facingRight;

        //Reflip Healthbar
        hb.flipHealthBar();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Player is in my trigger radius, I must be moving. Set the isMoving parameter to true
            animator.SetBool("isMoving", true);
            //Set target
            target = collision.gameObject.transform;
            //Now chase the player
            chasePlayer = true;

            if (rb.position.x < target.GetComponent<Rigidbody2D>().position.x)
            {
                //Debug.Log("Player is to my right");
                //Start moving right ->
                movementHorizontal = 1f;
                movementVertical = 0.01f; //Any number but 0 (The number doesn't actually matter)
            }
            else  //If I'm to the right of the player:
            {
                //Debug.Log("Player is to my left");
                //Start moving left <-
                movementHorizontal = -1f;
                movementVertical = 0.01f; //Any number but 0 (The number doesn't actually matter)
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) //Player exited trigger radius
        {
            chasePlayer = false; //No longer chase player. Return to usual movement/wandering
            target = null; //Set the target to null so I don't keep chasing the player anyway
            //Stop moving
            movementHorizontal = 0;
            movementVertical = 0;
            rb.velocity = new Vector2(movementSpeed * movementHorizontal, movementSpeed * movementVertical);
        }
        //Stop moving animation wise
        animator.SetBool("isMoving", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) //Just "Player" for now, but will change to the player's weapon
        {
            takeDamage(20);
        }
    }

    private void takeDamage(float damage)
    {
        health -= damage;
        hb.updateHealthBar(health, maxHealth);

        if (health <= 0)
        {
            //Drop dagger(s)
            Instantiate(daggerToDrop, thiefLocation.position, daggerToDrop.transform.rotation);

            //Die
            Destroy(this.gameObject);
        }
    }

    public float getAttackDamage()
    {
        return attackDamage;
    }
}
