using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHubController : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    public GameObject player;
    public Camera cam;
    public static PlayerHubController Instance;

    public GameObject gameManager;
    private GameManager gm;

    //Movement variables
    public float movementSpeed;
    private float inputHorizontal;
    private float inputVertical;

    //Health/HealthBar Stuff
    [SerializeField] PlayerHealthBar hb;
    public float maxHealth;//100
    private float health;

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

        //Health Bar Stuff
        hb = GetComponentInChildren<PlayerHealthBar>();
        health = maxHealth;
        hb.updateHealthBar(health, maxHealth);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ThiefDagger")) //Thief hits me with dagger
        {
            takeDamage(collision.gameObject.GetComponentInParent<ThiefController>().getAttackDamage()); //Take damage
        }
        else if (collision.gameObject.CompareTag("EnemyCow")) //Player runs into cow
        {
            takeDamage(collision.gameObject.GetComponent<CowEnemyController>().getPhysicalDamage());
        }
        else if (collision.gameObject.CompareTag("CowClub")) //Cow hits me with club
        {
            takeDamage(collision.gameObject.GetComponentInParent<CowEnemyController>().getAttackDamage());
        }
        else if (collision.gameObject.CompareTag("CowDagger")) //Cow hits me with dagger
        {
            takeDamage(collision.gameObject.GetComponentInParent<CowEnemyController>().getAttackDamage());
        }
        else if (collision.gameObject.CompareTag("Orc")) //Player runs into Orc
        {
            takeDamage(collision.gameObject.GetComponent<OrcController>().getPhysicalDamage());
        }
        else if (collision.gameObject.CompareTag("OrcAxe")) //Orc hits player with axe
        {
            takeDamage(collision.gameObject.GetComponentInParent<OrcController>().getAttackDamage());
        }
        else if (collision.gameObject.CompareTag("Boss01Entrance"))
        {
            //Go to boss02
            SceneManager.LoadScene("Boss01");
            //All this does right now is load the scene. It doesn't carry player over or save any information to a static class yet
        }
        else if (collision.gameObject.CompareTag("Boss02Entrance"))
        {
            //Go to boss02
            SceneManager.LoadScene("Boss02");
            //All this does right now is load the scene. It doesn't carry player over or save any information to a static class yet
        }
        else if (collision.gameObject.CompareTag("Boss03Entrance"))
        {
            //Go to boss02
            SceneManager.LoadScene("Boss03");
            //All this does right now is load the scene. It doesn't carry player over or save any information to a static class yet
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Apple"))
        {
            Destroy(collision.gameObject);
            //GIVE PLAYER APPLE
        }
    }

    private void takeDamage(float damage)
    {
        health -= damage;
        hb.updateHealthBar(health, maxHealth);

        if (health <= 0)
        {
            //Die
            Destroy(this.gameObject);

            //YOU LOSE - GAME OVER MENU
        }
    }
}
