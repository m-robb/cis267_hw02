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
        targetDirection = player.transform.position - transform.position;

        transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg);




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
}
