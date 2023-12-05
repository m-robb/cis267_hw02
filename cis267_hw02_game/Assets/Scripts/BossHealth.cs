using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public string name;
    private Slider healthBar;
    private GameObject boss;
    private Combatant currentHealth;
    

    void Start() {
        boss = GameObject.Find(name);
        currentHealth = boss.GetComponent<Combatant>();
        healthBar = this.GetComponent<Slider>();
    }

    void Update() {
        healthBar.value = currentHealth.curHealth() / currentHealth.healthMax();
    }
}
