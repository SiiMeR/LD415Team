using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public List<GameObject> enemies;
	public float cooldown = 5;
	public float waveCooldown = 15;

	void Start() {
		StartCoroutine(Spawn());
	}

	IEnumerator Spawn() {
		for (int i = 10; ; i += 10) {
			for (int j = 0; j < i; j++) {
				Instantiate(enemies[Random.Range(0, enemies.Count - 1)], transform.position, Quaternion.identity);
				yield return new WaitForSeconds(cooldown);
			}
			yield return new WaitForSeconds(waveCooldown);
		}
	}
}
