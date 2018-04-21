using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;

public class GridTile {
	
	public int gridX;
	public int gridY;

	public bool IsWall;
	public Vector3 Position;
	
	public GridTile Parent;

	public int gCost;
	public int hCost;

	public int FCost {
		get { return gCost + hCost; }

	}

	public GridTile(bool isWall, Vector3 pos, int gridX, int gridY)
	{
		this.IsWall = isWall;
		Position = pos;
		this.gridX = gridX;
		this.gridY = gridY;
		
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
