using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	
	[SerializeField] private int _health = 100;
    public Base snakeBase;

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
        Destroy(gameObject);
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

        if (Vector3.Distance(transform.position, snakeBase.transform.position) < 0.1f)
        {
            DamageBase();
        }
        
    }

    public void DamageBase()
    {
        snakeBase.hp -= 1;
        Die();
    }

}
