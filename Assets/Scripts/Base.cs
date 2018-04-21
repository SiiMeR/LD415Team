using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour {
	public int hp;
    public int gold = 0;
	public int Gold {
		get {
			return gold;
		} set {
			gold = value;
			UpdateGoldText();
		}
	}

    public Text goldText;

	void Start() {
        UpdateGoldText();
		GridSingleton.Instance.Set(new Vector2Int((int) transform.position.x, (int) transform.position.y), TileType.BASE);
	}

    public void UpdateGoldText()
    {
        goldText.text = "Gold: " + gold.ToString();
    }

}
