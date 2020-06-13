using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textUI = default;

    public void SetScore(float score)
    {
        textUI.text = Mathf.FloorToInt(score).ToString();
    }
}
