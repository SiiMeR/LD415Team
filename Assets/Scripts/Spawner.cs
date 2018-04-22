using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public List<GameObject> enemies;
	public float cooldown = 5;
	public float waveCooldown = 15;	

	void Start() {
		
		GridSingleton.Instance.Set(new Vector2Int((int) transform.position.x, (int) transform.position.y), TileType.SPAWNER);
		StartCoroutine(Spawn());
	}

	IEnumerator Spawn() {
		for (int wave = 1; ; wave++) {
			for (int i = 0; i < wave; i++) {
				Instantiate(enemies[Random.Range(0, enemies.Count)], transform.position, Quaternion.identity);
				yield return new WaitForSeconds(cooldown);
			}
			yield return new WaitForSeconds(waveCooldown);
		}
	}
}
