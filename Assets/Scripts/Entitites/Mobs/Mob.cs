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

/// <summary> The parent class of all mobs. </summary>
/// <remarks> Mobs can move and are often dangerous to the player. </remarks>
public class Mob : Entity {

	[Header ("Mob Fields")]
	/// <summary> The speed at which the mob moves. </summary>
	public float speed = 25f;

	/// <summary> The direction that the crate is moving. </summary>
	/// <remarks> The default value is the zero-vector. </remarks>
	public Vector3 direction;

	/// <summary> Calls methods involved with this mob's movement </summary>
	protected IEnumerator Move() {
		OnMoveEnter();
		yield return StartCoroutine ("Movement");
		OnMoveExit();
	}

	/// <summary> Marks that this mob's movement is incomplete. </summary>
	/// <remarks>
	/// Sets hasTicked to false, indicating that this mob is mid-movement.
	/// </remarks>
	/// <see cref="Entity.hasTicked"/>
	protected virtual void OnMoveEnter() {
		hasTicked = false;
	}

	/// <summary> Gradually moves this mob in the specified direction. </summary>
	protected IEnumerator Movement() {
		Vector3 start = transform.position;
        Vector3 finish = transform.position + direction;
        float delta = 0;
        while (delta < 1) {
            delta += speed * Time.deltaTime;
            transform.position = Vector3.Lerp (start, finish, delta);
            yield return null;
        }
	}

	/// <summary> Marks that this crate's movement is complete. </summary>
	/// <remarks>
	/// The position of all non-static entities is recalculated when a mob stops moving.
	/// </remarks>
	protected virtual void OnMoveExit() {
		World.Recalculate();
		hasTicked = true;
	}

	/// <summary> Checks if this mob can move in the given direction. </summary>
	/// <remarks>
	/// This method checks three conditions: one, that there's nothing obstructing this mob's
	/// movement; two, that the block ahead can be stepped on; and three, that the mob
	/// is not moving diagonally.
	/// </remarks>
	/// <returns> True if and only if a move is valid.
	/// <see cref="direction"/>
	protected virtual bool MoveIsValid() {
		Vector3 target = coordinates + direction;
		bool a = World.Get (target).isIntangible ||  World.Get (target) is Crate ||  World.Get (target) is Player;
		bool b = (World.Get (target + Vector3.down) as Block).isPlatform;
		bool c =  World.Get (target) is Crate ?  World.Get (target + direction).isIntangible : true;
		bool d = Mathf.Approximately (direction.magnitude, 1.0f);
		return a && b && c && d;
	}
}
