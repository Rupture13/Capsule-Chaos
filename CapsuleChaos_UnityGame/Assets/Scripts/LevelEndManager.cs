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
    private LevelInfo levelInfo = default;

    [SerializeField]
    private PlayerScore playerScore = default;

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
        //TODO delete
        string ghostJson2 = JsonUtility.ToJson(ghostInfo.PlayerPerformance);
        Debug.Log(ghostJson2);

        bool hasFailed = false;
        string successMessage = "Score saved successfully!";

        // -------------- Step 1: Check cheating --------------
        string validationJson = JsonUtility.ToJson(new ValidationData(levelInfo.LevelId, playerScore.score, playerScore.GetTimeInteger()));

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
                    onFail.Invoke($"Score could not be saved because level {levelInfo.LevelId} could not be found");
                }
                hasFailed = true;
            }
        }

        // Stop if cheating detected or the request failed in some other way
        if (hasFailed) { yield break; }


        // -------------- Step 2: Post score --------------
        bool newHighscore = false;

        string highscoreJson = JsonUtility.ToJson(new HighscoreData(
            playerInfo.Player.accountId, 
            playerInfo.Player.username,
            levelInfo.LevelId, 
            playerScore.score, 
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
                newHighscore = true;
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

        // Stop if request has failed in some way
        if (hasFailed) { yield break; }

        // -------------- Step 3.A: Stop on no new highscore --------------
        if (!newHighscore)
        {
            onSuccess.Invoke(successMessage);
            yield break;
        }

        // -------------- Step 3.B: Post new ghost data on new highscore --------------
        string ghostJson = JsonUtility.ToJson(ghostInfo.PlayerPerformance);

        using (UnityWebRequest req3 = UnityWebRequest.Post($"{APIGWbaseUrl}:{APIGWport}/api/ghost/playerperformances", ""))
        {
            req3.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(ghostJson));
            req3.SetRequestHeader("Content-Type", "application/json");

            yield return req3.SendWebRequest();
            while (!req3.isDone) { yield return null; }
            if (req3.isNetworkError)
            {
                onFail.Invoke("Score could not be saved due to a connection issue");
                hasFailed = true;
            }
            else if (req3.responseCode == (long)System.Net.HttpStatusCode.OK || req3.responseCode == (long)System.Net.HttpStatusCode.Created || req3.responseCode == 0)
            {
                hasFailed = false;
            }
            else
            {
                onFail.Invoke($"Score could not be saved due to an unknown error ({req3.responseCode})");
                hasFailed = true;
            }
        }

        // Stop if request has failed in some way
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