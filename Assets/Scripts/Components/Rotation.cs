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
