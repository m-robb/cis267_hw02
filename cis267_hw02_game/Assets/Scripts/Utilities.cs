using UnityEngine;

/*
 * Miscellaneous utilities.
 */
public static class Utilities {
	/*
	 *************
	 * CONSTANTS *
	 *************
	 */
	public const string AXIS_X = "Horizontal";
	public const string AXIS_Y = "Vertical";

	/* TAGS */
	public const string TAG_PLAYER = "PLAYER";
	public const string TAG_ENEMY = "ENEMY";
	public const string TAG_WEAPON = "WEAPON";


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
	 *********
	 * FLOAT *
	 *********
	 */

	/*
	 * Converts from degrees (where 0.00f points to the right) to something
	 * 		Unity understands.
	 */
	public static float convertAngles(float degrees) {
		return ((degrees + 180.00f) % 360.00f) - 180.00f;
	}
}
