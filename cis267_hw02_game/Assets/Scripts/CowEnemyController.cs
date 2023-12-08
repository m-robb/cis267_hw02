using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CowEnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movementSpeed;
    private Animator animator;

    [SerializeField] EnemyHealthBars hb;
    private Combatant combatantScript;

    public float attackDamage;
    public float physicalDamage;

    private float movementHorizontal;
    private float movementVertical;
    private bool facingRight = false;

    private float moveTime = 2f;
    private float stopTime = 20f; //Start value

    private Transform target;
    private Vector2 moveDirection;
    private bool chasePlayer = false;
    private bool doubledSpeed = false;

    public GameObject weaponToDrop;
    public Transform cowLocation;

    void Start()
    {
        combatantScript = GetComponent<Combatant>();
        //Health Bar Stuff
        hb = GetComponentInChildren<EnemyHealthBars>();
        hb.updateHealthBar(combatantScript.curHealth(), combatantScript.healthMax());

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
        dropDagger();
        hb.updateHealthBar(combatantScript.curHealth(), combatantScript.healthMax());
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
                    stopTime = 6f;
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
            animator.SetBool("isWalking", true); //Set the isWalking parameter in our animator to true. Starts walk animation
        }
        else //If I'm idle
        {
            animator.SetBool("isWalking", false); //Set to false. Starts idle animation
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
            //Player is in my trigger radius, I must be moving. Set the isRunning parameter to true
            animator.SetBool("isRunning", true);
            //Set target
            target = collision.gameObject.transform;
            //Now chase the player
            chasePlayer = true;

            if (rb.position.x < target.GetComponent<Rigidbody2D>().position.x)
            {
                Debug.Log("Player is to my right");
                //Start moving right ->
                movementHorizontal = 1f;
                movementVertical = 0.01f; //Any number but 0 (The number doesn't actually matter)
            }
            else  //If I'm to the right of the player:
            {
                Debug.Log("Player is to my left");
                //Start moving left <-
                movementHorizontal = -1f;
                movementVertical = 0.01f; //Any number but 0 (The number doesn't actually matter)
            }

            //Speed Stuff(The cow will run when it sees the player, and walk otherwise, so I'm doubling movement speed here)
            if (!doubledSpeed)
            {
                movementSpeed = movementSpeed * 2;
                doubledSpeed = true;
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

            //Stop moving animation wise
            animator.SetBool("isRunning", false);

            //Speed stuff
            movementSpeed = movementSpeed / 2;
            doubledSpeed = false;
        }
    }

    public float getAttackDamage()
    {
        return attackDamage;
    }

    public float getPhysicalDamage()
    {
        return physicalDamage;
    }

    private void dropDagger()
    {
        if (combatantScript.curHealth() <= 0)
        {
            Instantiate(weaponToDrop, cowLocation.position, weaponToDrop.transform.rotation);

            Destroy(this.gameObject);
        }
    }
}
