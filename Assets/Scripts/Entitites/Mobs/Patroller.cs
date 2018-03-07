using UnityEngine;
using System.Collections;

/// <summary> A basic enemy that moves back and forth. </summary>
public class Patroller : Mob {

    /// <summary> Reverses this mob's direction. </summary>
    public override void LateTick() {
        if (MoveIsValid()) {
            StartCoroutine ("Move");
        } else {
            direction *= -1;
            StartCoroutine ("Move");
        }
    }
}
