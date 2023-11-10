using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHubMovement : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private CircleCollider2D playerCollider;
    public GameObject player;

    public float movementSpeed;
    private Vector2 direction;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
    }

    private void movePlayer()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        playerRigidBody.MovePosition(playerRigidBody.position + direction * movementSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("House1"))
        {
            SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName("House1"));
            SceneManager.LoadScene("House1");
        }
    }
}
