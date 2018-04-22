using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

public static class GameScoreServer {

    static string highscoreURL = "https://www.mobileapplication.ga/ludumdare41/highscores";

    public static void SendGameScoreToServer(string username, int score)
    {
        ServicePointManager.ServerCertificateValidationCallback += (o, certificate, chain, errors) => true;
        Debug.Log("Starting to send POST request to server...");
        var httpWebRequest = (HttpWebRequest) WebRequest.Create(highscoreURL);
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
            Debug.Log(resultData.GetField("scoresamount").ToString());
            string x = resultData.GetField("scoresamount").ToString();
            Debug.Log(x);

            // NO BLJAT PARSIGE SEE  -- resultData.GetField("scoresamount").ToString(); -- INTIKS 
            // JA ASENDAGE SEE SELLE 4ga
            for (int i = 1; i < 4; i++)
            {
                Debug.Log(i.ToString());
                JSONObject oneScore = resultData.GetField(i.ToString());
                highScoresList.Add(new PlayerScore(oneScore.list[0].ToString(), (int)oneScore.list[1].n));
     
            }

            Debug.Log(highScoresList[0].ToString());
            Debug.Log(highScoresList[2].ToString());
            Debug.Log(resultData.GetField("scoresamount"));
            Debug.Log(result);
        }
        return null;

    }
}
