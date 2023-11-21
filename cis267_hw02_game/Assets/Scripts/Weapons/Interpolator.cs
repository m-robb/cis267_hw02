using UnityEngine;

/*
 * Moves the transform with respect to the percentage of the tick remaining.
 * Mostly behaves the same way as using a Rigidbody2D for applying velocity.
 * Note that this doesn't do any interpolation. It is more a movement smoother.
 */
public class Interpolator : MonoBehaviour {
	[System.NonSerialized] public Vector3 velocity;
	[System.NonSerialized] public Vector3 velocityAngular;
	[System.NonSerialized] public Vector3 angles;
	[System.NonSerialized] public float radius;
	public float test;

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
				Interpolator.convertAngles(angles.x),
				Interpolator.convertAngles(angles.y),
				Interpolator.convertAngles(angles.z)
		);


		tickPercentageLast = tickPercentage;
	}

	void FixedUpdate() {
		/* Flush the remaining velocity. */
		float tickDelta;

		tickDelta = 1.00f - tickPercentageLast;

		/* Calculate a fake centripital force if there is a radius. */
		if (radius != 0.00f) {
			Debug.Log("Before:\t" + velocity);
			velocity += -transform.localPosition.normalized
					* (Mathf.Pow(velocity.magnitude, 0.50f)
					/ radius);
			Debug.Log("After:\t" + velocity);
		}

		transform.position += velocity * Time.fixedDeltaTime
				* tickDelta;

		angles += velocityAngular * Time.fixedDeltaTime * tickDelta;
		transform.localEulerAngles = new Vector3(
				Interpolator.convertAngles(angles.x),
				Interpolator.convertAngles(angles.y),
				Interpolator.convertAngles(angles.z)
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

	/*
	 * Converts from degrees (where 0.00f points to the right) to something
	 * 		Unity understands.
	 */
	private static float convertAngles(float x) {
		return (x % 360.00f) - 180.00f;
	}
}
