using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class 	GridSingleton : Singleton<GridSingleton>
{
	[SerializeField] private GameObject bgTile;
	public List<GameObject> otherTiles;
	public GameObject specialTile;

	
	//public Transform StartPosition;
	public LayerMask LayerMask;
	public Vector2Int gridWorldSize;
	public float nodeRadius;
	public float Distance;
	public float foodSpawnDelay;
	public GameObject foodPrefab;

	public int BGUnitsPerTile = 4;
	
	private GridTile[] grid; // = new GridTile[width * height];

	public List<GridTile> FinalPath;
	private float nodeDiameter;
	private int gridSizeX, gridSizeY;

	private float offset = 1.5f;
	
	public GridTile Get(int col, int row) {
		return grid[row * gridSizeX + col];
	}

	public GridTile Get(Vector3 position)
	{
		return Get((int) position.x, (int) position.y);
	}

	public void Empty(Vector3 position)
	{
		Get(position).type = TileType.EMPTY;
	}
	
	public void Set(int col, int row, TileType type) {
		grid[row * gridSizeX + col].type = type;
	}

	public void Set(Vector2Int colRow, TileType type)
	{
		
		grid[colRow.y * gridSizeX + colRow.x].type = type;	
	}

	public void Set(Vector2Int colROw, TileType type, Vector3 size)
	{
		int bottomX = (int) (colROw.x - size.x / 2);
		int bottomY = (int) (colROw.y - size.y / 2);
		
	//	Vector2Int bottomLeft = new Vector2Int(bottomX, bottomY);

		for (int x = bottomX + 1; x < bottomX + size.x + 1; x++)
		{	
			for (int y = bottomY + 1; y < bottomY + size.y + 1; y++)
			{
				Set(new Vector2Int(x,y), type);
			}
		}
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

	private void Start() {
		StartCoroutine(SpawnFood());
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

				if (node.type == TileType.EMPTY)
				{
					Gizmos.color = Color.yellow;
				}
				if (node.type == TileType.SNAKE)
				{
					Gizmos.color = Color.white;
				}

				else if (node.type == TileType.ENEMY)
				{
					Gizmos.color = Color.cyan;
				}
				else if(node.type == TileType.BASE)
				{
					Gizmos.color = Color.magenta;
				}
				else
				{
				//	
				}



		

				Color c = Gizmos.color;
				c.a = 0.99f;
				Gizmos.color = c;
				Gizmos.DrawCube(new Vector3(node.Position.x -offset, node.Position.y -offset), new Vector3(0.7f,0.7f,0.7f) * (nodeDiameter - Distance));
				
			}
		}
	}




	public List<GridTile> GetNeighbours(GridTile currentTile)
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

	IEnumerator SpawnFood() {
		while (true) {
			yield return new WaitForSeconds(foodSpawnDelay);
			int x = Random.Range(0, gridSizeX);
			int y = Random.Range(0, gridSizeY);
			if (Get(x, y).type == TileType.EMPTY) {
				Set(x, y, TileType.PICKUP);
				Instantiate(foodPrefab, new Vector3(x, y), Quaternion.identity);
			}
		}
	}
}
