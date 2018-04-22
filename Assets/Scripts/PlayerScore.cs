using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerScore
{

    public string username;
    public int score;

    public PlayerScore(string username_, int score_)
    {
        username = username_;
        score = score_;
    }

    public PlayerScore()
    {
        
    }


    public override string ToString()
    {
        return "Score: " + username + " " + score.ToString();
    }
}
