using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfo", menuName = "ScriptableObjects/PlayerInfoSO", order = 1)]
public class PlayerInfo : ScriptableObject
{
    public PlayerInformation Player;
}

[System.Serializable]
public class PlayerInformation
{
    public int accountId;
    public string email;
    public string username;
}
