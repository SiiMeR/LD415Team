using UnityEngine;

public class Body : MonoBehaviour {
	public Body next;

	public void Move(Vector3 pos) {
		if (next != null) {
            next.Move(transform.position);
		}
        else
        {
            GridSingleton.Instance.Set(new Vector2Int((int)transform.position.x, (int)transform.position.y), TileType.EMPTY);
        }
        transform.position = pos;
	}

	public void Delete() {
		next.Delete();
		Destroy(gameObject);
	}

	public void Grow(GameObject bodyPrefab) {
		if (next == null) {
			GameObject go = Instantiate(bodyPrefab, transform.position, Quaternion.identity);
			next = go.GetComponent<Body>();
		} else {
			next.Grow(bodyPrefab);
		}
	}
}
