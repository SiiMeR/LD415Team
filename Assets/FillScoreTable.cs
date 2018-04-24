using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FillScoreTable : MonoBehaviour
{

	public List<GameObject> textlines;
	// Use this for initialization
	void Start ()
	{


	}

	public void GetScores()
	{
		var topscores = GameScoreServer.getHighScores().Take(10).OrderByDescending(highscore => highscore.score).ToList();
		
		for (int i = 0; i < textlines.Count; i++)
		{
			var texts = textlines[i].GetComponentsInChildren<Text>();

			if (i >= topscores.Count)
			{
				texts[0].text = (i + 1) + ". -";
				texts[1].text = " - ";
		
			}
			else
			{
				texts[0].text =  (i+1) + ". " + topscores[i].username.Trim('\"');
				texts[1].text = topscores[i].score.ToString();
			}			
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
