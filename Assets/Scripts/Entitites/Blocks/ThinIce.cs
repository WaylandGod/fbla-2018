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
using System.Collections;

/// <summary> A block that shatters when the player walks off of this block. </summary>
public class ThinIce : Block {

	/// <summary> Initiates the shatter animation. </summary>
	protected override void OnPlayerEnter() {
		StartCoroutine ("Shatter");
		isPlatform = false;
	}

	/// <summary> Makes this block fade away. </summary>
	private IEnumerator Shatter() {
		Renderer renderer = GetComponent<Renderer>();
		for (float alpha = 1.0f; alpha > 0.0f; alpha -= 0.1f) {
			Color color = renderer.material.color;
			color.a = alpha;
			renderer.material.color = color;
			yield return null;
		}
	}
}
