using UnityEngine;

public class Enemy : MonoBehaviour {	
	[SerializeField] private float _health = 100;
	public int gold = 1;
    Base snakeBase;

	public float Health
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

	private void Die() {
		GoldTracker.Gold += gold;
		EnemyTracker.enemies.Remove(this);
        Destroy(gameObject);
    }

	void Start () {
		snakeBase = GameObject.FindGameObjectWithTag("Base").GetComponent<Base>();
		EnemyTracker.enemies.Add(this);
	}

    private void FixedUpdate() {
        if (Vector3.Distance(transform.position, snakeBase.transform.position) < 0.1f) {
            DamageBase();
        }
	    
	    
    }

    public void DamageBase() {
        snakeBase.hp -= 1;
        Die();
    }
}
