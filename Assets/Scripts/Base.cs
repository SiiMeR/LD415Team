using UnityEngine;

public class Base : MonoBehaviour {
	
	public int Hp;

	void Start() {
        Debug.Log("BASE COORDINATES:");
        Debug.Log(transform.position);
		GridSingleton.Set(new Vector2Int((int) transform.position.x, (int) transform.position.y), TileType.BASE);
	}

}
