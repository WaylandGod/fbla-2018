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
