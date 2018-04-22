using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
	public float maxHealth = 100;
	[SerializeField] private float _health = 100;
	public int gold = 1;
    Base snakeBase;

	public Image hp;

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
				hp.fillAmount = _health / maxHealth;
			}
			
		}
	}

	private void Die() {
		GoldTracker.Gold += gold;
		EnemyTracker.Instance.enemies.Remove(this);
		GridSingleton.Instance.Set((int) transform.position.x, (int) transform.position.y, TileType.EMPTY);
        Destroy(gameObject);
    }

	void Start () {
		Health = maxHealth;
		snakeBase = GameObject.FindGameObjectWithTag("Base").GetComponent<Base>();
		EnemyTracker.Instance.enemies.Add(this);
	}

    private void FixedUpdate() {
        if (Vector3.Distance(transform.position, snakeBase.transform.position) < 0.1f) {
            DamageBase();
        }
	    
	    
    }

    public void DamageBase() {
        snakeBase.HP -= 1;
        Die();
    }
}
