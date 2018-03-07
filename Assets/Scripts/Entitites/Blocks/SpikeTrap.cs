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

/// <summary> A dangerous block found in the Desert biome. </summary>
public class SpikeTrap : Block {

	/// <summary> Possible spike trap behaviors. </summary>
	public enum State {Dormant = 0, Charging = 1, Active = 2}

	[Header ("Spike Trap Fields")]
	/// <summary> The current behavior of the spike trap. </summary>
	/// <remarks>
	/// The spike trap is safe to walk on if and only if it is dormant.
	/// </remarks>
	[SerializeField] private State state = State.Dormant;
	/// <summary> A reference to the spikes model. </summary>
	[SerializeField] private Transform spikes;

	/// <summary> The desired position of the spikes. </summary>
	/// <remarks> The spikes will gradually move towards this position. </remarks>
	private Vector3 target = Vector3.zero;
	/// <summary> A temporary variable necessary to animate spikes. </summary>
	private Vector3 velocity = Vector3.zero;

	/// <summary>
	/// Gradually moves the spikes towards their expected position.
	/// </summary>
	/// <see cref="target"/>
	private void Update() {
		Vector3 current = spikes.localPosition;
		float delta = 0.05f;
		spikes.localPosition = Vector3.SmoothDamp (current, target, ref velocity, delta);
	}

	/// <summary> Changes the spike trap's state. </summary>
	/// <remarks>
	/// If the spike trap loops through the following behaviours, in order: dormant,
	/// charging, and active. The spike trap is hidden when dormant, somewhat visible when
	/// charging, and entirely visible when active.
	/// </remarks>
	public override void Tick() {
		state = state < State.Active ? state += 1 : 0;
		if (state == State.Charging) {
			target = new Vector3 (0, 0.65f, 0);
		} else if (state == State.Dormant) {
			target = new Vector3 (0, 0.35f, 0);
		} else if (state == State.Active) {
			target = new Vector3 (0, 0.55f, 0);
		}
	}

	/// <summary> Checks if the spikes have hit an entity. </summary>
	/// <remarks>
	/// If the spike trap is charging or active, the entity immideatley above this spike trap is
	/// is killed.
	/// </remarks>
	/// <see cref="Entity.Kill"/>
	public override void LateTick() {
		Entity entity =  World.Get (coordinates + Vector3.up);
		bool isDeadly = (state == State.Active) || (state == State.Charging);
		if (isDeadly && entity.isDestructible) { Kill (entity); }
	}
}
