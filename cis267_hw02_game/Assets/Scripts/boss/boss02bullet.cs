using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss02bullet : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float speed;
    private float timer;
    private PlayerBossMovement pHealth;
    public float damage;
    private GameObject canvas;

    //private Projectile projectile;
    // Start is called before the first frame update
    void Start()
    {
        //projectile = GetComponent<Projectile>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2 (direction.x, direction.y).normalized * speed;
        //projectile.launch(speed, direction);
        
        float rotateThingy = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotateThingy);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 5)
        {

            Destroy(gameObject);
            timer = 0;

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pHealth = other.transform.parent.gameObject.GetComponent<PlayerBossMovement>();
            //if (pHealth != null)
            //{
            //    Debug.Log("pHealth");
            //}

            if (other.gameObject.CompareTag("Player"))
            {
                //Debug.Log("trigger");
                canvas = pHealth.getCanvas(0);
                //Debug.Log("grass");
                canvas.GetComponent<playerHealthBoss>().changeHealth(damage);
            }
            //Debug.Log("here");
            Destroy(this.gameObject);

        }
    }
}
