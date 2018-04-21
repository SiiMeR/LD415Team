using UnityEngine;

public class Body : MonoBehaviour {
	public Body next;

	public void Move(Vector3 pos) {
		if (next != null) {
			next.Move(transform.position);
		}
        else
        {
            GridSingleton.Set(new Vector2Int((int)transform.position.x, (int)transform.position.y), TileType.EMPTY);
        }
		transform.position = pos;
	}

	public void Grow(GameObject bodyPrefab) {
		if (next == null) {
			GameObject gameObject = Instantiate(bodyPrefab, transform.position, Quaternion.identity);
			next = gameObject.GetComponent<Body>();
		} else {
			next.Grow(bodyPrefab);
		}
	}
}
