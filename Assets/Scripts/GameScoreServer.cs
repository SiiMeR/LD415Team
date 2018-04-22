using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

public static class GameScoreServer
{

    static string highscoreURL = "https://www.mobileapplication.ga/ludumdare41/highscores";

    public static void SendGameScoreToServer(string username, int score)
    {
        ServicePointManager.ServerCertificateValidationCallback += (o, certificate, chain, errors) => true;
        Debug.Log("Starting to send POST request to server...");
        var httpWebRequest = (HttpWebRequest)WebRequest.Create(highscoreURL);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {

            PlayerScore playerScore = new PlayerScore();
            playerScore.username = username;
            playerScore.score = score;

            string sendScoreJSON = JsonUtility.ToJson(playerScore);

            Debug.Log(sendScoreJSON);

            streamWriter.Write(sendScoreJSON);
            streamWriter.Flush();
            streamWriter.Close();
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
            if (result.Equals("score_set"))
            {
                Debug.Log("Score set");
            }
        }


    }

    public static List<PlayerScore> getHighScores()
    {
        ServicePointManager.ServerCertificateValidationCallback += (o, certificate, chain, errors) => true;
        var httpWebRequest = (HttpWebRequest)WebRequest.Create(highscoreURL);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "GET";

        List<PlayerScore> highScoresList = new List<PlayerScore>();

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
            JSONObject resultData = new JSONObject(result);
            int x = int.Parse(resultData.GetField("scoresamount").str);

            for (int i = 1; i <= x; i++)
            {
                JSONObject oneScore = resultData.GetField(i.ToString());
                highScoresList.Add(new PlayerScore(oneScore.list[0].ToString(), (int)oneScore.list[1].n));

            }

            foreach (PlayerScore p in highScoresList)
            {
                Debug.Log(p.ToString());
            }

        }
        return highScoresList;

    }
}
