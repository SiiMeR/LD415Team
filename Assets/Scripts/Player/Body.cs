using UnityEngine;

public class Body : MonoBehaviour {
	public Body next;

	public void Move(Vector3 pos) {
		if (next != null) {
			next.Move(transform.position);
		}
		transform.position = pos;
	}
}
