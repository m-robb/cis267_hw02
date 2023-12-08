using static Utilities;
using UnityEngine;


[RequireComponent(typeof(Melee))]
public class Sword : MonoBehaviour {
	public GameObject blade;
	[Min(0.00f)] [SerializeField]
	private float bladeLengthMultiplier;

	[Header("IDLE")] /* ------------------------------------------------- */
	[Tooltip("The angle the sword rests when idle.\nMeasured in degrees.")]
	public float idleAngle;
	[Tooltip("The offset the sword rests when idle.\nMeasured in uu.")]
	public Vector3 idleOffset;

	[Header("SWING")] /* ------------------------------------------------ */
	[Tooltip("Total length of a swing.\nMeasured in degrees.")]
	public float swingArc;
	[Tooltip("Where the arc of a swing starts.\nMeasured in degrees.")]
	public float swingAngle;
	[Tooltip("Measured in degrees / second.")]
	[Min(0.00f)]
	public float swingSpeed;
	[Tooltip("Radius of the circle that the swing should track.\n"
			+ "Measured in degrees.")]
	public float swingRadius;

	[Header("STAB")] /* ------------------------------------------------- */
	[Tooltip("Measured in uu/s.")]
	[Min(0.00f)]
	public float stabSpeed;
	[Tooltip("Local position.\nMeasured in uu.")]
	public Vector3 stabStart;
	[Tooltip("Local position.\nMeasured in uu.")]
	public Vector3 stabEnd;

    [Header("Attack/Damage #s")] /* ------------------------------------------------- */
    [Tooltip("Attack Damage")]
    public float attackDamage;


    private bool isSwinging;
	private bool isStabbing;
	private Vector3 windup;
	private float actionStart;
	private float actionDuration;


	void Start() {
		isSwinging = false;
		isStabbing = false;

		idle();
		setLength(bladeLengthMultiplier);
	}

	void Update() {
		if (isSwinging) {
			float completion;

			completion = (Time.time - actionStart) / actionDuration;

			windup = Vector3.forward
					* (swingAngle + swingArc * completion);

			transform.localPosition = new Vector3(
				swingRadius * Mathf.Cos(Mathf.Deg2Rad
						* (swingAngle
						+ swingArc * completion)),
				swingRadius * Mathf.Sin(Mathf.Deg2Rad
						* (swingAngle
						+ swingArc * completion)),
				transform.localPosition.z
			) * Mathf.Sign(swingSpeed * swingAngle);

			if (completion >= 1.00f) {
				/* End the swing. */
				idle();

				isSwinging = false;
			}
		}
		else if (isStabbing) {
			float completion;

			completion = (Time.time - actionStart) / actionDuration;

			transform.localPosition = stabStart
					+ (stabEnd - stabStart) * completion;
			if (completion >= 1.00f) {
				/* End the stab. */
				idle();
				isStabbing = false;
			}
		}

		transform.localEulerAngles = convertAnglesVector3(windup);
	}


	/*
	 * Swings the sword in an arc.
	 */
	[ContextMenu("swing()")]
	public void swing() {
		if (isSwinging || isStabbing) { return; }

		isSwinging = true;

		windup = Vector3.forward * swingAngle;
		actionStart = Time.time;
		actionDuration = Mathf.Abs(swingArc / swingSpeed);
	}

	/*
	 * Stabs the sword forward.
	 */
	[ContextMenu("stab()")]
	public void stab() {
		Vector3 stabDiff;
		if (isSwinging || isStabbing) { return; }

		isStabbing = true;

		stabDiff = stabEnd - stabStart;

		windup = Vector3.forward * Mathf.Atan2(stabDiff.y, stabDiff.x)
				* Mathf.Rad2Deg;

		actionStart = Time.time;
		actionDuration = Mathf.Abs(stabDiff.magnitude / stabSpeed);
	}

	/*
	 * Changes the sword blade's length.
	 */
	public void setLength(float multiplier) {
		bladeLengthMultiplier = multiplier;
		blade.transform.localScale = v3Set(blade.transform.localScale,
				0, bladeLengthMultiplier);
	}

	/*
	 * Resets the sword's position and rotation to idle.
	 */
	public void idle() {
		transform.localPosition = idleOffset;
		windup = Vector3.forward * idleAngle;
	}

	public float getAttackDamage()
	{
		return attackDamage;
	}
}
