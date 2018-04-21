using UnityEngine;
using UnityEngine.UI;

public class GoldTracker : MonoBehaviour {
	public static int gold = 0;
	public static int Gold {
		get {
			return gold;
		}
		set {
			gold = value;
			UpdateGoldText();
		}
	}

	public static Text goldText;

	void Start() {
		goldText = GetComponent<Text>();
	}

	public static void UpdateGoldText() {
		goldText.text = "Gold: " + gold.ToString();
	}
}
