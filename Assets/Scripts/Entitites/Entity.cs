using UnityEngine;

 /// <summary> The parent class of all game objects. </summary>
 /// <remarks>
/// Entities are the most basic form of a game object; blocks, items, and mobs all derive
/// from Entity. This class defines behaviors that are common across all entities.
/// </remarks>
public class Entity : MonoBehaviour {

	[Header ("Entity Fields")]
	/// <summary> True only if the player can move through this entity. </summary>
	public bool isIntangible = false;

	/// <summary> True if and only if this entity can be destroyed. </summary>
	/// <see cref="Entity.Kill"/>
	public bool isDestructible = false;

	/// <summary>
	/// An overridable method that is called when the player starts moving.
	/// </summary>
	public virtual void Tick() { }

	/// <summary>
	/// An overridable method that is called when the player stops moving.
	/// </summary>
	public virtual void LateTick() { }

	/// <summary>
	/// An overridable method that is called when this entity enters the player's collider.
	/// </summary>
	/// <remarks>
	/// This method is also called when the player enters this entity's collider.
	/// </remarks>
	protected virtual void OnPlayerEnter() { }

	/// <summary>
	/// An overridable method that is called when this entity exits the player's collider.
	/// </summary>
	/// <remarks>
	/// This method is also called when the player exits this entity's collider.
	/// </remarks>
	protected virtual void OnPlayerExit() { }

	/// <summary>
	/// An overridable method that is called when this entity enters a mob's collider.
	/// </summary>
	/// <remarks>
	/// This method is also called when a mob enters this entity's collider.
	/// </remarks>
	/// <param name="mob"> The mob that entered this entity's collider </param>
	protected virtual void OnMobEnter (Mob mob) { }

	/// <summary>
	/// An overridable method that is called when this entity exits a mob's collider.
	/// </summary>
	/// <remarks>
	/// This method is also called when a mob exits this entity's collider.
	/// </remarks>
	/// <param name="mob"> The mob that exited this entity's collider </param>
	protected virtual void OnMobExit (Mob mob) { }

	/// <summary>
	/// An overridable method that is called when this entity enters a block's collider.
	/// </summary>
	/// <remarks>
	/// This method is also called when a block enters this entity's collider.
	/// </remarks>
	/// <param name="mob"> The block that entered this entity's collider </param>
	protected virtual void OnBlockEnter (Block block) { }

	/// <summary>
	/// An overridable method that is called when this entity exits a block's collider.
	/// </summary>
	/// <remarks>
	/// This method is also called when a block exits this entity's collider.
	/// </remarks>
	/// <param name="mob"> The block that exited this entity's collider </param>
	protected virtual void OnBlockExit (Block block) { }

	/// <summary> This entity's position in the World matrix. </summary>
	/// <see cref="World.Recalculate"/>
	[HideInInspector] public Vector3 coordinates = Vector3.zero;

	/// <summary>
	/// True if and only if this entity's Tick() and LateTick() methods have executed.
	/// </summary>
	/// <see cref="Entity.Tick"/>
	/// <see cref="Entity.LateTick"/>
	[HideInInspector] public bool hasTicked = true;

	/// <summary>
	/// True if and only if this entity's Tick() and LateTick() methods are allowed to execute.
	/// </summary>
	/// <see cref="Entity.Tick"/>
	/// <see cref="Entity.LateTick"/>
	[HideInInspector] public bool isEnabled = true;

	/// <summary>
	/// True if and only if this entity's colliders in inside the player's collider.
	/// </summary>
	/// <see cref="Entity.OnTriggerEnter"/>
	[HideInInspector] private bool isTouchingPlayer = false;

	/// <summary> Calls collision-related methods. </summary>
	/// <param name="collider"> An entity's collider </param>
	/// <remarks>
	/// This method is called when this entity's collider enters any collider.
	/// </remarks>
	protected void OnTriggerEnter (Collider collider) {
		if (collider.name == "Player") {
			isTouchingPlayer = true;
			OnPlayerEnter();
		}
		if (collider.tag == "Mob") {
			Mob mob = GetEntity<Mob> (collider);
			OnMobEnter (mob);
		}
		if (collider.tag == "Block") {
			Block block = GetEntity<Block> (collider);
			OnBlockEnter (block);
		}
	}

	/// <summary> Calls collision-related methods. </summary>
	/// <param name="collider"> An entity's collider </param>
	/// <remarks>
	/// This method is called when this entity's collider exits any collider.
	/// </remarks>
	protected void OnTriggerExit (Collider collider) {
		if (collider.name == "Player") {
			isTouchingPlayer = false;
			OnPlayerExit();
		}
		if (collider.tag == "Mob") {
			Mob mob = GetEntity<Mob> (collider);
			OnMobExit (mob);
		}
		if (collider.tag == "Block") {
			Block block = GetEntity<Block> (collider);
			OnBlockExit (block);
		}
	}

	/// <summary> Returns the entity associated with a given collider. </summary>
	/// <param name="collider"> An entity's collider </param>
	/// <returns> Returns the entity associated with the given collider </return>
	private T GetEntity<T> (Collider collider) {
		GameObject go = collider.gameObject;
		T entity = go.GetComponent<T>();
		return entity;
	}

	/// <summary> Calls the OnDeath method of an entity. </summary>
	/// <param name="entity"> The entity that will be killed.  </param>
	/// <see cref="Entity.OnDeath"/>
	protected virtual void Kill (Entity entity) {
		entity.OnDeath();
	}

	/// <summary>
	/// An overridable method that is called when an entity is killed.
	/// </summary>
	/// <see cref="Entity.Kill"/>
	public virtual void OnDeath() {
		// By default, the entity self-destructs.
		Destroy (gameObject);
	}
}
