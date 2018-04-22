using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GridMovement : MonoBehaviour {
	[SerializeField] private float _tilesPerSecond = 1;
	
	public float TilesPerSecond { get; set; }

	private int counter = 0;
	private int n;
	
	public Vector2 MoveDirection { get; set; }

	private Base baas;

	public List<GridTile> closestPath;

	private Vector3 realLoc;
	
	void Start () {
		n = Mathf.RoundToInt(1 / (Time.fixedDeltaTime * _tilesPerSecond));
		MoveDirection = Vector2.down;
		baas = GameObject.FindGameObjectWithTag("Base").GetComponent<Base>();
		
		closestPath = new List<GridTile>();
		realLoc = transform.position;
		UpdatePaths();
	}

	void FixedUpdate() {
		counter++;
	
		if (counter == n) {
			counter = 0;
			
			UpdatePaths();

			Vector3 startPos = transform.position;
			Vector3 endPos = transform.position + new Vector3(MoveDirection.x, MoveDirection.y);
			realLoc = endPos;
			StartCoroutine(VisualLerp(startPos, endPos, 1 / _tilesPerSecond));
			
			GridTile oldTile = GridSingleton.Instance.Get(startPos);

			oldTile.type = TileType.EMPTY;
			
			GridTile tile = GridSingleton.Instance.Get(endPos);
	
			
			if (tile.type == TileType.SNAKE)
			{
				FindObjectOfType<Head>().DeleteBodyAtPos(transform.position);
			}

			tile.type = TileType.ENEMY;
		}
		
	}

	IEnumerator VisualLerp(Vector3 startPos, Vector3 endPos, float duration) {
		float startTime = Time.time;
		float endTime = startTime + duration;

		while (Time.time < endTime) {
			transform.position = Vector3.Lerp(startPos, endPos, (Time.time - startTime) / duration);
			yield return null;
		}

		transform.position = endPos;
	}

	void UpdatePaths()
	{

		List<GridTile> grid = Pathfinder.Instance.FindPath(realLoc, baas.transform.position);

		if (grid != null)
		{
			closestPath = grid;
		}
		else
		{
			grid = Pathfinder.Instance.FindPath(realLoc, baas.transform.position, true);
			if (grid != null)
			{
				closestPath = grid;
				return;
			}
		}
		
		
		Vector2 dir = closestPath[0].Position - transform.position;
		
		dir = new Vector2(dir.x-1.5f, dir.y-1.5f);

		MoveDirection = dir;
	}

	private void OnDrawGizmos()
	{
		
		
		if (closestPath != null)
		{
			Gizmos.color = Color.red;
			closestPath.ForEach(path => Gizmos.DrawCube(new Vector3(path.Position.x - 1.5f, path.Position.y - 1.5f, 0), new Vector3(0.5f,0.5f,0.5f)));
		}

		
	}


}
