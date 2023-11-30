using static Utilities;
using UnityEngine;
using Unity.Collections.LowLevel.Unsafe;

public class Bow : MonoBehaviour {
	public GameObject bowString;
	[SerializeField] private float stringWidth;

	[Header("IDLE")]
	[Tooltip("Local position.\nMeasured in uu.")]
	public Vector3 idleOffset;
	[Tooltip("Local rotation.\nMeasured in degrees.")]
	public Vector3 idleAngle;

	[Header("BOW")]
	[Tooltip("The time from call until release.\nMeasured in seconds.")]
	public float drawDuration;
	[Tooltip("The distance the furthest away point on the string will be.\n"
			+ "Measured in uu.")]
	public float drawLength;
	[Tooltip("The size of the array of vertices which defines the string.")]
	public int roundness;

	[Header("LAUNCH")]
	[Tooltip("The projectile to launch.\n"
			+ "It is required to have a Projectile component.")]
	public GameObject arrowPrefab;
	[Tooltip("The speed the projectile is launched at.\nMeasured in uu/s.")]
	[Min(0.00f)]
	public float launchSpeed;
	[Tooltip("The offset the projectile is launched from.\n"
			+ "Measured in uu.")]
	public Vector3 launchOffset;
	[Tooltip("The angle the projectile is launched at.\n"
			+ "Measured in degrees.")]
	public Vector3 launchAngle;

	private LineRenderer stringRenderer;
	private float offsetX;
	private float height;
	private float actionStart;
	private bool isShooting;
	private Vector3 windup;
	private GameObject arrowNew;


	void Start() {
		stringRenderer = bowString.GetComponent<LineRenderer>();

		offsetX = stringRenderer.GetPosition(0).x;
		height = Mathf.Abs(stringRenderer.GetPosition(0).y
				- stringRenderer.GetPosition(1).y);

		isShooting = false;

		/* Adjust the string's width. */
		stringRenderer.startWidth = 1.00f;
		stringRenderer.endWidth = 1.00f;
		setStringWidth(stringWidth);

		idle();
	}

	void Update() {
		if (isShooting) {
			float completion;

			completion = (Time.time - actionStart) / drawDuration;

			stringCurve(completion);
			windup = idleAngle + launchAngle * completion;
			transform.localPosition = idleOffset
					+ launchOffset * completion;

			/* Move the arrow to the bow string. */
			arrowNew.transform.localPosition
					= stringRenderer.GetPosition((int)
					((roundness - 1) / 2.00f));

			if (completion >= 1.00f) {
				/* Release the projectile. */
				arrowNew.GetComponent<Projectile>().launchAngle(
						launchSpeed, windup.z);
				arrowNew.transform.parent = null;

				idle();

				isShooting = false;
			}
		}

		transform.localEulerAngles = convertAnglesVector3(windup);
	}


	/*
	 * Shoots an arrow from the bow.
	 */
	[ContextMenu("shoot")]
	public void shoot() {
		if (isShooting) { return; }

		isShooting = true;

		arrowNew = Instantiate(arrowPrefab, transform);

		actionStart = Time.time;
	}

	/*
	 * Changes the bow string's width.
	 */
	public void setStringWidth(float width) {
		stringWidth = width;
		stringRenderer.widthMultiplier = stringWidth;
	}

	/*
	 * Resets the sword's position and rotation to idle.
	 */
	public void idle() {
		stringCurve(0.00f);
		transform.localPosition = idleOffset;
		windup = idleAngle;
	}

	/*
	 * Create and set the accurate positions of the string.
	 * Drawn should be a percentage in the domain of [0.00f, 1.00f].
	 */
	private void stringCurve(float drawn) {
		Vector3[] positions;
		float middle;
		int i;

		positions = new Vector3[roundness];
		middle = (roundness - 1) / 2.00f;

		for (i = 0; i < roundness; ++i) {
			positions[i] = new Vector3(
				offsetX - (-Mathf.Pow((i - middle) / middle,
						2.00f) + 1.00f)
						* drawLength * drawn,
				-(height / 2.00f) + height
						* (i / (roundness - 1.00f)),
				0.00f
			);
		}

		stringRenderer.positionCount = roundness;
		stringRenderer.SetPositions(positions);
	}
}
