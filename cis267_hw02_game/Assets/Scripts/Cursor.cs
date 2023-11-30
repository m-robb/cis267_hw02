using static Utilities;
using UnityEngine;


/*
 * Creates a cursor that can be moved with default movement keys.
 * There may only be one Cursor. All others are immediately destroyed.
 * Upon being enabled or reset, the Cursor will set it's parent
 * 		to the main camera.
 */
public class Cursor : MonoBehaviour {
	/* The sprite to be used for the cursor. */
	public Sprite sprite;
	/* Measured in percent of window (width) per second at full tilt. */
	public float speed;

	private SpriteRenderer spriteRenderer;
	/* A percentage of the camera's render region. */
	private Vector3 position;

	/* Make the cursor available to everyone. */
	public static Cursor cursor;


	void Start() {
		if (Cursor.cursor) {
			Debug.Log("There may only be one Cursor.");
			Destroy(this); /* Only destroys the script. */
		}

		Cursor.cursor = this;

		reset();
		cursor.enable(true);

		/* Make the cursor visible. */
		spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
		spriteRenderer.sprite = sprite;
		spriteRenderer.spriteSortPoint = SpriteSortPoint.Center;
		
	}

	void Update() {
		move();
	}

	void OnEnable() {
		reset();
	}


	/*
	 * Moves the cursor based on joystick movement or button presses (axes).
	 * Multiplies movement values by Time.deltaTime: call in Update.
	 */
	private void move() {
		float horizontal;
		float vertical;

		/* Update position. */
		horizontal = Input.GetAxisRaw(AXIS_X);
		vertical = Input.GetAxisRaw(AXIS_Y);

		if (horizontal != 0.00f) {
			position.x += horizontal * speed * Time.deltaTime;
		}
		if (vertical != 0.00f) {
			/* Additional scaling to account for aspect ratio. */
			position.y += vertical * speed * Camera.main.aspect
					* Time.deltaTime;
		}

		/* Clamp values to screen. */
		position.x = Mathf.Clamp(position.x, 0.00f, 1.00f);
		position.y = Mathf.Clamp(position.y, 0.00f, 1.00f);

		/* Convert and apply position in world coordinates. */
		transform.position = Camera.main.ViewportToWorldPoint(position);
	}

	/*
	 * Enables or disables the cursor.
	 * Simply calls the SetActive function.
	 */
	public void enable(bool b) {
		gameObject.SetActive(b);
	}

	/*
	 * Resets the cursor's position to the center of the screen and sets
	 * 		it's parent to the main camera.
	 * Note: the cursor's transform only changes when Update is called.
	 */
	public void reset() {
		transform.parent = Camera.main.transform;
		position = new Vector3(0.50f, 0.50f, Camera.main.nearClipPlane);
	}
}
