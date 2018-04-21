using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour {
	public int hp;
    public int gold;
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
