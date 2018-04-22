using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCog : MonoBehaviour {
    public float direction = 1;
    public GameObject[] gears;
    public float[] gearSpeeds;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float speed = direction;
        for (int i = 0; i < gears.Length; i++)
        {
            gears[i].transform.Rotate(new Vector3(0, 0, speed * gearSpeeds[i]));
                speed = -speed;
        }
    }
}
