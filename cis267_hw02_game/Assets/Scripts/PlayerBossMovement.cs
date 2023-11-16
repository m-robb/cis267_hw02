using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBossMovement : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;

    //Movement/Jump variables
    public float movementSpeed;
    private float inputHorizontal;
    public float jumpForce;
    private bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        jump();
    }

    private void jump()
    {
        if (Input.GetAxis("Jump") > 0)
        {
            if (isGrounded)
            {
                playerRigidBody.AddForce(new Vector2(playerRigidBody.velocity.x, jumpForce));
                Debug.Log("JUMP");
                isGrounded = false;
            }
        }
    }

    private void movePlayer()
    {
        //Get horizontal and vertical input
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        //Update player's position
        playerRigidBody.velocity = new Vector2(movementSpeed * inputHorizontal, playerRigidBody.position.y);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform")) //OR ANY PIECE OF GROUND
        {
            Debug.Log("Hit platform");
            isGrounded = true;
        }
    }
}
