using System.Collections.Generic;

using UnityEngine;

public class GridMovement : MonoBehaviour {
	[SerializeField] private float _tilesPerSecond = 1;
	
	public float TilesPerSecond { get; set; }

	private int counter = 0;
	private int n;
	
	public Vector2 MoveDirection { get; set; }

	private Base baas;
	
	void Start () {
		n = Mathf.RoundToInt(1 / (Time.fixedDeltaTime * _tilesPerSecond));
		MoveDirection = Vector2.down;
		baas = GameObject.FindGameObjectWithTag("Base").GetComponent<Base>();
	}

	void FixedUpdate() {
		counter++;

		List<GridTile> grid = Pathfinder.Instance.FindPath(transform.position, baas.transform.position);

	
		Vector3 ret = new Vector3();
		if (grid != null)
		{
			ret = Pathfinder.Instance.FindPath(transform.position, baas.transform.position)[0].Position;
		}
		
		Vector2 dir = ret - transform.position;
		
		dir = new Vector2(dir.x-1.5f, dir.y-1.5f);

		MoveDirection = dir;
	
		if (counter == n) {
			counter = 0;
			transform.Translate(MoveDirection);
		}
		
	}
}
