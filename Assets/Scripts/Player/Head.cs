using UnityEngine;

public class Head : MonoBehaviour {
	public float tilesPerSecond = 1;
	public Body neck;

	int counter = 0;
	int n;
	Vector2 moveDirection = Vector2.right;

	void Start() {
		n = Mathf.RoundToInt(1 / (Time.fixedDeltaTime * tilesPerSecond));
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
		}
		if (Input.GetButtonDown("Down") && lastDirection != Direction.up) {
			direction = Direction.down;
			moveDirection = Vector2.down;
		}
		if (Input.GetButtonDown("Left") && lastDirection != Direction.right) {
			direction = Direction.left;
			moveDirection = Vector2.left;
		}
		if (Input.GetButtonDown("Right") && lastDirection != Direction.left) {
			direction = Direction.right;
			moveDirection = Vector2.right;
		}
	}

	void FixedUpdate() {
		counter++;

		if (counter == n) {
			counter = 0;
			if (neck != null) {
				neck.Move(transform.position);
			}
			transform.Translate(moveDirection);
			lastDirection = direction;
		}		
	}
}
