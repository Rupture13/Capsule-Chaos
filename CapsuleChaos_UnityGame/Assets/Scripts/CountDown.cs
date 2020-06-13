using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CountDown : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text = default;
    [SerializeField]
    private UnityEvent startGame = default;

    private int countDown = 3;

    public void DecreaseCountdown()
    {
        --countDown;
        if (countDown > 0)
        {
            text.text = countDown.ToString();
        }
        else
        {
            startGame.Invoke();
            GameObject.Destroy(gameObject);
        }
    }
}
