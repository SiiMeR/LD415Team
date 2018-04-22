using UnityEngine;

public class Base : MonoBehaviour {
	public int maxHP = 100;
	[SerializeField] private int hp;
	public int HP {
		get {
			return hp;
		} set {
			hp = value;
			UpdateHP();
		}
	}
	public GameObject measure;

	public float RotBegin;
	public float RotEnd;
	void Awake() {
		
		if (GridSingleton.Instance != null)
		{
			GridSingleton.Instance.Set(new Vector2Int((int) transform.position.x, (int) transform.position.y), TileType.BASE, GetComponent<SpriteRenderer>().bounds.size);
		}
		
	}

	void Start() {
		hp = maxHP;
		UpdateHP();
	}

	void UpdateHP() {
		measure.transform.rotation = Quaternion.Euler(0, 0, (float) hp / maxHP * 315 - 180);
	}
}
