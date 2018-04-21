using UnityEngine;

public class GridSingleton : MonoBehaviour {
	public static int width;
	public static int height;
	private static GridTile[] grid = new GridTile[width * height];

	public static GridTile Get(int col, int row) {
		return grid[row * width + col];
	}

	public static void Set(int col, int row, GridTile tileType) {
		grid[row * width + col] = tileType;
	}
}
