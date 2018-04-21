using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile {
	
	public int gridX;
	public int gridY;

	public bool isWall;
	public Vector3 Position;
	
	public GridTile Parent;

	public int gCost;
	public int hCost;

	public TileType type;

	public int FCost {
		get { return gCost + hCost; }

	}

	public GridTile(bool isWall, Vector3 pos, int gridX, int gridY, TileType type)
	{
		this.type = type;
		this.isWall = isWall;
		Position = pos;
		this.gridX = gridX;
		this.gridY = gridY;
		
	}
}
