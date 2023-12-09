using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBossMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movementSpeed;
    private float inputHorizontal;
    public float jumpForce;
    private bool isGrounded = false;
    private bool facingRight = false;
    private Animator animator;
    public GameObject canvas;
    private GameObject canvasChild;
    private bool canHitBoss;
    



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        canHitBoss = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        movementHorizontal();
        swingSword();
        jump();
        
        //Debug.Log(rb.velocity);
    }

    private void movementHorizontal()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movementSpeed * inputHorizontal, rb.velocity.y);

        if (inputHorizontal != 0) 
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false); 
        }

        //Debug.Log("between");

        if (facingRight == false && inputHorizontal > 0)
        {
            Debug.Log("if statement");
            flip();

        }
        else if (facingRight == true && inputHorizontal < 0)
        {
            //Debug.Log("2nd if statement");
            flip();
        }
    }

    private void jump()
    {
        if (Input.GetAxis("Jump") > 0) //Y for controller, Space for kbm
        {
            if (isGrounded)
            {
                rb.AddForce(new Vector2(rb.velocity.x, jumpForce));
                isGrounded = false;

            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
            

            if (collision.gameObject.CompareTag("Platform"))
            {
                isGrounded = true;
            }
            else if (collision.gameObject.CompareTag("HubEntrance"))
            {
                //Go back to hub
                SceneManager.LoadScene("Hub");
                //All this does right now is load the scene. It doesn't carry player over or save any information to a static class yet
            }
            else if (collision.gameObject.CompareTag("Boss02Ground"))
            {
                //Player fell off the platforms in Boss02 (Kill player?)
            }
        
        Debug.Log("grass");
        if (collision.gameObject.CompareTag("Boss") && canHitBoss)
        {
            canHitBoss = false;
            Debug.Log("trigger");
            canvasChild = getCanvas(1);
            //Debug.Log("grass");
            canvasChild.GetComponent<playerHealthBoss>().changeHealth(1);

        }

    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            canHitBoss = true;
        }
    }
    private void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    private void swingSword()
    {
        if (Input.GetAxisRaw("Fire1") != 0)
        {
            GetComponentInChildren<Sword>().swing();
        }
    }
    public GameObject getCanvas(int c)
    {
        //Debug.Log("outsidce");
        canvasChild = canvas.transform.GetChild(c).gameObject;
        if (canvasChild != null)
        {
            //Debug.Log("canvas child");
        }
        return canvasChild;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("grass");
        if (collision.gameObject.CompareTag("Boss"))
        {

            Debug.Log("trigger");
            canvasChild = getCanvas(1);
            //Debug.Log("grass");
            canvasChild.GetComponent<playerHealthBoss>().changeHealth(5);

        }
    }


}
