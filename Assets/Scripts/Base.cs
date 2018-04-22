using UnityEngine;

public class Base : MonoBehaviour {
	public int hp;
	public int maxhp = 10;
	public GameObject measure;

	public float RotBegin;
	public float RotEnd;
	void Start() {
		
		if (GridSingleton.Instance != null)
		{
			GridSingleton.Instance.Set(new Vector2Int((int) transform.position.x, (int) transform.position.y), TileType.BASE);
		}
		
	}

	private void Update()
	{
		float ratio = (float) hp / maxhp;
		Quaternion start = Quaternion.Euler(0,0, RotBegin);
		Quaternion end = Quaternion.Euler(0, 0, RotEnd);
		
		//measure.transform.rotation = Quater
		measure.transform.rotation = Quaternion.Slerp(end,start, ratio); // TODO FIX
	//	measure.transform.rotation = Quaternion.RotateTowards(start, end, ratio);
		

		//measure.transform.rotation = Quaternion.Euler(new Vector3(0,0,lerp));

	}
}
