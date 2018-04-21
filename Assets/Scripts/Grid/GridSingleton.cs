using System.Collections.Generic;
using UnityEngine;

public class GridSingleton : Singleton<GridSingleton>
{
	[SerializeField] private GameObject bgTile;
	
	public Transform StartPosition;
	public LayerMask LayerMask;
	public Vector2Int gridWorldSize;
	public float nodeRadius;
	public float Distance;

	public int BGUnitsPerTile = 4;
	
	private GridTile[] grid; // = new GridTile[width * height];

	public List<GridTile> FinalPath;
	private float nodeDiameter;
	private int gridSizeX, gridSizeY;

	private float offset = 1.5f;
	
	public GridTile Get(int col, int row) {
		return grid[row * gridSizeX + col];
	}

	public void Set(int col, int row, TileType type) {
		grid[row * gridSizeX + col].type = type;
	}

	public void Set(Vector2Int colRow, TileType type)
	{

		grid[colRow.y * gridSizeX + colRow.x].type = type;	
	}
	
	
	private void Awake()
	{
		nodeDiameter = nodeRadius * 2;  
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
		
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
		
		grid = new GridTile[gridSizeX *  gridSizeY];
		
		CreateGrid();
	}

	private void CreateGrid()
	{
		

		Vector3 bottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 -
												  Vector3.up * gridWorldSize.y / 2;
		
		
		
		for (var y = 0; y < gridSizeY; y++)
		{
			for (var x = 0; x < gridSizeX; x++)
			{
				Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) +
				                     Vector3.up * (y * nodeDiameter + nodeRadius);

				//var wall = !Physics.CheckBox(worldPoint, new Vector3(nodeRadius, nodeRadius, nodeRadius), Quaternion.identity, LayerMask);
				
				grid[y * gridSizeX + x] = new GridTile(worldPoint, x, y, TileType.EMPTY);
				
				grid[y * gridSizeX + x] = new GridTile(worldPoint, x, y, TileType.EMPTY);
				if (y % BGUnitsPerTile == 0 && x % BGUnitsPerTile == 0)
				{
					
					GameObject tile = Instantiate(bgTile, new Vector3(worldPoint.x, worldPoint.y, 0), Quaternion.identity);   
					tile.transform.SetParent(transform);   
				
				}
				else
				{
					
				
				}
				


			}		
		}
	}

	private void OnDrawGizmos()
	{

		Gizmos.DrawWireCube(new Vector3(transform.position.x - offset, transform.position.y - offset,0), new Vector3(gridWorldSize.x, gridWorldSize.y,1));

		if (grid != null)
		{
			foreach (var node in grid)
			{
				if (node.type == TileType.SNAKE)
				{
					Gizmos.color = Color.white;
				}
				else if(node.type == TileType.BASE)
				{
					Gizmos.color = Color.magenta;
				}
				else if (node.type == TileType.ENEMY)
				{
					Gizmos.color = Color.cyan;
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


				Color c = Gizmos.color;
				c.a = 0.99f;
				Gizmos.color = c;
				Gizmos.DrawCube(new Vector3(node.Position.x -offset, node.Position.y -offset), new Vector3(0.7f,0.7f,0.7f) * (nodeDiameter - Distance));
			}
		}
	}
}
