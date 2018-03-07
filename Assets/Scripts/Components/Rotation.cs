using UnityEngine;

/// <summary> A component that rotates an object on the y-axis. </summary>
public class Rotation : MonoBehaviour {

    [Header ("Rotation Files")]
    /// <summary> The speed at which the object rotates. </summary>
    public float speed = 1.0f;

    /// <summary> Randomizes the object's starting rotation. </summary>
	protected void Awake() {
        float random = Random.Range (0, 360);
        transform.eulerAngles = new Vector3 (0, random, 0);
	}

    /// <summary> Rotates the object on the y-axis at a constant speed. </summary>
	protected void Update() {
        Vector3 constant = new Vector3 (0, 45, 0);
        Vector3 rotation = speed * constant;
		transform.Rotate (rotation * Time.deltaTime);
	}
}
