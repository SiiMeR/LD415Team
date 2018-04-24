using UnityEngine;

public class Base : MonoBehaviour {
	public int maxHP = 100;
	[SerializeField] private int hp;
	public int HP {
		get {
			return hp;
		} set {
			if (value < 1)
			{
				StartCoroutine(GameObject.FindGameObjectWithTag("Head").GetComponent<Head>().DIE());
			}
			hp = value;
			UpdateHP();
		}
	}
	public GameObject measure;

	public float RotBegin;
	public float RotEnd;

	void Start() {
		GridSingleton.Instance.Set(new Vector2Int((int) transform.position.x, (int) transform.position.y), TileType.BASE);
		GridSingleton.Instance.Set(new Vector2Int((int) transform.position.x, (int) transform.position.y + 1), TileType.BASE);
		GridSingleton.Instance.Set(new Vector2Int((int) transform.position.x + 1, (int) transform.position.y), TileType.BASE);
		GridSingleton.Instance.Set(new Vector2Int((int) transform.position.x + 1, (int) transform.position.y + 1), TileType.BASE);

		hp = maxHP;
		UpdateHP();
	}

	void UpdateHP() {
		measure.transform.rotation = Quaternion.Euler(0, 0, (float) hp / maxHP * 315 - 180);
	}
}
