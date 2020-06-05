using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EndPortal : MonoBehaviour
{
    [SerializeField]
    private int requiredScore;
    
    [SerializeField]
    private GameObject[] toActivateOnOpen = default;

    [SerializeField]
    private UnityStringEvent onLevelFinish = default;

    public void AddScore(float value)
    {
        requiredScore = Mathf.Max(0, requiredScore - Mathf.FloorToInt(value));
        if (requiredScore == 0)
        {
            foreach (GameObject toActivate in toActivateOnOpen)
            {
                toActivate.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var playerScore = other.gameObject.GetComponent<PlayerScore>();
        if (!playerScore) { return; }

        if (requiredScore == 0)
        {
            playerScore.StopTimer();
            onLevelFinish.Invoke(Utils.FormatTime(playerScore.timer));
        }
    }
}