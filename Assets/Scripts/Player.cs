using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // Initial position: x = 0; y = 0;

    Vector2Int lastMoveDirection;
    GridMovement gridMovement;
    PlayerBody nextSnakePiece;
    Vector2Int lastPosition;
 
    // Use this for initialization
    void Start () {   
        lastMoveDirection = Vector2Int.up;
        gridMovement = GetComponent<GridMovement>();
        nextSnakePiece = GetComponent<PlayerBody>();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            lastMoveDirection = Vector2Int.right;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            lastMoveDirection = Vector2Int.left;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            lastMoveDirection = Vector2Int.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            lastMoveDirection = Vector2Int.down;
        }   
    }

    private void FixedUpdate()
    {
        //transform.Translate(new Vector3(lastMoveDirection.x, lastMoveDirection.y, 0) * Time.fixedDeltaTime);  
        lastPosition = new Vector2Int((int) transform.position.x, (int) transform.position.y);
        gridMovement.MoveDirection = lastMoveDirection;
    }



}
