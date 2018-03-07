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

/// <summary> A block that can be moved by the player. </summary>
/// <remarks> The crate class duplicates code found in the mob class. </remarks>
public class Crate : Block {

    [Header ("Crate Fields")]
    /// <summary> The speed at which the crate moves. </summary>
    public float speed = 40.0f;

    /// <summary> The direction that the crate is moving. </summary>
    /// <remarks> The default value is the zero-vector. </remarks>
    private Vector3 direction;

    /// <summary> Pushes the crate in the same direction as a mob.  </summary>
    /// <param name="mob"> The mob that entered this crate's collider. </param>
    /// <remarks>
    /// If the crate is moving and hits another mob, that mob is killed.
    /// </remarks>
    protected override void OnMobEnter (Mob mob) {
        if (hasTicked) {
            direction = mob.direction;
            StartCoroutine ("Move");
        } else {
             Kill (mob);
        }
    }

    /// <summary> Calls methods involved with this crate's movement </summary>
    /// <remarks>
    /// This method is a coroutine, meaning that the OnMoveExit method will not be called
    /// until the Movement animation is complete.
    /// </remarks>
    protected IEnumerator Move() {
        OnMoveEnter();
        yield return StartCoroutine ("Movement");
        OnMoveExit();
    }

    /// <summary> Marks that this crate's movement is incomplete. </summary>
    /// <remarks>
    /// Sets hasTicked to false, indicating that the crate is mid-movement.
    /// </remarks>
    /// <see cref="Entity.hasTicked"/>
    protected virtual void OnMoveEnter() {
        hasTicked = false;
    }

    /// <summary> Gradually moves this crate in the specified direction. </summary>
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
    /// If this crate is moved such that it lands on ice, the crate will continue to move in the
    /// designated direction.
    /// </remarks>
    protected void OnMoveExit() {
        if (this.isSliding()) {
			StartCoroutine ("Move");
        } else {
            hasTicked = true;
        }
	}

    /// <summary> Returns true if and only if this crate should slide. </summary>
    /// <returns> True if and only if this crate should slide. </returns>
    private bool isSliding() {
		Vector3 target = coordinates + direction;
		bool a =  World.Get (coordinates + Vector3.down) is Ice;
		bool b =  World.Get (target).isIntangible ||  World.Get (target) is Crate ||  World.Get (target) is Player;
		bool c =  World.Get (target) is Crate ?  World.Get (target + direction).isIntangible : true;
		return a && b && c;
	}
}
