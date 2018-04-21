using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] private int _tilesPerSecond = 1;
	[SerializeField] private int _health = 100;

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


	public int TilesPerSecond
	{
		get { return _tilesPerSecond; }
		set { _tilesPerSecond = value; }
	}

	private float _moveTimer;
	
	public Vector2 MoveDirection { get; set; }

	// Use this for initialization
	void Start () {
		MoveDirection = Vector2.right;
	}
	
	// Update is called once per frame
	void Update ()
	{
		_moveTimer += Time.deltaTime;
	}

	private void FixedUpdate()
	{
		if (_moveTimer > 1.0f/_tilesPerSecond)
		{
			_moveTimer = 0;
			transform.Translate(MoveDirection);
		}
		
	}
}
