using UnityEngine;

/// <summary> A block that transports the player to a different level. </summary>
public class Exit : Block {

	[Header ("Exit Fields")]
	/// <summary> The level that the player will travel to. </summary>
	public Object scene;
}
