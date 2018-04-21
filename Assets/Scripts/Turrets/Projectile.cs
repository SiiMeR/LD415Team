using UnityEngine;

public class Projectile : MonoBehaviour {
	public float damage;
	public float speed;
	public Enemy target;
   
    void Update() {
		if (target == null) {
			Destroy(gameObject);
		} else {
			Vector3 separation = target.transform.position - transform.position;
			transform.Translate(separation.normalized * speed * Time.deltaTime);
			if (separation.sqrMagnitude < 0.1) {
                target.Health -= damage;
				print(target.Health);
				Destroy(gameObject);
			}
		}
	}
}
