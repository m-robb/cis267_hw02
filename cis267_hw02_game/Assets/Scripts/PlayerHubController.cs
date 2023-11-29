using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHubController : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    public GameObject player;
    public Camera cam;
    public static PlayerHubController Instance;

    private GameManager gm;
    private Combatant combatantScript;

    //Movement variables
    public float movementSpeed;
    private float inputHorizontal;
    private float inputVertical;

    //Health/HealthBar Stuff
    [SerializeField] PlayerHealthBar hb;
    private float health;
    private float maxHealth;

    //Giving players GameObjects (Swords, Apples, etc)
    public Transform playerPosition;
    public GameObject swordToGivePlayer;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        gm = GetComponent<GameManager>();
        combatantScript = GetComponent<Combatant>();

        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);

        //Health Bar Stuff
        hb = GetComponentInChildren<PlayerHealthBar>();
        health = combatantScript.healthMax();
        maxHealth = combatantScript.healthMax();
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
        else if (collision.gameObject.CompareTag("Skeleton")) //Player gets hit by/runs into skeleton
        {
            takeDamage(collision.gameObject.GetComponent<SkeletonMovement>().getAttackDamage());
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
            //GIVE PLAYER APPLE


            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("DaggerCollectable"))
        {
            //GIVE PLAYER DAGGER
            Instantiate(swordToGivePlayer, playerPosition.position, swordToGivePlayer.transform.rotation);

            Destroy(collision.gameObject);
        }
    }

    private void takeDamage(float damage)
    {
        combatantScript.takeDamage(damage);
        health = combatantScript.curHealth();
        hb.updateHealthBar(health, maxHealth);
    }
}
