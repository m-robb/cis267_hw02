using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss02bullet : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float speed;
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
        
    }
}
