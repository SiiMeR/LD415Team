using System.Collections;
using UnityEngine;

public class Body : MonoBehaviour
{
	public Body Next;

	public void Move(Vector3 pos) {
		if (Next != null) {
            Next.Move(transform.position);
		}
        else {
            GridSingleton.Instance.Set((int)transform.position.x, (int)transform.position.y, TileType.EMPTY);
        }
        transform.position = pos;
	}

	public void Delete() {
		if (Next)
		{
			Next.Delete();
			
		}
		Destroy(gameObject);

	}

	public void DeleteSlow(float secondsPerDelete)
	{	
		
		StartCoroutine(slowDeath(secondsPerDelete));
	}


	void SetColors()
	{
		GetComponent<SpriteRenderer>().color = Color.red;

		if (Next)
		{
			Next.SetColors();
		}
	}
	void SetEmpty()
	{
		GridSingleton.Instance.Get(transform.position).type = TileType.EMPTY;

		if (Next)
		{
			Next.SetEmpty();
		}
	}

	IEnumerator slowDeath(float seconds)
	{
		SetColors();
		SetEmpty();
		float timer = 0;

		while ((timer += Time.deltaTime) < seconds)
		{
			yield return null;
		}
		
		Destroy(gameObject);
		
		if (Next)
		{
			Next.DeleteSlow(seconds);
		}
		
		
		
		
	}
	
	public void Grow(GameObject bodyPrefab) {
        if (Next == null) {
			GameObject go = Instantiate(bodyPrefab, transform.position, Quaternion.identity);
			Next = go.GetComponent<Body>();
		} else {
			Next.Grow(bodyPrefab);
           
        }
	}

	public void DeleteBodyAtPos(Vector3 pos)
	{
		if (Vector3.Distance(transform.position,pos) < 0.5f)
		{
			DeleteSlow(0.07f);
		}
		else
		{
			if (Next)
			{
				Next.DeleteBodyAtPos(pos);
			}
			
		}
	}
}
