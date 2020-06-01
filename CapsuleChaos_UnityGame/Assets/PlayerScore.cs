using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerScore : MonoBehaviour
{
    [SerializeField]
    private UnityFloatEvent scoreEvent = default;
    [SerializeField]
    private UnityFloatEvent timeEvent = default;

    private int score;
    private float timer;


    void Start()
    {
        score = 0;
        scoreEvent.Invoke(score);
        timer = 0f;
        timeEvent.Invoke(timer);
    }

    void Update()
    {
        timer += Time.deltaTime;
        timeEvent.Invoke(timer);
    }

    public void AddScore(int value)
    {
        score += value;
        scoreEvent.Invoke(score);
    }

    [System.Serializable]
    public class UnityFloatEvent : UnityEvent<float>
    {
    }
}
