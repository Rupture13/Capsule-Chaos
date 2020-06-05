using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time - Mathf.FloorToInt(time)) * 100);
        return $"{minutes:00}:{seconds:00}:{milliseconds:00}";
    }
}
