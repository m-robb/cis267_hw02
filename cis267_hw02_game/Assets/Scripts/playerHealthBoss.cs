using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class playerHealthBoss : MonoBehaviour
{
    public Image healthbar;
    public GameObject thing;
    public float health;
    public float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
        
        
    }
    public void changeHealth(float damage)
    {
        Debug.Log(health);
        health = health - damage;
        //Debug.Log(health);
    }
}
