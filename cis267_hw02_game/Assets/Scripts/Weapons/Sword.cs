using UnityEngine;


public class Sword : MonoBehaviour {
	public GameObject blade;
	public int swingDamage;
	public int stabDamage;
	public float bladeLength;
	/* The angle the sword rests when idle. Measured in degrees. */
	public float idleAngle;
	/* Total length of a swing. Measured in degrees. */
	public float swingArc;
	/* Where the arc of a swing starts. Measured in degrees. */
	public float swingAngle;
	/* Measured in 360.00f degrees / second. */
	public float swingSpeed;
	/* Measured in uu/s. */
	public float stabSpeed;
	/* Measured in uu. */
	public float stabDistance;
	/* Measured in degrees. */
	public float stabAngle;

	private Interpolator interpolator;
	private bool isSwinging;
	private bool isStabbing;
	private Vector3 idleAngles;
	private Vector3 swingAngles;
	private Vector3 stabAngles;


	void Start() {
		interpolator = GetComponent<Interpolator>();

		/* The sprite is offset by 90.00f degrees. Undo that. */
		/* 0.00f should point to the right (global). */
		idleAngle -= 90.00f;
		swingAngle -= 90.00f;
		stabAngle -= 90.00f;
		idleAngles = Vector3.forward * idleAngle;
		swingAngles = Vector3.forward * swingAngle;
		stabAngles = Vector3.forward * stabAngle;

		isSwinging = false;
		isStabbing = false;

		interpolator.angles = idleAngles;
		blade.transform.localScale = new Vector3(
				1.00f, bladeLength, 1.00f);
	}

	void Update() {}

	void FixedUpdate() {
		if (isSwinging) {
			if (Mathf.Abs(interpolator.angles.z - swingAngle)
					>= swingArc) {
				/* End the swing. */
				interpolator.velocityAngular = Vector3.zero;
				interpolator.angles = idleAngles;

				isSwinging = false;
			}
		}
		else if (isStabbing) {
			if (transform.localPosition.x >= stabDistance) {
				/* End the stab. */
				interpolator.velocity = Vector3.zero;
				transform.localPosition = Vector3.zero;
				interpolator.angles = idleAngles;

				isStabbing = false;
			}
		}
	}


	/*
	 * Swings the sword in an arc.
	 */
	[ContextMenu("Swing")]
	public void swing() {
		if (isSwinging || isStabbing) { return; }

		isSwinging = true;

		interpolator.angles = swingAngles;
		interpolator.velocityAngular = new Vector3(0.00f, 0.00f,
				swingSpeed * 360.00f);
	}

	/*
	 * Stabs the sword forward.
	 */
	[ContextMenu("Stab")]
	public void stab() {
		if (isSwinging || isStabbing) { return; }

		isStabbing = true;

		interpolator.angles = stabAngles;
		interpolator.velocity = new Vector3(stabSpeed, 0.00f, 0.00f);
	}
}
