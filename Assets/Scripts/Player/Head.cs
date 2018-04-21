using UnityEngine;

public class Head : MonoBehaviour {
	public float tilesPerSecond = 1;
	public Body neck;
    public GameObject bodyPrefab;

	int counter = 0;
	int n;
	Vector2 moveDirection = Vector2.up;

	void Start() {
		n = Mathf.RoundToInt(1 / (Time.fixedDeltaTime * tilesPerSecond));
		//TEMPORARY
		for (int i = 0; i < 1; i++) {
			Grow();
		}
	}

	enum Direction {
		up,
		down,
		left,
		right
	}

	Direction lastDirection = Direction.up;
	Direction direction = Direction.up;

	void Update() {
		if (Input.GetButtonDown("Up") && lastDirection != Direction.down) {
			direction = Direction.up;
			moveDirection = Vector2.up;
			transform.rotation = Quaternion.Euler(0, 0, 0);
		}
		if (Input.GetButtonDown("Down") && lastDirection != Direction.up) {
			direction = Direction.down;
			moveDirection = Vector2.down;
			transform.rotation = Quaternion.Euler(0, 0, 180);
		}
		if (Input.GetButtonDown("Left") && lastDirection != Direction.right) {
			direction = Direction.left;
			moveDirection = Vector2.left;
			transform.rotation = Quaternion.Euler(0, 0, 90);
		}
		if (Input.GetButtonDown("Right") && lastDirection != Direction.left) {
			direction = Direction.right;
			moveDirection = Vector2.right;
			transform.rotation = Quaternion.Euler(0, 0, 270);
		}
	}

	void FixedUpdate() {
		counter++;

		if (counter == n) {
			counter = 0;
			if (neck != null) {
				neck.Move(transform.position + new Vector3(0, 0, 0.01f));
			}
            Debug.Log(transform.position.x);
            Debug.Log(transform.position.y);
            Debug.Log(GridSingleton.Instance.Get((int)transform.position.x, (int)transform.position.y));
            Vector2 temp = new Vector2(transform.position.x , transform.position.y) + moveDirection;
            Debug.Log("---");
            Debug.Log(temp);
            //if (GridSingleton.Get(temp). GET_THE_TILE_TYPE_OR_SOMETHING == TileType.SNAKE)
            //{
            //    Debug.Log("Game Over");  
            //}
			transform.Translate(moveDirection, Space.World);
            GridSingleton.Instance.Set(new Vector2Int((int)transform.position.x, (int)transform.position.y), TileType.SNAKE);
            
            lastDirection = direction;
		}		
	}

    public void Grow() {
        if (neck == null) {
            GameObject gameObject = Instantiate(bodyPrefab, transform.position, Quaternion.identity);
            neck = gameObject.GetComponent<Body>();
        } else {
			neck.Grow(bodyPrefab);
		}
    }
}
