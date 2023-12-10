using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Boss03 : MonoBehaviour
{
    public float movementSpeed;
    public float offset;
    //private bool moveLeft;
    private float startPosx;
    public float initialGameSpeed = 1f;
    public float gameSpeed;
    // Start is called before the first frame update
    void Start()
    {
        gameSpeed = initialGameSpeed;
        startPosx = transform.position.x;
        //moveLeft = false;
    }

    // Update is called once per frame
    void Update()
    {

        moveBossRight();
    }

    public void moveBossRight()
    {
        transform.position += Vector3.right * gameSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("player"))
        {
            SceneManager.LoadScene("Boss03");
            //Destroy(collision.gameObject);
        }
    }
}
