using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public List<GameObject> enemies;
	public float waveCooldown = 10;	

	void Start() {
		
		GridSingleton.Instance.Set(new Vector2Int((int) transform.position.x, (int) transform.position.y), TileType.SPAWNER);
		StartCoroutine(Spawn());
	}

	IEnumerator Spawn() {
		for (int wave = 1; ; wave++) {
			int monsterCount = (int) Mathf.Pow((3 * wave), 1.5f);
			float cooldown = 5 * Mathf.Sqrt(monsterCount) / monsterCount;

			for (int i = 0; i < monsterCount; i++) {
				Instantiate(enemies[Random.Range(0, enemies.Count)], transform.position, Quaternion.identity);
				yield return new WaitForSeconds(cooldown);
			}
			yield return new WaitForSeconds(waveCooldown);
		}
	}
}
