using UnityEngine;

/// <summary> A component that stores all of the entities in a level </summary>
/// <remarks>
/// The World class implements methods that store and retrieve game objects. All active
/// entities in a scene are stored in the World's entity matrix.
/// </remarks>
public class World : MonoBehaviour {

	[Header ("World Fields")]
	/// <summary> A reference to an Air prefab. </summary>
	/// <remarks> The reference is necessary to generate air blocks. </remarks>
	private static GameObject air;
	/// <summary> The folder where generated air blocks will be stored. </summary>
	private static Transform folder;
	/// <summary> A multidimensional array holding all entities in a level. </summary>
	private static Entity[,,] matrix;
	/// <summary>
	/// A value needed to convert between matrix coordinates and World positions.
	/// </summary>
	private static Vector3 offset;
	/// <summary> The dimensions of the entity matrix. </summary>
	private static Vector3 size;

	/// <summary> Finds references to non-static entities. </summary>
	private void Awake() {
		folder = GameObject.Find ("Atmosphere").transform;
		air = (GameObject) Resources.Load ("Air");
		Renderer[] renderers = FindObjectsOfType (typeof (Renderer)) as Renderer[];
		foreach (Renderer renderer in renderers) {
			renderer.enabled = true;
		}
	}

	/// <summary> Initializes the entity matrix. </summary>
	/// <remarks>
	/// The entities matrix includes a two block buffer around the perimeter over the level.
	/// This buffer is filled with air blocks, and is used to prevent IndexOutOfRange errors.
	/// </remarks>
	private void Start() {
		Vector3 maximum = new Vector3 (0, 0, 0);
		Vector3 minimum = new Vector3 (0, 0, 0);
		// Calculates the maximum and minimum position components of the World.
		Entity[] entities = FindObjectsOfType (typeof (Entity)) as Entity[];
		foreach (Entity entity in entities) {
			GameObject go = entity.gameObject;
			// Rounds position components to the nearest integer.
			Vector3 position = go.transform.position;
			float x = Mathf.Round (position.x);
			float y = Mathf.Round (position.y);
			float z = Mathf.Round (position.z);
			position = new Vector3 (x, y, z);
			// Checks to see if entity has larger position components than maximum.
			if (position.x > maximum.x) { maximum =
				new Vector3 (position.x, maximum.y, maximum.z); }
			if (position.y > maximum.y) { maximum =
				new Vector3 (maximum.x, position.y, maximum.z); }
			if (position.z > maximum.z) { maximum =
				new Vector3 (maximum.x, maximum.y, position.z); }
			// Checks to see if entity has has smaller position components than minimum.
			if (position.x < minimum.x) { minimum =
				new Vector3 (position.x, minimum.y, minimum.z); }
			if (position.y < minimum.y) { minimum =
				new Vector3 (minimum.x, position.y, minimum.z); }
			if (position.z < minimum.z) { minimum =
				new Vector3 (minimum.x, minimum.y, position.z); }
		}
		// Sets the offset needed to convert between matrix coordinates and World positions.
		offset = new Vector3 (1, 1, 1) - minimum;
		// Initializes size as the dimensions of the level plus a one block buffer.
		size = maximum - minimum + new Vector3 (7, 7, 7);
		matrix = new Entity[(int)size.x, (int)size.y, (int)size.z];
		Recalculate();
	}

	/// <summary> Retrieves the entity located at the given coordinates. </summary>
	/// <param name="coordinates"> The matrix coordinates of an entity </param>
	/// <returns> The entity stored at the given coordinates. </returns>
	public static Entity Get (Vector3 coordinates) {
		return matrix[(int)coordinates.x, (int)coordinates.y, (int)coordinates.z];
	}

	/// <summary> Places an entity in the matrix at the given coordinates. </summary>
	/// <param name="coordinates"> The matrix coordinates where the entity will be placed. </param>
	/// <param name="entity"> The entity to be placed in the matrix. </param>
	private static void Set (Vector3 coordinates, Entity entity) {
		matrix[(int)coordinates.x, (int)coordinates.y, (int)coordinates.z] = entity;
	}

	/// <summary>
	/// Populates the matrix with entities and fills any nullspace with air.
	/// </summary>
	public static void Recalculate() {
		Entity[] entities = FindObjectsOfType (typeof (Entity)) as Entity[];
		foreach (Entity entity in entities) {
			GameObject go = entity.gameObject;
			// Rounds position components to the nearest integer.
			Vector3 position = go.transform.position;
			float x = Mathf.Round (position.x);
			float y = Mathf.Round (position.y);
			float z = Mathf.Round (position.z);
			position = new Vector3 (x, y, z);
			// Assigns coordinates and places the entity in the matrix.
			entity.coordinates = position + offset;
			Set (entity.coordinates, entity);
		}
		// Iterates through each element in the matrix.
		for (int i = 0; i < size.x; i++) {
			for (int j = 0; j < size.y; j++) {
				for (int k = 0; k < size.z; k++) {
					if (matrix[i,j,k] == null) {
						// Generates an air block to fill the empty space.
						Vector3 position = new Vector3 (i, j, k) - offset;
						Quaternion rotation = Quaternion.identity;
						GameObject go = Instantiate (air, position, rotation, folder);
						matrix[i, j, k] = go.GetComponent<Air>();
					}
				}
			}
		}
	}
}
