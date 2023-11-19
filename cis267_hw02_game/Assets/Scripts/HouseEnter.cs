using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class HouseEnter : MonoBehaviour
{
    private GameObject player;
    private bool enterAllowed;
    public string sceneToLoad;
    //spawn location of player
    public Vector2 spawnPlayer;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enterAllowed == true && Input.GetAxis("Jump") > 0)
        {
            DontDestroyOnLoad(player);
            SceneManager.LoadScene(sceneToLoad);
            player.transform.position = spawnPlayer;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            enterAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enterAllowed = false;
    }
}
