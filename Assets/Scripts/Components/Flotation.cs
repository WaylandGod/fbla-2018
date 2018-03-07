using UnityEngine;

/// <summary>
/// A component that moves an object sinusoidally along the y-axis.
/// </summary>
/// <remarks> This component uses the sin function. </remarks>
public class Flotation : MonoBehaviour {

    [Header ("Flotation Fields")]
    /// <summary> The speed at which the object moves. </summary>
    public float speed = 2.0f;
    /// <summary> The peak height of the object relative to its starting position. </summary>
	public float amplitude = 0.15f;
    /// <summary> The initial y-position of the object. </summary>
    private float vertical;
    /// <summary> A random integer between [0, 10).  </summary>
    private int offset;

    /// <summary> Initializes the <b>vertical</b> and <b>offset</b> fields. </summary>
	protected void Awake() {
		vertical = transform.position.y;
		offset = Random.Range (0, 10);
	}

    /// <summary> Moves the object vertically using a sin function. </summary>
	protected void Update() {
        Vector3 position = transform.position;
		position.y = vertical + amplitude * Mathf.Sin (speed * Time.time + offset);
		transform.position = position;
	}
}