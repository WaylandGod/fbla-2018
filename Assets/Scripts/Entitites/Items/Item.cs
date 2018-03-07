using UnityEngine;

/// <summary> The parent class of all items. </summary>
/// <remarks>
/// All items are intangible and self-destruct when touched by the player .
/// </remarks>
public class Item : Entity {

	/// <summary>
	/// Causes this item to self-destruct when touched by the player.
	/// </summary>
	protected override void OnPlayerEnter() {
		Kill (this); }
}

