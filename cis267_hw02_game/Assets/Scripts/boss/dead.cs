using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dead : MonoBehaviour
{
    private playerHealthBoss phealth;
    public GameObject playerHealthBar;
    // Start is called before the first frame update
    void Start()
    {
        phealth = GetComponent<playerHealthBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        if (phealth.health <= 0)
        {
            Debug.Log("0 Health");
            SceneManager.LoadScene("Boss02");
           
        }
    }
}
