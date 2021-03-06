﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile {
	
	public int gridX;
	public int gridY;

	public Vector3 Position;
	
	public GridTile Parent;

	public int gCost;
	public int hCost;

	public TileType type = TileType.EMPTY;

	public int FCost {
		get { return gCost + hCost; }

	}

	public GridTile( Vector3 pos, int gridX, int gridY, TileType type)
	{
		this.type = type;
		Position = pos;
		this.gridX = gridX;
		this.gridY = gridY;
		
	}
}
