using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour {

    Vector2Int lastMoveDirection;
    GridMovement gridMovement;
    PlayerBody nextSnakePiece;
    Vector2Int lastPosition;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        lastPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        gridMovement.MoveDirection = lastMoveDirection;
    }
}
