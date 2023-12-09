using static Utilities;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Combatant : MonoBehaviour
{
	[Tooltip("The base health of this combatant. Other numbers may modify "
			+ "it, so don't read from it directly. "
			+ "If it drops to zero or below, the combatant dies.")]
	[Min(1)]
	public int healthBase;

	/* The combatant's current health. */
	private int health;


	void Start()
	{
		health = healthMax();
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		//Debug.Log(name + " detected something.");
		string tag;

		tag = collision.tag;

		//Debug.Log(tag);
		
		if (tag == TAG_WEAPON) {
			Weapon weapon;

			weapon = collision.GetComponent<Weapon>();

			takeDamage(weapon.hit());

			Debug.Log(name + "'s new health: " + health);
		}

	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("WEAPON"))
        {
            Debug.Log("Hit by player weapon");
            takeDamage(collision.collider.gameObject.GetComponentInParent<Sword>().getAttackDamage());
        }
		if (collision.collider.gameObject.CompareTag("Orc"))
		{
            Debug.Log("Hit by orc himself");
            takeDamage(collision.collider.gameObject.GetComponent<OrcController>().getPhysicalDamage());
        }
        if (collision.collider.gameObject.CompareTag("OrcAxe"))
        {
            Debug.Log("Hit by orc's axe");
            takeDamage(collision.collider.gameObject.GetComponentInParent<OrcController>().getAttackDamage());
        }
		if (collision.collider.gameObject.CompareTag("ThiefDagger"))
		{
            Debug.Log("Hit by thief's dagger");
            takeDamage(collision.collider.gameObject.GetComponentInParent<ThiefController>().getAttackDamage());
        }
        if (collision.collider.gameObject.CompareTag("EnemyCow"))
        {
            Debug.Log("Hit by cow himself");
            takeDamage(collision.collider.gameObject.GetComponentInParent<CowEnemyController>().getPhysicalDamage());
        }
        if (collision.collider.gameObject.CompareTag("CowDagger"))
        {
            Debug.Log("Hit by cow's dagger");
            takeDamage(collision.collider.gameObject.GetComponentInParent<CowEnemyController>().getAttackDamage());
        }
        if (collision.collider.gameObject.CompareTag("CowClub"))
        {
            Debug.Log("Hit by cow's club");
            takeDamage(collision.collider.gameObject.GetComponentInParent<CowEnemyController>().getAttackDamage());
        }
    }


    /*
	 * Returns the combatant's maximum health.
	 */
    public int healthMax()
	{
		return healthBase; /* Add modifiers for max health here. */
	}

	public int curHealth()
	{
		return health;
	}

	/*
	 * Applies damage to the combatant.
	 * Negative numbers heal the combatant!
	 */
	public void takeDamage(int d)
	{
		health -= d;

		if (health <= 0)
		{
			die();
		}
	}

	/*
	 * Applies damage to the combatant.
	 * Negative numbers heal the combatant!
	 * Truncates the float to an int (this is intended).
	 */
	public void takeDamage(float d)
	{
		takeDamage((int)d);
	}

	/*
	 * Kills the combatant.
	 */
	private void die()
	{
		Debug.Log(gameObject.name + " was slain.");

		//Don't destroy game object here. It will mess up enemies dropping their weapons.
	}
}
