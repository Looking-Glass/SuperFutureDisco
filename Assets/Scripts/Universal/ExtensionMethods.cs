using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ExtensionMethods {


	#region transform methods
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Set World X/Y/Z/XY
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public static void SetX(this Transform tf, float newX){
		Vector3 pos = tf.position;
		pos.x = newX;
		tf.position = pos;
	}

	public static void SetY(this Transform tf, float newY){
		Vector3 pos = tf.position;
		pos.y = newY;
		tf.position = pos;
	}

	public static void SetZ(this Transform tf, float newZ){
		Vector3 pos = tf.position;
		pos.z = newZ;
		tf.position = pos;
	}

	public static void SetXY(this Transform tf, float newX, float newY){
		Vector3 pos = tf.position;
		pos.x = newX;
		pos.y = newY;
		tf.position = pos;
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Set Local X/Y/Z/XY
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public static void SetLocalX(this Transform tf, float newX){
		Vector3 pos = tf.localPosition;
		pos.x = newX;
		tf.localPosition = pos;
	}

	public static void SetLocalY(this Transform tf, float newY){
		Vector3 pos = tf.localPosition;
		pos.y = newY;
		tf.localPosition = pos;
	}

	public static void SetLocalZ(this Transform tf, float newZ){
		Vector3 pos = tf.localPosition;
		pos.z = newZ;
		tf.localPosition = pos;
	}

	public static void SetLocalXY(this Transform tf, float newX, float newY){
		Vector3 pos = tf.localPosition;
		pos.x = newX;
		pos.y = newY;
		tf.localPosition = pos;
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Shift X/Y/Z
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public static void ShiftX(this Transform tf, float shiftAmt){
		Vector3 pos = tf.position;
		pos.x += shiftAmt;
		tf.position = pos;
	}

	public static void ShiftY(this Transform tf, float shiftAmt){
		Vector3 pos = tf.position;
		pos.y += shiftAmt;
		tf.position = pos;
	}

	public static void ShiftZ(this Transform tf, float shiftAmt){
		Vector3 pos = tf.position;
		pos.z += shiftAmt;
		tf.position = pos;
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Flip X/Y/Z
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public static void FlipX(this Transform tf){
		Vector3 scale = tf.localScale;
		scale.x *= -1f;
		tf.localScale = scale;
	}

	public static void FlipY(this Transform tf){
		Vector3 scale = tf.localScale;
		scale.y *= -1f;
		tf.localScale = scale;
	}

	public static void FlipZ(this Transform tf){
		Vector3 scale = tf.localScale;
		scale.z *= -1f;
		tf.localScale = scale;
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Rotate X/Y/Z
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public static void RotateX(this Transform tf, float rotAmt){
		Vector3 rot = tf.eulerAngles;
		rot.x += rotAmt;
		tf.eulerAngles = rot;
	}

	public static void RotateY(this Transform tf, float rotAmt){
		Vector3 rot = tf.eulerAngles;
		rot.y += rotAmt;
		tf.eulerAngles = rot;
	}

	public static void RotateZ(this Transform tf, float rotAmt){
		Vector3 rot = tf.eulerAngles;
		rot.z += rotAmt;
		tf.eulerAngles = rot;
	}


	#endregion


	#region list methods
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Shuffle List
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public static void Shuffle<T> (this List<T> list) {
		if (list.Count == 0) {
			throw new System.IndexOutOfRangeException ("Cannot shuffle empty list");
		}
		for (int i = list.Count - 1; i > 0; i--) {
			int rnd = Random.Range (0, i + 1);
			T temp = list [i];
			list [i] = list [rnd];
			list [rnd] = temp;
		}
	}


	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Get Random Element from List
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public static T GetRandomElement<T>(this List<T> l){
		//Fisher-Yates
		int n = Random.Range(0,l.Count-1);
		return l[n];
	}

	#endregion

	#region vector3 methods

	public static Vector3 UnitDirectionVector(this Vector3 vector, float degrees){
		vector.x = Mathf.Cos(degrees * Mathf.Deg2Rad);
		vector.y = Mathf.Sin (degrees * Mathf.Deg2Rad);
		return vector;
	}

	#endregion
}
