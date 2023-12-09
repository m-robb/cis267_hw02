using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    [SerializeField] EnemyHealthBars hb;
    public Behaviour hbCanvas;
    public GameObject woodToDrop;
    public Transform treeLocation;
    public float maxHealth;
    public float health;

    // Start is called before the first frame update
    void Start()
    {
        hb = GetComponentInChildren<EnemyHealthBars>();
        health = maxHealth;
        hb.updateHealthBar(health, maxHealth);
        hbCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        checkHealth();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("WEAPON"))
        {
            hbCanvas.enabled = true;

            takeDamage(collision.gameObject.GetComponentInChildren<Sword>().getTreeDamage());
        }
    }

    public void takeDamage(float d)
    {
        Debug.Log(d);
        health -= d;
        hb.updateHealthBar(health, maxHealth);
    }

    private void checkHealth()
    {
        if (health <= 0)
        {
            dropWood();
            Destroy(this.gameObject);
        }
    }

    private void dropWood()
    {
        Instantiate(woodToDrop, treeLocation.position, woodToDrop.transform.rotation);
    }
}
