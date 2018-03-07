using UnityEngine;

/// <summary> The parent class of all blocks. </summary>
/// <remarks> Blocks are used to build levels. </remarks>
public class Block : Entity {

	[Header ("Block Fields")]
	/// <summary> True only if the player can walk onto this block. </summary>
	public bool isPlatform = true;

	/// <summary> Sets the color of this object. </summary>
	/// <remarks> This method should only be used in debug. </remarks>
	public void Highlight (Color color) {
		Renderer[] renderers = GetComponentsInChildren<Renderer>();
		foreach (Renderer renderer in renderers) {
			renderer.material.color = color;
		}
	}
}
