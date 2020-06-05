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
    public int PlayerId;
    public int LevelId;
    public List<PerformanceSnapshot> Snapshots;
}

[System.Serializable]
public class PerformanceSnapshot
{
    public int Timestamp;
    public List<Vector3> Location;
    public List<Vector3> Rotation;
}
