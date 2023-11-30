using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCamera : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(transform.position);
        //Debug.Log(player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = player.transform.position;
        //Debug.Log(transform.position);
        //Debug.Log(player.transform.position);
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }
}
