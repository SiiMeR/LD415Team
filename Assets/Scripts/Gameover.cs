using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gameover : MonoBehaviour
{

	public InputField inputField;

	public GameObject namePanel;

	public GameObject lbPanel;

	public List<GameObject> txtElems;

	// Use this for initialization
	void Start()
	{
		namePanel.SetActive(false);
		lbPanel.SetActive(false);

		int playerscore = PlayerPrefs.GetInt("score");
		
		bool isHighScore = GameScoreServer
			                   .getHighScores()
			                   .Count(score => score.score >= playerscore) <= 10;
	
		if (isHighScore)
		{
			namePanel.SetActive(true);
		}
		else
		{
			var scores = GameScoreServer
				.getHighScores()
				.OrderByDescending(highscore => highscore.score)
				.ToList();
				
		
			FillTable(scores);
			lbPanel.SetActive(true);
		}
	}

/*	IEnumerator getScores()
	{

		var hs = GameScoreServer.getHighScores();

	//	yield return new WaitForSeconds(6.0f);
		
		
		bool isHighScore = hs.Count(score => score.score >= PlayerPrefs.GetInt("score")) < 10;
		
		if (isHighScore)
		{
			namePanel.SetActive(true);
		}
		else
		{
			lbPanel.SetActive(true);
		}
		
	}*/
	bool GetScoreNowIsInLB(int pScore)
	{
		return (GameScoreServer.getHighScores().Count(score => score.score >= pScore)) <= 10;
	}

	// Update is called once per frame
	void Update()
	{

	}

	private string nsm;
	
	public void OnEditEnd()
	{
		GameScoreServer.SendGameScoreToServer(inputField.text, PlayerPrefs.GetInt("score"));
		

//		ReplaceInHS(inputField.text, PlayerPrefs.GetInt("score"));
		nsm = inputField.text;
		namePanel.SetActive(false);
		lbPanel.SetActive(true);

		ReplaceInHS(inputField.text,PlayerPrefs.GetInt("score"));
	}

	public void OnRestart()
	{
		SceneManager.LoadScene("Main");
	}

	private void FillTable(List<PlayerScore> topscores)
	{
		for (int i = 0; i < txtElems.Count; i++)
		{
			var texts = txtElems[i].GetComponentsInChildren<Text>(true);

			if (i >= topscores.Count)
			{
				texts[0].text = (i + 1) + ". -";
				texts[1].text = " - ";
			}
			else
			{
				texts[0].text = (i + 1) + ". " + topscores[i].username.Trim('\"');
				texts[1].text = topscores[i].score.ToString();
			}

		}
	}
	private void ReplaceInHS(string name, int score)
	{		
		
		var topscores = GameScoreServer.getHighScores().OrderByDescending(highscore => highscore.score).ToList();

		if (topscores.Count == 0)
		{
			namePanel.SetActive(true);
			topscores.Add(new PlayerScore(nsm, score));
		}
		else if (topscores.Count > 0 && topscores[topscores.Count-1].score < score)
		{
			namePanel.SetActive(true);
			topscores.Add(new PlayerScore(nsm, score));
		}
		else
		{
			topscores = topscores.OrderByDescending(highscore => highscore.score).ToList();
			lbPanel.SetActive(true);
		}
	
		
		topscores = topscores.OrderByDescending(highscore => highscore.score).ToList();
		
		
		FillTable(topscores);
		

	}
}
