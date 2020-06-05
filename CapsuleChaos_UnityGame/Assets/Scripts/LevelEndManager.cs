using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LevelEndManager : MonoBehaviour
{
    [SerializeField]
    private PlayerScore playerScore = default;
    [SerializeField]
    private int levelId = 0;

    [SerializeField]
    private string APIGWbaseUrl = "https://localhost";
    [SerializeField]
    private int APIGWport = 5010;

    [SerializeField]
    private PlayerInfo playerInfo = default;
    [SerializeField]
    private GhostInfo ghostInfo = default;

    [SerializeField]
    private UnityStringEvent onSuccess = default;
    [SerializeField]
    private UnityStringEvent onFail = default;


    public void StartSaving()
    {
        StartCoroutine(SaveScoreToBackend());
    }

    public void ReturnToMainMenu()
    {
        Physics.gravity = new Vector3(0, -9.81f, 0);
        SceneManager.LoadScene(0);
    }

    private IEnumerator SaveScoreToBackend()
    {
        bool hasFailed = false;
        string successMessage = "Score saved successfully!";

        string validationJson = JsonUtility.ToJson(new ValidationData(levelId, playerScore.score, playerScore.GetTimeInteger()));

        using (UnityWebRequest req = UnityWebRequest.Post($"{APIGWbaseUrl}:{APIGWport}/api/validation/validate", ""))
        {
            req.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(validationJson));
            req.SetRequestHeader("Content-Type", "application/json");

            yield return req.SendWebRequest();
            while (!req.isDone) { yield return null; }
            if (req.isNetworkError)
            {
                onFail.Invoke("Score could not be saved due to a connection issue");
                hasFailed = true;
            }
            else if (req.responseCode != 0 && req.responseCode != (long)System.Net.HttpStatusCode.OK)
            {
                if (req.responseCode == (long)System.Net.HttpStatusCode.Conflict)
                {
                    onFail.Invoke("Score could not be saved because cheating was detected");
                }
                else if (req.responseCode == (long)System.Net.HttpStatusCode.NotFound)
                {
                    onFail.Invoke($"Score could not be saved because level {levelId} could not be found");
                }
                hasFailed = true;
            }
        }

        if (hasFailed) { yield break; }

        string highscoreJson = JsonUtility.ToJson(new HighscoreData(
            playerInfo.Player.accountId, 
            playerInfo.Player.username, 
            levelId, playerScore.score, 
            playerScore.GetTimeInteger(), 
            playerScore.GetTimeInteger()
            ));

        using (UnityWebRequest req2 = UnityWebRequest.Post($"{APIGWbaseUrl}:{APIGWport}/api/scoreboard/highscores", ""))
        {
            req2.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(highscoreJson));
            req2.SetRequestHeader("Content-Type", "application/json");

            yield return req2.SendWebRequest();
            while (!req2.isDone) { yield return null; }
            if (req2.isNetworkError)
            {
                onFail.Invoke("Score could not be saved due to a connection issue");
                hasFailed = true;
            }
            else if (req2.responseCode == (long)System.Net.HttpStatusCode.OK || req2.responseCode == (long)System.Net.HttpStatusCode.Created || req2.responseCode == 0)
            {
                successMessage = "New highscore!\nSuccessfully saved";
            }
            else if (req2.responseCode == (long)System.Net.HttpStatusCode.NoContent)
            {
                successMessage = "No highscore\nData successfully saved";
            }
            else
            { 
                onFail.Invoke($"Score could not be saved due to an unknown error ({req2.responseCode})");
                hasFailed = true;
            }
        }

        if (hasFailed) { yield break; }

        onSuccess.Invoke(successMessage);
    }

    [System.Serializable]
    private class ValidationData
    {
        public int levelId;
        public int maximumScore;
        public int minimumTime;

        public ValidationData(int levelId, int maximumScore, int minimumTime)
        {
            this.levelId = levelId;
            this.maximumScore = maximumScore;
            this.minimumTime = minimumTime;
        }
    }

    [System.Serializable]
    private class HighscoreData
    {
        public int playerId;
        public string playername;
        public int levelId;
        public int collectedScore;
        public int finishedTime;
        public int calculatedTotal;

        public HighscoreData(int playerId, string playername, int levelId, int collectedScore, int finishedTime, int calculatedTotal)
        {
            this.playerId = playerId;
            this.playername = playername ?? throw new ArgumentNullException(nameof(playername));
            this.levelId = levelId;
            this.collectedScore = collectedScore;
            this.finishedTime = finishedTime;
            this.calculatedTotal = calculatedTotal;
        }
    }
}

[System.Serializable]
    public class UnityStringEvent : UnityEvent<string>
    {
    }