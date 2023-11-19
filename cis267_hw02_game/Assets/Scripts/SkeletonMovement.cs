using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 position;
    public float movementSpeed;
    private float movementDistance;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        position = transform.position;
        movementSpeed = 2;
        walkSkeleton();
    }

    
    void Update()
    {
        
    }

    private void walkSkeleton()
    {
        //wander around
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //move toward player and attack
        }
    }
}
