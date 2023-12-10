using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableBossDoor : MonoBehaviour
{
    public GameObject door;
    private playerHealthBoss bhealth;
    public GameObject bossHealthBar;
    public GameObject boss;


    // Start is called before the first frame update
    void Start()
    {
        bhealth = GetComponent<playerHealthBoss>();

    }

    // Update is called once per frame
    void Update()
    {
        

        if (bhealth.health <= 0)
        {
            Debug.Log("0 Health");
            door.SetActive(true);
            boss.SetActive(false);
        }
    }
}
