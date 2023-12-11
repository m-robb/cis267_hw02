using UnityEngine;

/*
 * Miscellaneous utilities.
 */
public static class Utilities {
	/*
	 *********
	 * INPUT *
	 *********
	 */
	public const string AXIS_X = "Horizontal";
	public const string AXIS_Y = "Vertical";
	public const string BUTTON_ATTACK = "Fire1";
	public const string BUTTON_INVENTORY = "Fire2";
	/* public const string INPUT_AXIS_MOVEMENT_X = "MovementX"; */
	/* public const string INPUT_AXIS_MOVEMENT_Y = "MovementY"; */
	/* public const string INPUT_AXIS_DIRECTION_X = "DirectionX"; */
	/* public const string INPUT_AXIS_DIRECTION_Y = "DirectionY"; */
	/* public const string INPUT_BUTTON_ACCEPT = "Accept"; */
	/* public const string INPUT_BUTTON_REJECT = "Reject"; */
	/* public const string INPUT_BUTTON_ATTACK01 = "Attack01"; */


	/*
	 ********
	 * TAGS *
	 ********
	 */

	public const string TAG_PLAYER = "PLAYER";
	public const string TAG_ENEMY = "ENEMY";
	public const string TAG_WEAPON = "WEAPON";
	public const string TAG_INVENTORIABLE = "INVENTORIABLE";


	/*
	 ***********
	 * VECTOR3 *
	 ***********
	 */

	/*
	 * Returns a reference to vector with the specified index set to value.
	 */
	public static ref Vector3 v3Set(ref Vector3 vector, int index,
			float value) {
		vector[index] = value;
		return ref vector;
	}

	/*
	 * Returns a copy of vector with the specified index set to value.
	 */
	public static Vector3 v3Set(Vector3 vector, int index, float value) {
		vector[index] = value;
		return vector;
	}

	/*
	 * Returns a reference to vector with the specified index
	 * 		incremented by value.
	 */
	public static ref Vector3 v3Inc(ref Vector3 vector, int index,
			float value) {
		vector[index] += value;
		return ref vector;
	}

	/*
	 * Returns a copy of vector with the specified index
	 * 		incremented by value.
	 */
	public static Vector3 v3Inc(Vector3 vector, int index,
			float value) {
		vector[index] += value;
		return vector;
	}


	/*
	 **********
	 * ANGLES *
	 **********
	 */

	/*
	 * Converts from degrees (where 0.00f points to the right) to something
	 * 		Unity understands.
	 */
	public static float convertAngles(float degrees) {
		return ((degrees + 180.00f) % 360.00f) - 180.00f;
	}

	/*
	 * Converts an entire vector from degrees (where 0.00f points to
	 * 		the right) to something Unity understands.
	 */
	public static Vector3 convertAnglesVector3(Vector3 angles) {
		return new Vector3(
			convertAngles(angles.x),
			convertAngles(angles.y),
			convertAngles(angles.z)
		);
	}

	/*
	 * Converts an angle measured in degrees to a unit Vector3.
	 */
	public static Vector3 degreesToVector3(float degrees) {
		float angleRadians;

		angleRadians = Mathf.Deg2Rad * convertAngles(degrees);

		return new Vector3(Mathf.Cos(angleRadians),
				Mathf.Sin(angleRadians), 0.00f).normalized;
	}

	/*
	 * Converts an angle measured in degrees to a unit Vector2.
	 */
	public static Vector2 degreesToVector2(float degrees) {
		float angleRadians;

		angleRadians = Mathf.Deg2Rad * convertAngles(degrees);

		return new Vector2(Mathf.Cos(angleRadians),
				Mathf.Sin(angleRadians)).normalized;
	}

	/*
	 * Converts an Vector2 into an angle measured in degrees.
	 */
	public static float vector2ToDegrees(Vector2 vector) {
		return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
	}

	/*
	 * Converts an Vector3 into an angle measured in degrees.
	 * Note: disregards the z component.
	 */
	public static float vector3ToDegrees(Vector3 vector) {
		return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
	}



	/*
	 ***********
	 * BITWISE *
	 ***********
	 */

	/*
	 * Returns true if the bitfield has all of the flags.
	 * Can be used with one or more flags.
	 */
	public static bool hasFlag(uint bitfield, uint flags) {
		return (bitfield & flags) == flags;
	}



	/*
	 ********
	 * MATH *
	 ********
	 */

	/*
	 * Find and return all factors of the integer.
	 */
	public static Vector2Int[] factorsV2I(int x) {
		Vector2Int[] factors;
		int lastCheck;
		int i, j;

		lastCheck = (int)Mathf.Sqrt(x); /* Truncation is intentional. */

		i = 0;

		/* Find the number of factors. */
		for (j = 1; j <= lastCheck; ++j) {
			if (x % j == 0) {
				++i;
			}
		}

		factors = new Vector2Int[i];
		i = 0;

		for (j = 1; j <= lastCheck; ++j) {
			if (x % j == 0) {
				factors[i++] = new Vector2Int(j, x / j);
			}
		}

		return factors;
	}



	/*
	 *************
	 * RESOURCES *
	 *************
	 */

	/*
	 * The sprite for empty inventory slots.
	 */
	public static readonly Sprite SPRITE_INVENTORY_EMPTY
			= Resources.Load<Sprite>(
			"Sprites/Custom/Inventory/Empty/EmptySprite");
}
