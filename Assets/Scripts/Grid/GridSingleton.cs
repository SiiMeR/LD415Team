using UnityEngine;

public class GridSingleton : MonoBehaviour {
	public int width;
	public int height;
	private GridTile[] grid;
	
	void Start() {
		grid = new GridTile[width * height];
	}

	public GridTile Get(int col, int row) {
		return grid[row * width + col];
	}

	public void Set(int col, int row, GridTile tileType) {
		grid[row * width + col] = tileType;
	}
}
