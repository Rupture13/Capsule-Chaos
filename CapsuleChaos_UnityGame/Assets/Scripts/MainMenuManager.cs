using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private string APIGWbaseUrl = "http://localhost";
    //[SerializeField]
    //private int APIGWport = 5010;

    [SerializeField]
    private PlayerInfo playerInfo = default;
    [SerializeField]
    private LevelInfo levelInfo = default;
    [SerializeField]
    private GhostInfo ghostInfo = default;

    [SerializeField]
    private Animator mainMenuCamera = default;

    [SerializeField]
    private UnityEvent alreadyLoggedIn = default;

    private List<PlayerPerformance> ghostData;
    [SerializeField]
    private List<GhostDataButton> ghostDataButtons = default;

    [SerializeField]
    private UnityEvent ghostsRetrieveSuccess = default;
    [SerializeField]
    private UnityEvent ghostsRetrieveFail = default;

    private void Start()
    {
        //Reset gravity for main menu physics simulation
        Physics.gravity = new Vector3(0, -9.81f, 0);

        //Point camera to level select when coming out of a level and being logged in
        if (playerInfo.Player.accountId != -1)
        {
            //Debug.Log("Player already logged in!");
            mainMenuCamera.Play("MainMenuCameraInstantLevelSelect");
            alreadyLoggedIn.Invoke();
        }
    }

    public void SetLevelId(int levelId)
    {
        levelInfo.LevelId = levelId;

        //Retrieve ghosts and pass in OnSuccess and OnFail methods
        StartCoroutine(GetGhostDataWebRequest(levelId, OnRetrieveGhostDataSuccess, OnRetrieveGhostDataFail));
    }

    private void OnRetrieveGhostDataSuccess()
    {
        //Pull player up to first index
        var ownGhost = ghostData.Where(ghost => ghost.playerId == playerInfo.Player.accountId).ToList();
        if (ownGhost.Count > 0)
        {
            var realOwnGhost = ownGhost[0];
            realOwnGhost.playerName = "You";
            ghostData.Remove(realOwnGhost);
            ghostData.Insert(0, realOwnGhost);
        }

        //Set ghostButtons data
        for (int i = 0; i < ghostData.Count; i++)
        {
            var ghost = ghostData[i];
            var ghostButton = ghostDataButtons[i];
            ghostButton.gameObject.SetActive(true);
            ghostButton.SetData(
                ghost.playerName, 
                ghost.snapshots.Last().timestamp, 
                ghost.playerId
                );
        }

        ghostsRetrieveSuccess.Invoke();
    }

    private void OnRetrieveGhostDataFail(string failReason)
    {
        //Debug.Log($"[GhostDataRetrieve Fail] Reason: {failReason}.");
        ghostsRetrieveFail.Invoke();
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(levelInfo.LevelId);

        ghostInfo.PlayerPerformance.playerId = -1;
    }

    public void StartLevelWithGhost(int ghostPlayerId)
    {
        //Get correct ghost data
        ghostInfo.PlayerPerformance = ghostData.FirstOrDefault(ghost => ghost.playerId == ghostPlayerId);

        SceneManager.LoadScene(levelInfo.LevelId);
    }

    private void OnApplicationQuit()
    {
        playerInfo.Player.accountId = -1;
        playerInfo.Player.email = "";
        playerInfo.Player.username = "";
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator GetGhostDataWebRequest(int levelId, System.Action onSuccess, System.Action<string> onFail)
    {
        using (UnityWebRequest req = UnityWebRequest.Get($"{APIGWbaseUrl}/api/ghost/PlayerPerformances/{levelId}"))
        {
            yield return req.SendWebRequest();
            while (!req.isDone) { yield return null; }
            if (req.isNetworkError)
            {
                onFail(req.error);
            }
            else if (req.responseCode != 0 && req.responseCode != (long)System.Net.HttpStatusCode.OK)
            {
                onFail(req.responseCode.ToString());
            }
            else
            {
                byte[] result = req.downloadHandler.data;
                string performancesJson = System.Text.Encoding.Default.GetString(result);
                //Debug.Log(performancesJson);
                ghostData = JsonArrayUtility.FromJson<PlayerPerformance>(performancesJson);
                if (ghostData.Count == 0)
                {
                    onFail("No PlayerPerformances exist yet for this level");
                }
                else
                {
                    onSuccess();
                }
            }
        }
    }
}
