// Copyright (C) 2018 Bristol Street Studios. All rights reserved.
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.

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