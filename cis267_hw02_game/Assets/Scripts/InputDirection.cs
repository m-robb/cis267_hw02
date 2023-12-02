using static Utilities;
using UnityEngine;


/*
 * There may only be one.
 */
public class InputDirection : MonoBehaviour {
	public struct InputOutput {
		public float x;
		public float y;
		public Vector3 v3;
		public Vector2 v2;
	}
	[HideInInspector] public static InputOutput movement;
	[HideInInspector] public static InputOutput direction;

	[HideInInspector] private static InputDirection unique;


	void Start() {
		/* There may only be one. */
		if (unique != null) {
			Destroy(this);
		}

		unique = this;

		movement.x = 0.00f;
		movement.y = 0.00f;
		movement.v3 = Vector3.zero;
		movement.v2 = Vector2.zero;

		direction.x = 0.00f;
		direction.y = 0.00f;
		direction.v3 = Vector3.zero;
		direction.v2 = Vector2.zero;
	}

	void Update() {
		float x;
		float y;

		x = Input.GetAxisRaw(AXIS_MOVEMENT_X);
		y = Input.GetAxisRaw(AXIS_MOVEMENT_Y);
		movement.x = x;
		movement.y = y;
		movement.v3.x = x;
		movement.v3.y = y;
		movement.v2.x = x;
		movement.v2.y = y;

		x = Input.GetAxisRaw(AXIS_DIRECTION_X);
		y = Input.GetAxisRaw(AXIS_DIRECTION_Y);
		direction.x = x;
		direction.y = y;
		direction.v3.x = x;
		direction.v3.y = y;
		direction.v2.x = x;
		direction.v2.y = y;
	}
}
