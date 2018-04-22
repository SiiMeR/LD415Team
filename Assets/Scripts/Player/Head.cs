using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
		for (int i = 0; i < 2; i++) {
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
			Move();
		}		
	}

	private void CheckEdible(int x, int y) {
		if (GridSingleton.Instance.Get(x, y).type == TileType.PICKUP) {
			Grow();
		}
	}

	private void Move() {

		//Calculate next position
		int newX = (int) (transform.position.x + moveDirection.x);
		int newY = (int) (transform.position.y + moveDirection.y);
		if (newX < 0) {
			newX = GridSingleton.Instance.gridWorldSize.x - 1;
		} else if (newX == GridSingleton.Instance.gridWorldSize.x) {
			newX = 0;
		}
		if (newY < 0) {
			newY = GridSingleton.Instance.gridWorldSize.y - 1;
		} else if (newY == GridSingleton.Instance.gridWorldSize.y) {
			newY = 0;
		}

		//Check if something is in the way
		GridTile tile = GridSingleton.Instance.Get(newX, newY);
		if (tile.type == TileType.BASE || tile.type == TileType.SNAKE)
		{
			StartCoroutine(DIE());

		} else {
			CheckEdible(newX, newY);
			transform.position = new Vector3(newX, newY);
			GridSingleton.Instance.Set(newX, newY, TileType.SNAKE);
			lastDirection = direction;
		}
	}

	private IEnumerator FadeToBlack(float seconds)
	{
		
		
		float timer = 0;

		float perFrameFade = 1 / seconds;
		
		while ((timer += Time.fixedUnscaledDeltaTime) < seconds)
		{
			Color c = GameObject.FindGameObjectWithTag("deathscreen").GetComponent<Image>().color;
			c.a += perFrameFade * Time.fixedUnscaledDeltaTime;
			GameObject.FindGameObjectWithTag("deathscreen").GetComponent<Image>().color = c;

			yield return null;
		}

		GameObject.FindGameObjectWithTag("deathscreen").GetComponentInChildren<Text>(true).enabled = true;
	}
	private IEnumerator DIE()
	{
		print("you died, no implementation tho :DDDDDDDDDDDDDDDDDD, press enter to restart");

		Time.timeScale = 0f;

		StartCoroutine(FadeToBlack(4.0f));
		
		yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
		
		GameObject.FindGameObjectWithTag("deathscreen").GetComponentInChildren<Text>(true).enabled = false;
		Time.timeScale = 1f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		
	//	Application.Quit(); //TODO: BAD STUFF
	}

	public void Grow() {
        if (neck == null) {
            GameObject gameObject = Instantiate(bodyPrefab, transform.position, Quaternion.identity);
            neck = gameObject.GetComponent<Body>();
        } else {
			neck.Grow(bodyPrefab);
		}
    }
	
	public void DeleteBodyAtPos(Vector3 pos)
	{
		neck.DeleteBodyAtPos(pos);
	}
}
