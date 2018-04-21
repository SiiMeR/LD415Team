using UnityEngine;

public class Base : MonoBehaviour {
	public int hp;
	public int x, y;

	void Start() {
		GridSingleton.Set(x, y, GridTile.BASE);
	}
}
