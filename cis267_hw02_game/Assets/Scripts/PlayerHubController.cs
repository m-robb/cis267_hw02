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
    private Animator animator;

    private GameManager gm;
    private Combatant combatantScript;

    //Movement variables
    public float movementSpeed;
    private float inputHorizontal;
    private float inputVertical;
    private bool facingRight = false;

    //Health/HealthBar Stuff
    [SerializeField] PlayerHealthBar hb;
    private float health;
    private float maxHealth;

    //Giving players GameObjects (Swords, Apples, etc)
    public Transform playerPosition;
    public Transform backArmPosition;
    public GameObject daggerToGivePlayer;
    public GameObject axeToGivePlayer;
    private GameObject emptyObj;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        gm = GetComponent<GameManager>();
        combatantScript = GetComponent<Combatant>();
        animator = GetComponent<Animator>();

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
        animate();
        swingSword();
    }

    private void movePlayer()
    {
        //Get horizontal and vertical input
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
        //Update player's position
        playerRigidBody.velocity = new Vector2(movementSpeed * inputHorizontal, movementSpeed * inputVertical);
    }

    private void animate()
    {
        if (inputHorizontal != 0 || inputVertical != 0) //I'm moving
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false); //I'm idle
        }

        if (inputHorizontal > 0 && !facingRight)
        {
            flip();
        }
        else if (inputHorizontal < 0 && facingRight)
        {
            flip();
        }
    }

    private void flip()
    {
        Vector3 currentScale = gameObject.transform.localScale; //Get current scale
        currentScale.x *= -1; //Opposite it (-1 becomes 1 and vice versa). This flips the sprite 
        gameObject.transform.localScale = currentScale; //Set it
        facingRight = !facingRight; //Flip boolean also
        hb.flipHealthBar(); //Flip health bar since I just flipped the player (which also flips the health bar)
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
            emptyObj = new GameObject("EmptyObjectForDagger"); //Make empty object for it
            emptyObj.transform.parent = backArmPosition.gameObject.transform; //Make the player's back arm the parent
            //Instantiate a newDagger
            GameObject newDagger = Instantiate(daggerToGivePlayer, playerPosition.position, daggerToGivePlayer.transform.rotation);
            newDagger.transform.parent = emptyObj.gameObject.transform; //Make the empty object the parent of the new dagger
            emptyObj.transform.position = backArmPosition.transform.position; //Move the empty object holding the dagger to the arm

            //Destroy the collectable
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("AxeCollectable"))
        {
            //GIVE PLAYER AXE
            emptyObj = new GameObject("EmptyObjectForAxe"); //Make empty object for it
            emptyObj.transform.parent = backArmPosition.gameObject.transform; //Make the player's back arm the parent
            //Instantiate a newAxe
            GameObject newAxe = Instantiate(axeToGivePlayer, playerPosition.position, axeToGivePlayer.transform.rotation);
            newAxe.transform.parent = emptyObj.gameObject.transform; //Make the empty object the parent of the new axe
            emptyObj.transform.position = backArmPosition.transform.position; //Move the empty object holding the axe to the arm

            //Destroy the collectable
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("GoldSack"))
        {
            //GIVE PLAYER GOLD

            Destroy(collision.gameObject);
        }
    }

    private void takeDamage(float damage)
    {
        combatantScript.takeDamage(damage);
        health = combatantScript.curHealth();
        hb.updateHealthBar(health, maxHealth);
    }

    private void swingSword()
    {
        if (Input.GetAxisRaw("Fire1") != 0)
        {
            GetComponentInChildren<Sword>().swing();
        }
    }

}
