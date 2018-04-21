using UnityEngine;

public class Turret : MonoBehaviour {
	public float range;
	public float cooldown;
	public GameObject projectile;

	float timer = 0;

	void Update() {
		timer += Time.deltaTime;
		if (timer > cooldown) {
			timer -= cooldown;

			Enemy target = EnemyTracker.GetNearest(transform.position);
			if (target != null) {
				Projectile shotRef = Instantiate(projectile).GetComponent<Projectile>();
				shotRef.target = target;
			}
		}
	}
}
