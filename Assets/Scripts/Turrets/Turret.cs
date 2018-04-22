using UnityEngine;

public class Turret : MonoBehaviour {
	public float range;
	public float cooldown;
	public GameObject projectile;
	Enemy target;

	float timer = 0;

    void Update() {
		timer += Time.deltaTime;

		//Keep aiming at the closest enemy
		if (target != null) {
			Aim();
		} else {
			target = EnemyTracker.Instance.GetNearest(transform.position);
			Aim();
		}

		if (timer > cooldown) {
			timer -= cooldown;

			//Get a new closest enemy if the current one is out of range
			if (target != null) {
				if ((target.transform.position - transform.position).magnitude < range) {
					Shoot();
				} else {
					Enemy trgt = EnemyTracker.Instance.GetNearest(transform.position);
					if ((trgt.transform.position - transform.position).magnitude < range) {
						Shoot();
					}
				}
			}
		}
	}

	void Aim() {
		transform.LookAt(new Vector3(target.transform.position.x, target.transform.position.y));
	}

	void Shoot() {
		AudioManager.instance.Play("shootingSound"); // string nimi
		Projectile shotRef = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
		shotRef.target = target;
	}
}
