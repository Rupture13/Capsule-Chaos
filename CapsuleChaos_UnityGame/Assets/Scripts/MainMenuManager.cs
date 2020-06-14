using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
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

    private void Start()
    {
        //Reset gravity for main menu physics simulation
        Physics.gravity = new Vector3(0, -9.81f, 0);

        //Point camera to level select when coming out of a level and being logged in
        if (playerInfo.Player.accountId != -1)
        {
            Debug.Log("Player already logged in!");
            mainMenuCamera.Play("MainMenuCameraInstantLevelSelect");
            alreadyLoggedIn.Invoke();
        }
    }

    public void SetLevelId(int levelId)
    {
        levelInfo.LevelId = levelId;
    }

    public void StartLevel()
    {
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
}
