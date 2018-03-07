using UnityEngine;
using System;

/// <summary> A helper class that is used to determine player inputs </summary>
public class InputHandler : MonoBehaviour {

    /// <summary> The designated up button. </summary>
    public readonly KeyCode up = KeyCode.UpArrow;
    /// <summary> The designated down button. </summary>
    public readonly KeyCode down = KeyCode.DownArrow;
    /// <summary> The designated right button. </summary>
    public readonly KeyCode right = KeyCode.RightArrow;
    /// <summary> The designated left button. </summary>
    public readonly KeyCode left = KeyCode.LeftArrow;

    /// <summary>
    /// Determines which player the should move based on the user's keyboard input.
    /// </summary>
    public Vector3 GetInput() {
        if (Input.GetKey (up)) {
            return new Vector3 (0, 0, 1);
        } else if (Input.GetKey (down)) {
            return new Vector3 (0, 0, -1);
        } else if (Input.GetKey (right)) {
            return new Vector3 (1, 0, 0);
        } else if (Input.GetKey (left)) {
            return new Vector3 (-1, 0, 0);
        } else {
            throw new ArgumentException();
        }
    }

    /// <summary>
    /// Determines which player the rotate based on the user's keyboard input.
    /// </summary>
    public float GetRotation() {
        if (Input.GetKey (up)) {
            return 270;
        } else if (Input.GetKey (down)) {
            return 90;
        } else if (Input.GetKey (right)) {
            return 0;
        } else if (Input.GetKey (left)) {
            return 180;
        } else {
            throw new ArgumentException();
        }
    }
}
