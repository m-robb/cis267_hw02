using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHubMovement : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    public GameObject player;
    public Camera cam;
    public static PlayerHubMovement Instance;

    public GameObject gameManager;
    private GameManager gm;

    //Movement variables
    public float movementSpeed;
    private float inputHorizontal;
    private float inputVertical;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
    }

    private void movePlayer()
    {
        //Get horizontal and vertical input
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
        //Update player's position
        playerRigidBody.velocity = new Vector2(movementSpeed * inputHorizontal, movementSpeed * inputVertical);
    }
}
