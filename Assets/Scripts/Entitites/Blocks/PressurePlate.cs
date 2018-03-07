using UnityEngine;
using System;

[Obsolete ("This class is deprecated, and will be refactored in the next version.")]
/// <summary>
/// This class is deprecated, and will be refactored in the next version.
/// </summary>
public class PressurePlate : Block {

	public enum Action {Summon, Reset};

	[Header ("Pressure Plate Attributes")]
	public Action action;
	public GameObject go;
	public Vector3 position;

	[HideInInspector]
	public GameObject summon;

	[Header ("Instantiation Attributes")]
	public Vector3 direction = new Vector3 (0,0,0);
	public int turns = 1;
	public GameObject model;
	public Material first;
	public Material second;
	private bool mobSummoned = false;
	private Vector3 velocity = Vector3.zero;
	public GameObject cube;

	private bool risen = true;
	private Renderer fiesta;
	private void OnEnable() {
		fiesta = cube.GetComponent<Renderer>();
	}

	private Vector3 target = Vector3.zero;

	private void Update() {
		 matrix.Set (coordinates, this);
		if (TickManager.GetTickComplete()) {
			if (isTouchingPlayer && summon == null) {
				SoundEffectsManager.Play ("Snap");
				Instantiate (go);
				fiesta.material = second;
				isTouchingPlayer = false;
			}
		}
		if (summon) {
			target = new Vector3 (0, -0.09f, 0);
		} else if (fiesta.material = second) {
			fiesta.material = first;
			target = Vector3.zero;
		} else {
			target = Vector3.zero;
		}
		model.transform.localPosition = Vector3.SmoothDamp (model.transform.localPosition, target, ref velocity, 0.05f);
	}

	protected override void OnPlayerEnter() {
		fiesta.material = second;
		switch (action) {
			case Action.Summon:
				if (summon == null) {
					Instantiate (go);
				}
		}
	}

	private void Instantiate (GameObject go) {
		mobSummoned = false;
		if ( matrix.Get (coordinates) is PressurePlate) {
		} else {
			 matrix.Destroy ( matrix.Get (coordinates));
		}
		Quaternion rotation = Quaternion.identity;
		Transform parent = GetParent (go);
		summon = Instantiate (go, position, rotation, parent);
		summon.name = go.name;
		Entity entity = go.GetComponent<Entity> ();
	}

	public override void Tick() {
		if (mobSummoned) {
			TickManager.Recalculate ();
			mobSummoned = false;
		}
	}
}
