using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;

public class Pathfinder : MonoBehaviour
{

	private GridSingleton grid;

	public Transform startPos;

	public Transform endPos;
	
	 
	// Use this for initialization
	void Awake ()
	{
		grid = GetComponent<GridSingleton>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		FindPath(startPos.position, endPos.position);
	}

	private void FindPath(Vector3 startPos, Vector3 endPos)
	{

		GridTile startTile = grid.TileFromWorldPos(startPos);
		GridTile endTile = grid.TileFromWorldPos(endPos);
		
//		print(endTile.gridX + " " + endTile.gridY);
//		print(startTile.gridX + " " + startTile.gridY);
		
		List<GridTile> openList = new List<GridTile>();
		HashSet<GridTile> closedList = new HashSet<GridTile>();
		
		openList.Add(startTile);

		while (openList.Count > 0)
		{
			GridTile currentTile = openList[0];
			for (int i = 1; i < openList.Count; i++)
			{
				if (openList[i].FCost < currentTile.FCost ||
				    openList[i].FCost == currentTile.FCost && 
				    openList[i].hCost < currentTile.hCost)
				{
					currentTile = openList[i];
				}

			}

			openList.Remove(currentTile);
			closedList.Add(currentTile);

			if (currentTile == endTile)
			{
				GetFinalPath(startTile, endTile);
			}

			foreach (var neighbour in grid.getNeighbours(currentTile))
			{
				if (neighbour.type == TileType.SNAKE || closedList.Contains(neighbour))
				{
					continue;
				
				}

				int movecost = currentTile.gCost + GetManHTDist(currentTile, neighbour);

				if (movecost < neighbour.gCost || !openList.Contains(neighbour))
				{
					neighbour.gCost = movecost;
					neighbour.hCost = GetManHTDist(neighbour, endTile);
					neighbour.Parent = currentTile;

					if (!openList.Contains(neighbour))
					{
						openList.Add(neighbour);
					}
				}
			}
		}
	}

	private int GetManHTDist(GridTile tile1, GridTile tile2)
	{
		int ix = Mathf.Abs(tile1.gridX - tile2.gridX);
		int iy = Mathf.Abs(tile1.gridY - tile2.gridY);

		return ix + iy;
	}

	private void GetFinalPath(GridTile startTile, GridTile endTile)
	{
		List<GridTile> finalPath = new List<GridTile>();
		GridTile currentTile = endTile;

		while (currentTile != startTile)
		{
			finalPath.Add(currentTile);
			currentTile = currentTile.Parent;
			
		}
		
		finalPath.Reverse();

		grid.FinalPath = finalPath;
		
		
//		print(finalPath.Count);
	}
}
