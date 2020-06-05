using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textUI = default;

    public void SetTime(float time)
    {
        textUI.text = Utils.FormatTime(time);
    }
}
