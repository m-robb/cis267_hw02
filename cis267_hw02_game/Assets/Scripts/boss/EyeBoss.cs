using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBoss : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    public float timeToArrive;
    public float idleTime;
    private float startTime;
    private Vector3 targetDirection;
    public float minSpeed;
    private PlayerBossMovement pHealth;
    public float damage;
    private GameObject canvas;



    // Start is called before the first frame update
    void Start()
    {
        startTime = 0;
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //timer
        if (startTime + idleTime < Time.time) 
        {
            attack();
            startTime = Time.time;
        }
        


    }
    private void attack()
    {
        targetDirection = player.transform.position - transform.position;

        rb.velocity = targetDirection;
        
        if (rb.velocity.magnitude < minSpeed)
        {
            rb.velocity = rb.velocity.normalized * minSpeed;
        }
        transform.eulerAngles = new Vector3(0,0,Mathf.Atan2(targetDirection.y, targetDirection.x)*Mathf.Rad2Deg);
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
                canvas.GetComponent<playerHealthBoss>().changeHealth(10);
            }
            //Debug.Log("here");
            //Destroy(this.gameObject);

        }
    }
}
