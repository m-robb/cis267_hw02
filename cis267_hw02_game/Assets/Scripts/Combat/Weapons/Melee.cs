using static Utilities;
using UnityEngine;


public class Melee : Weapon {
	private Vector3 positionLast;
	private Vector3 velocity;


	void Start() {
		armed = false;
		positionLast = Vector3.zero;
		velocity = Vector3.zero;
	}

	void FixedUpdate() {
		Vector3 position;

		position = transform.position;

		velocity = positionLast - position;
		positionLast = position;
	}


	public override float hit() {
		if (!armed) { return 0.00f; }
		return damageVelocityMultiplier + velocity.magnitude
				* damageVelocityMultiplier;
	}
}
