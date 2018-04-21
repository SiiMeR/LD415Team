using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSingleton : Singleton<GridSingleton>
{
	[SerializeField] private GameObject bgTile;
	public List<GameObject> otherTiles;
	public GameObject specialTile;

	public Transform StartPosition;
	
	//public Transform StartPosition;
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

	public void Set(int col, int row, GridTile tile)
	{
		grid[row * gridSizeX + col] = tile;
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
		
		Vector3 bottomLeft = transform.position -
		                     Vector3.right * gridWorldSize.x / 2 -
							 Vector3.up * gridWorldSize.y / 2;
		
		
		
		for (var x = 0; x < gridSizeX; x++)
		{
			for (var y = 0; y < gridSizeY; y++)
			{
				Vector3 worldPoint = bottomLeft +
				                     Vector3.right * (x * nodeDiameter + nodeRadius) +
				                     Vector3.up * (y * nodeDiameter + nodeRadius);

				//var wall = !Physics.CheckBox(worldPoint, new Vector3(nodeRadius, nodeRadius, nodeRadius), Quaternion.identity, LayerMask);
				
		    	GridTile tileempty = new GridTile(worldPoint, x, y, TileType.EMPTY);
				
				Set(x,y, tileempty);
				if (y % BGUnitsPerTile == 0 && x % BGUnitsPerTile == 0)
				{
					float randomNumber = Random.Range(0f, 1f);
					GameObject randomTile = bgTile;
					for (int i = 0; i < otherTiles.Count; i++) {
						if (randomNumber > 1 - 0.1f * (i + 1)) {
							randomTile = otherTiles[i];
							break;
						}
					}
					if (y == gridSizeY - BGUnitsPerTile && x == gridSizeX - BGUnitsPerTile) {
						randomTile = specialTile;
					}
					GameObject tile = Instantiate(randomTile, new Vector3(worldPoint.x, worldPoint.y, 0), Quaternion.identity);   
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

	public GridTile TileFromWorldPos(Vector3 startPos)
	{
		float xpoint = ((startPos.x + (float)gridWorldSize.x/ 2) / (float)gridWorldSize.x);

		float ypoint = ((startPos.y+ (float)gridWorldSize.y/ 2) / (float)gridWorldSize.y);

	
		
//		print(xpoint + " ss " + ypoint);
		xpoint = Mathf.Clamp01(xpoint);
		ypoint = Mathf.Clamp01(ypoint);

		int x = Mathf.RoundToInt((gridSizeX - 1) * xpoint);
		int y = Mathf.RoundToInt((gridSizeY - 1) * ypoint);

		return Get(x, y);

	}

	public List<GridTile> getNeighbours(GridTile currentTile)
	{
		List<GridTile> neighbours = new List<GridTile>();

		int xCheck;
		int yCheck;

		
		// right nb
		xCheck = currentTile.gridX + 1;
		yCheck = currentTile.gridY;

		if (xCheck >= 0 && xCheck < gridSizeX)
		{
			if (yCheck >= 0 && yCheck < gridSizeY)
			{
				neighbours.Add(Get(xCheck, yCheck));
			}
		}
		
		// left nb
		xCheck = currentTile.gridX - 1;
		yCheck = currentTile.gridY;

		if (xCheck >= 0 && xCheck < gridSizeX)
		{
			if (yCheck >= 0 && yCheck < gridSizeY)
			{
				neighbours.Add(Get(xCheck, yCheck));
			}
		}

		
		// top nb
		xCheck = currentTile.gridX;
		yCheck = currentTile.gridY + 1;

		if (xCheck >= 0 && xCheck < gridSizeX)
		{
			if (yCheck >= 0 && yCheck < gridSizeY)
			{
				neighbours.Add(Get(xCheck, yCheck));
			}
		}
		
		
		// bot nb
		xCheck = currentTile.gridX;
		yCheck = currentTile.gridY -1;

		if (xCheck >= 0 && xCheck < gridSizeX)
		{
			if (yCheck >= 0 && yCheck < gridSizeY)
			{
				neighbours.Add(Get(xCheck, yCheck));
			}
		}

		return neighbours;
	}
}
