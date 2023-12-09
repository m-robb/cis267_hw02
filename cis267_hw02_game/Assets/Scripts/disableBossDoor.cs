using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableBossDoor : MonoBehaviour
{
    private Combatant currentHealth;
    public GameObject door;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = GetComponent<Combatant>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth.curHealth() == 0)
        {
            door.SetActive(true);
        }
    }
}
