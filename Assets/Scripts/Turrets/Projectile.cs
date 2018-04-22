using UnityEngine;

public class Projectile : MonoBehaviour {
	public float damage;
	public float speed;
	public Enemy target;

	private Vector3 lastKnownPosition;
	void Awake()
	{
		transform.parent = GameObject.FindGameObjectWithTag("Bullets").transform;
	}
	void Update() {
		
		Vector3 targetPos = lastKnownPosition;

		if (target)
		{
			targetPos = target.transform.position;
			lastKnownPosition = targetPos;
		}
		
		Vector3 separation = targetPos - transform.position;
		
		transform.Translate(separation.normalized * speed * Time.deltaTime);
			
		if (separation.sqrMagnitude < 0.1) {

			if (target)
			{
				target.Health -= damage;
			}
			Destroy(gameObject);
		}
	}
}
