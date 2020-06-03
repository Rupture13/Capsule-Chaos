using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPortal : MonoBehaviour
{
    [SerializeField]
    private int requiredScore;
    
    [SerializeField]
    private GameObject[] toActivateOnOpen = default;

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
        if (!other.gameObject.GetComponent<PlayerController>()) { return; }

        if (requiredScore == 0)
        {
            Physics.gravity = new Vector3(0, -9.81f, 0);
            SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log("Get more points, plz");
        }
    }
}
