using UnityEngine;

/*
 * Moves the transform with respect to the percentage of the tick remaining.
 * Mostly behaves the same way as using a Rigidbody2D for applying velocity.
 */
public class Interpolator : MonoBehaviour {
	[System.NonSerialized] public Vector3 velocity;
	[System.NonSerialized] public Vector3 velocityAngular;
	[System.NonSerialized] public Vector3 angles;

	private float tickPercentageLast;
	

	void Start() {
		velocity = Vector3.zero;
		angles = transform.localEulerAngles;
		tickPercentageLast = 0.00f;
	}

	void Update() {
		float tickPercentage;
		float tickDelta;

		tickPercentage = (Time.time % Time.fixedDeltaTime)
				/ Time.fixedDeltaTime;

		tickDelta = tickPercentage - tickPercentageLast;

		/*
		 * Convert velocity and angular velocity from
		 * per second -> per tick -> per percentage of tick
		 * and apply it.
		 */
		transform.position += velocity * Time.fixedDeltaTime
				* tickDelta;

		angles += velocityAngular * Time.fixedDeltaTime * tickDelta;
		transform.localEulerAngles = new Vector3(
				(angles.x % 360.00f) - 180.00f,
				(angles.y % 360.00f) - 180.00f,
				(angles.z % 360.00f) - 180.00f
		);

		tickPercentageLast = tickPercentage;

		Debug.Log(angles);
	}

	void FixedUpdate() {
		/* Flush the remaining velocity. */
		float tickDelta;

		tickDelta = 1.00f - tickPercentageLast;

		transform.position += velocity * Time.fixedDeltaTime
				* tickDelta;

		angles += velocityAngular * Time.fixedDeltaTime * tickDelta;
		transform.localEulerAngles = new Vector3(
				(angles.x % 360.00f) - 180.00f,
				(angles.y % 360.00f) - 180.00f,
				(angles.z % 360.00f) - 180.00f
		);

		tickPercentageLast = 0.00f;
	}

	/*
	 * The panic button.
	 * Immediately removes all velocity and angular velocity.
	 */
	public void stop() {
		velocity = Vector3.zero;
		velocityAngular = Vector3.zero;
	}
}
