using static Utilities;
using System.Threading;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : Weapon {
	[Tooltip("The number of combatant's this projectile can hit.")]
	[Min(0)]
	public int pierce;
	[Tooltip("The delay after launch until the projectile is armed.\n"
			+ "Measured in seconds.")]
	public float armingDelay;

	private Rigidbody2D rb;


	void Start() {
		rb = GetComponent<Rigidbody2D>();
		armed = false;
	}


	public override float hit() {
		if (!armed) { return 0.00f; }
		if (--pierce <= 0) {
			/* Destroy is queued, the function will first return. */
			Destroy(gameObject);
		}

		return damageFlat + rb.velocity.magnitude
				* damageVelocityMultiplier;
	}

	/*
	 * Launches the projectile in the specified direction.
	 * Normalizes the direction before usage.
	 */
	public void launch(float speed, Vector3 direction,
			bool rotateToVelocity = true) {
		Thread armingThread;

		rb.velocity = speed * direction.normalized;

		if (rotateToVelocity) {
			transform.localEulerAngles = rb.velocity.normalized
					* 360.00f;
		}

		/* Setup timer to arm the projectile. */
		armingThread = new Thread(armingTimer);
		armingThread.Start();
	}

	/*
	 * Launches the projectile in the specified direction.
	 * Angle is measured in degrees. 0.00f degrees points to the
	 * 		positive x-axis.
	 */
	public void launchAngle(float speed, float angle,
			bool rotateToVelocity = true) {
		float angleRadians;

		angleRadians = Mathf.Deg2Rad * convertAngles(angle);

		launch(speed, new Vector3(Mathf.Cos(angleRadians),
				Mathf.Sin(angleRadians), 0.00f),
				rotateToVelocity);
	}

	/*
	 * BLOCKS; ONLY FOR USE IN A THREAD.
	 * Sets armed to true after delay seconds.
	 */
	private void armingTimer() {
		System.Diagnostics.Stopwatch stopwatch;

		stopwatch = System.Diagnostics.Stopwatch.StartNew();

		/* Block... */
		while (stopwatch.ElapsedMilliseconds < armingDelay * 1000);

		armed = true;
		UnityEngine.Debug.Log("Armed!");
	}
}
