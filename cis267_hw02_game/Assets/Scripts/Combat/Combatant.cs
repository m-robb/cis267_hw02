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
		Debug.Log(name + " detected something.");
		string tag;

		tag = collision.tag;

		Debug.Log(tag);

		if (tag == TAG_WEAPON) {
			Weapon weapon;

			weapon = collision.GetComponent<Weapon>();

			takeDamage(weapon.hit());

			Debug.Log(name + "'s new health: " + health);
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
	}
}
