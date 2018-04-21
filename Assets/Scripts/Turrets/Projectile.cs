using UnityEngine;

public class Projectile : MonoBehaviour {
	public float damage;
	public float speed;
	public Enemy target;

	void Update() {
		Vector3 separation = target.transform.position - transform.position;
		transform.Translate(separation.normalized * speed * Time.deltaTime);
		if (separation.sqrMagnitude < 0.1) {
			//HURT THEM
		}
	}
}
