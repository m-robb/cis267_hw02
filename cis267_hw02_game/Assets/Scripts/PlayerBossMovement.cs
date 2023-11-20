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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementHorizontal();
        jump();
    }

    private void movementHorizontal()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movementSpeed * inputHorizontal, rb.velocity.y);
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
        if(collision.gameObject.CompareTag("Platform"))
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
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }
}
