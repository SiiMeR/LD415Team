using System.Collections.Generic;
using UnityEngine;

public class GridSingleton : MonoBehaviour
{
	[SerializeField] private GameObject bgTile;
	
	
	public Transform StartPosition;
	public LayerMask LayerMask;
	public Vector2 gridWorldSize;
	public float nodeRadius;
	public float Distance;

	public int BGUnitsPerTile = 4;
	
	public static int width;
	public static int height;
	
	private static GridTile[] grid; // = new GridTile[width * height];

	public List<GridTile> FinalPath;
	private float nodeDiameter;
	private int gridSizeX, gridSizeY;
	
	public static GridTile Get(int col, int row) {
		return grid[row * width + col];
	}

	public static void Set(int col, int row, TileType gridType) {
	//	grid[row * width + col] = gridType;
	}

	public static void Set(Vector2Int colRow, TileType gridType)
	{
	//	grid[colRow.y * width + colRow.x] = gridType;
		
	}

	private void Start()
	{
		nodeDiameter = nodeRadius * 2;  
		//gridWorldSize *= nodeDiameter;
		
		
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

		CreateGrid();
	}

	private void CreateGrid()
	{
		grid = new GridTile[gridSizeX *  gridSizeY];

		Vector3 bottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 -
												  Vector3.up * gridWorldSize.y / 2;
		
		
		
		for (var y = 0; y < gridSizeY; y++)
		{
			for (var x = 0; x < gridSizeX; x++)
			{
				Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) +
				                     Vector3.up * (y * nodeDiameter + nodeRadius);

				var wall = !Physics.CheckBox(worldPoint, new Vector3(nodeRadius, nodeRadius, nodeRadius), Quaternion.identity, LayerMask);
				
				grid[y * gridSizeX + x] = new GridTile(wall, worldPoint, x, y);

				if (y % BGUnitsPerTile == 0 && x % BGUnitsPerTile == 0)
				{
					GameObject tile = Instantiate(bgTile, new Vector3(worldPoint.x, worldPoint.y, 0), Quaternion.identity);   
					tile.transform.SetParent(transform);   
				}
				


			}		
		}
	}

	private void OnDrawGizmos()
	{

		Gizmos.DrawWireCube(new Vector3(transform.position.x -1.5f, transform.position.y - 1.5f,0), new Vector3(gridWorldSize.x, gridWorldSize.y,1));
	//	Gizmos.DrawWireCube(bottomLeft, new Vector3(gridWorldSize.x, gridWorldSize.y,1));

		if (grid != null)
		{
			foreach (var node in grid)
			{
				if (node.IsWall)
				{
					Gizmos.color = Color.white;
				}
				else
				{
					Gizmos.color = Color.yellow;
				}

				if (FinalPath != null)
				{
					if (FinalPath.Contains(node))
					{
						Gizmos.color = Color.red;
					}
				}
				
			//	Gizmos.DrawCube(node.Position, Vector3.one * (nodeDiameter - Distance));
			}
		}
	}
}
