using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    public string backGroundMusic = "Pim Poy";

	// Use this for initialization
	void Start () {
        //GameScoreServer.SendGameScoreToServer("user1", 100);
        GameScoreServer.getHighScores();
        AudioManager.instance.Play(backGroundMusic, isLooping: true);
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
