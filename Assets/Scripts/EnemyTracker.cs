using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : Singleton<EnemyTracker> {
	public List<Enemy> enemies = new List<Enemy>();

	public Enemy GetNearest(Vector3 position) {
		Enemy nearest = null;
		float sqrDistance = float.MaxValue;

		foreach (Enemy enemy in enemies) {
			if (enemy == null)
			{
				continue;
			}
			Vector3 diff = enemy.transform.position - position;
			float refDistance = new Vector2(diff.x, diff.y).sqrMagnitude;
			if (refDistance < sqrDistance) {
				nearest = enemy;
				sqrDistance = refDistance;
			}
		}

		return nearest;
	}
}
