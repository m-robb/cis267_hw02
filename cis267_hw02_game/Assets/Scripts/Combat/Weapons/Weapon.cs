using static Utilities;
using UnityEngine;


public abstract class Weapon : MonoBehaviour {
	[Tooltip("Damage that does not change with velocity.")]
	public int damageFlat;
	[Tooltip("The coefficient for the velocity-based component of damage.")]
	public float damageVelocityMultiplier;

	[HideInInspector] public bool armed;


	/*
	 * Call when the weapon hits a combatant.
	 * Returns the projectile's damage.
	 */
	public abstract float hit();
}
