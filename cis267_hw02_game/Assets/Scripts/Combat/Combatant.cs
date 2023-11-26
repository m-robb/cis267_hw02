using static Utilities;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Combatant : MonoBehaviour {
	[Tooltip("The base health of this combatant. Other numbers may modify "
			+ "it, so don't read from it directly. "
			+ "If it drops to zero or below, the combatant dies.")]
	[Min(1)]
	public int healthBase;

	/* The combatant's current health. */
	private int health;


	void Start() {
		health = healthMax();
	}

	void OnTriggerEnter2D(Collider2D collision) {
		Debug.Log(name + " detected something.");
		string tag;

		tag = collision.tag;

		Debug.Log(tag);

		if (tag == TAG_WEAPON) {
			Weapon weapon;

			weapon = collision.GetComponent<Weapon>();

			damage(weapon.hit());

			Debug.Log(name + "'s new health: " + health);
		}

	}


	/*
	 * Returns the combatant's maximum health.
	 */
	public int healthMax() {
		return healthBase; /* Add modifiers for max health here. */
	}

	/*
	 * Applies damage to the combatant.
	 * Negative numbers heal the combatant!
	 */
	public void damage(int n) {
		health -= n;

		if (health <= 0) {
			die();
		}
	}

	/*
	 * Applies damage to the combatant.
	 * Negative numbers heal the combatant!
	 * Truncates the float to an int (this is intended).
	 */
	public void damage(float x) {
		damage((int)x);
	}

	/*
	 * Kills the combatant.
	 */
	private void die() {
		Debug.Log(gameObject.name + " was slain.");
	}
}
