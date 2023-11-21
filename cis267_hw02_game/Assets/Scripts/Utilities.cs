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
	/* public const string AXIS_Z = ""; */


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
	 *********
	 * FLOAT *
	 *********
	 */

	public const float rtdc = 180.00f / Mathf.PI;
	public const float dtrc = Mathf.PI / 180.00f;

	/*
	 * Convert radians to degrees.
	 */
	public static float rtd(float radians) {
		return radians * rtdc;
	}

	/*
	 * Convert degrees to radians.
	 */
	public static float dtr(float degrees) {
		return degrees * dtrc;
	}
}
