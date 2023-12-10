using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public string go_name;
    private Slider healthBar;
    private GameObject entity;
    private Combatant currentHealth;
    

    void Start() {
        entity = GameObject.Find(go_name);
        currentHealth = entity.GetComponent<Combatant>();
        healthBar = this.GetComponent<Slider>();
    }

    void Update() {
        healthBar.value = currentHealth.curHealth() / currentHealth.healthMax();
    }
}
