using UnityEngine;

public class Turret : MonoBehaviour {
	public float range;
	public float cooldown;
	public GameObject projectile;

	float timer = 0;
    public AudioClip shootSound;
    private AudioSource shootingSound;

    private void Awake()
    {
        shootingSound = GetComponent<AudioSource>();
    }

    void Update() {
		timer += Time.deltaTime;
		if (timer > cooldown) {
			timer -= cooldown;

			Enemy target = EnemyTracker.GetNearest(transform.position);
			if (target != null) {
             //   shootingSound.PlayOneShot(shootSound, 1.0f);
				AudioManager.instance.Play("shootingSound"); // string nimi
                Projectile shotRef = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
				shotRef.target = target;
			}
		}
	}
}
