using static Utilities;
using UnityEngine;


/*
 * Created to make retrieving input slightly easier. It prevents the need to
 * 		collect input in each script's individual Update.
 * There may only be one.
 */
public class In: MonoBehaviour {
	public struct InputDirection {
		public float x;
		public float y;
		public Vector3 v3;
		public Vector2 v2;
	}

	public struct ButtonData {
		public bool down;
		public bool current;
		public bool up;
	}


	[HideInInspector] public static InputDirection movement;
	[HideInInspector] public static InputDirection direction;
	[HideInInspector] public static ButtonData accept;
	[HideInInspector] public static ButtonData reject;
	[HideInInspector] public static ButtonData attack01;

	[HideInInspector] private static In unique;


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

		accept.down = false;
		accept.current = false;
		accept.up = false;

		reject.down = false;
		reject.current = false;
		reject.up = false;

		attack01.down = false;
		attack01.current = false;
		attack01.up = false;
	}

	void Update() {
		movement.x = Input.GetAxisRaw(INPUT_AXIS_MOVEMENT_X);
		movement.y = Input.GetAxisRaw(INPUT_AXIS_MOVEMENT_Y);
		movement.v3.x = movement.x;
		movement.v3.y = movement.y;
		movement.v3.z = 0.00f;
		movement.v2.x = movement.x;
		movement.v2.y = movement.y;

		direction.x = Input.GetAxisRaw(INPUT_AXIS_DIRECTION_X);
		direction.y = Input.GetAxisRaw(INPUT_AXIS_DIRECTION_Y);
		direction.v3.x = direction.x;
		direction.v3.y = direction.y;
		direction.v3.z = 0.00f;
		direction.v2.x = direction.x;
		direction.v2.y = direction.y;

		accept.down = Input.GetButtonDown(INPUT_BUTTON_ACCEPT);
		accept.current = Input.GetButton(INPUT_BUTTON_ACCEPT);
		accept.up = Input.GetButtonUp(INPUT_BUTTON_ACCEPT);

		reject.down = Input.GetButtonDown(INPUT_BUTTON_REJECT);
		reject.current = Input.GetButton(INPUT_BUTTON_REJECT);
		reject.up = Input.GetButtonUp(INPUT_BUTTON_REJECT);

		attack01.down = Input.GetButtonDown(INPUT_BUTTON_ATTACK01);
		attack01.current = Input.GetButton(INPUT_BUTTON_ATTACK01);
		attack01.up = Input.GetButtonUp(INPUT_BUTTON_ATTACK01);
	}
}
