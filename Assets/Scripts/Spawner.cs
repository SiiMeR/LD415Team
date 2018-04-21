using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public List<GameObject> enemies;
	public float cooldown = 5;

	void Start() {
		StartCoroutine(Spawn());
	}

	IEnumerator Spawn() {
		while (true) {
			Instantiate(enemies[Random.Range(0, enemies.Count - 1)], transform.position, Quaternion.identity);
			yield return new WaitForSeconds(cooldown);
		}
	}
}
