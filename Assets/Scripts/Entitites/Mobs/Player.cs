using UnityEngine;

/// <summary> The character that the player controls. </summary>
public class Player : Mob {

    [Header ("Player Dependencies")]
    ///<summary> A referemce necessary to determine appropriate inputs. </summary>
    [SerializeField] private InputHandler inputHandler;
    ///<summary> A referemce to the player's animator component. </summary>
    [SerializeField] private Animator animator;

    [Header ("Player Fields")]
    /// <summary> The direction the player should be facing. </summary>
    private float targetRotation;
    /// <summary> A temporary variable used to smoothly rotate the player. </summary>
    private float velocity = 0.0f;

    /// <summary>
    /// Checks if the player has pressed an arrow key and updates the player's rotation.
    /// </summary>
    private void Update() {
        if (GetArrowKeyDown()) { InitiateMovement(); }
        if (this.isEnabled) UpdateRotation();
    }

    /// <summary>
    /// Checks if an arrow key has been pressed.
    /// </summary>
    private bool GetArrowKeyDown() {
        bool a = Input.GetKeyDown (KeyCode.UpArrow);
        bool b = Input.GetKeyDown (KeyCode.DownArrow);
        bool c = Input.GetKeyDown (KeyCode.RightArrow);
        bool d = Input.GetKeyDown (KeyCode.LeftArrow);
        return a || b || c || d;
    }

    /// <summary> Initiates movement if a move is legal. </summary>
    private void InitiateMovement() {
        if (this.hasTicked) {
            direction = inputHandler.GetInput();
            targetRotation = inputHandler.GetRotation();
        }
        if (MoveIsValid()) {
            animator.Play ("Walk Animation");
            StartCoroutine ("Move");
        }
     }

     /// <summary> Updates the player's rotation on the y-axis. </summary>
     private void UpdateRotation() {
         float a = transform.eulerAngles.y;
         float b = targetRotation;
         float delta = 0.025f;
         float y = Mathf.SmoothDampAngle (a, b, ref velocity, delta);
         transform.rotation = Quaternion.Euler (0, y, 0);
     }

     /// <summary> Marks that this crate's movement is complete. </summary>
     /// <remarks>
     /// If the player moves onto ice, the player will continue to move until he or she  is no
     /// longer on ice.
     /// </remarks>
     protected override void OnMoveExit() {
        base.OnMoveExit();
        if (this.isSliding()) {
			StartCoroutine ("Move");
			hasTicked = false;
		}
    }

    /// <summary> Returns true if and only if this crate should slide. </summary>
    /// <returns> True if and only if this crate should slide. </returns>
    protected bool isSliding() {
		Vector3 target = coordinates + direction;
		bool a =  World.Get (coordinates + Vector3.down) is Ice;
		bool b =  World.Get (target).isIntangible ||  World.Get (target) is Crate ||  World.Get (target) is Player;
		bool c =  World.Get (target) is Crate ?  World.Get (target + direction).isIntangible : true;
		return a && b && c;
	}

    /// <summary> Returns true if and only the player cannot legally move. </summary>
    /// <returns> True if and only the player cannot legally move. </returns>
    protected bool isStuck() {
		bool a = (World.Get (coordinates + Vector3.down + Vector3.right) as Block).isPlatform;
		bool b = (World.Get (coordinates + Vector3.down + Vector3.left) as Block).isPlatform;
		bool c = (World.Get (coordinates + Vector3.down + Vector3.forward) as Block).isPlatform;
		bool d = (World.Get (coordinates + Vector3.down + Vector3.back) as Block).isPlatform;
		return !a && !b && !c && !d;
	}
}
