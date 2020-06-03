using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private int currentLevelId = 0;

    private void Start()
    {
        Physics.gravity = new Vector3(0, -9.81f, 0);
    }

    public void SetLevelId(int levelId)
    {
        currentLevelId = levelId;
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(currentLevelId);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
