using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	
	[SerializeField] private int _health = 100;
    Base snakeBase;

	public int Health
	{
		get { return _health; }
		set
		{
			if (value < 1)
			{
				Die();
			}
			else
			{
				_health = value;	
			}
			
		}
	}

	private void Die()
	{
		print("Minion died");
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
        
    }

    private void FixedUpdate()
    {
        Debug.Log("ENEMY COORDINATES");
        Debug.Log(transform.position);
        GameObject obj = GameObject.FindWithTag("Base");
        Debug.Log("BASE COORDINATES");
        Debug.Log(obj.transform.position);
        if ((int) transform.position.x == (int) obj.transform.position.x && (int) transform.position.y == (int) obj.transform.position.y) {
            DamageBase();
        }
    }

    public void DamageBase()
    {
        snakeBase.Hp -= 1;
        Destroy(gameObject);
    }

}
