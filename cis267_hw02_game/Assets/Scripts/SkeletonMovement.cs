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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPositionY = transform.position.y;
        animator = GetComponent<Animator>();
        moveDown = true;
        canMove = true;
    }

    
    void Update()
    {
        walkSkeleton();
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
                StartCoroutine(moveTimer());
            }

            if (transform.position.y >= startPositionY)
            {
                moveDown = true;
                StartCoroutine(moveTimer());
            }
        }

    }

    private void randomMovement()
    {

    }

    private void stopMoving()
    {
        //stop moving
        rb.velocity = Vector2.zero;


    }


    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (Vector3.Distance(collision.transform.position, transform.position) >= 1.25)
            {
                Vector2 desiredDirection = collision.transform.position - transform.position;
                rb.velocity = desiredDirection * movementSpeed;
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
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        canMove = true;
    }



    IEnumerator moveTimer()
    {
        //canMove = false;
        //animator.SetBool("isWalking", false);
        yield return new WaitForSeconds(5);
        //canMove = true;
        //walkSkeleton();
    }
}
