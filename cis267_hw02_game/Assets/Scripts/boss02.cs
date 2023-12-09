using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss02 : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletpos;
    public Transform bullettpos2;


    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 2.8)
        {
            timer = 0;
            shoot();

        }
        void shoot()
        {
            Instantiate(bullet, bulletpos.position, Quaternion.identity);
            Instantiate(bullet, bullettpos2.position, Quaternion.identity);
        }


    }
}
