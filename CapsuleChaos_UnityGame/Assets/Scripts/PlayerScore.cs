using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerScore : MonoBehaviour
{
    [SerializeField]
    private UnityFloatEvent scoreEvent = default;
    [SerializeField]
    private UnityFloatEvent scoreValueEvent = default;
    [SerializeField]
    private UnityFloatEvent timeEvent = default;

    public int score;
    public float timer;

    private bool pause;

    void Start()
    {
        pause = false;
        score = 0;
        timer = 0f;
        timeEvent.Invoke(timer);
    }

    void Update()
    {
        if (!pause)
        {
            timer += Time.deltaTime;
            timeEvent.Invoke(timer);
        }
    }

    public void AddScore(int value)
    {
        score += value;
        scoreEvent.Invoke(score);
        scoreValueEvent.Invoke(value);
    }

    public int GetTimeInteger()
    {
        return Mathf.FloorToInt(timer * 100);
    }

    public void StopTimer()
    {
        pause = true;
    }

    
}