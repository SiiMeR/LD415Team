using UnityEditorInternal;
using UnityEngine;

public class Base : MonoBehaviour {
	public int hp;

	void Start() {
		GridSingleton.Instance.Set(new Vector2Int((int) transform.position.x, (int) transform.position.y), TileType.BASE);
	}

}
