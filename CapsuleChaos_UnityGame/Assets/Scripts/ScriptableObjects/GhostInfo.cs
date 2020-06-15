using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GhostInfo", menuName = "ScriptableObjects/GhostInfoSO", order = 1)]
public class GhostInfo : ScriptableObject
{
    public PlayerPerformance PlayerPerformance;
}

[System.Serializable]
public class PlayerPerformance
{
    public int playerId;
    public string playerName;
    public int levelId;
    public List<PerformanceSnapshot> snapshots;

    public PlayerPerformance(int _playerId, string _playerName,int _levelId)
    {
        playerId = _playerId;
        playerName = _playerName;
        levelId = _levelId;
        snapshots = new List<PerformanceSnapshot>();
    }
}

[System.Serializable]
public class PerformanceSnapshot
{
    public float timestamp;
    public Vector3 location;
    public Quaternion rotation;

    public PerformanceSnapshot(float _timestamp, Vector3 _location, Quaternion _rotation)
    {
        timestamp = _timestamp;
        location = _location;
        rotation = _rotation;
    }
}
