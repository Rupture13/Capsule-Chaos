using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GhostDataButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI playerNameText = default;
    [SerializeField]
    private TextMeshProUGUI playerTimeText = default;
    private int ghostId;

    [SerializeField]
    private UnityIntegerEvent onClick = default;

    public void SetData(string _playerName, float _playerTime, int _ghostPlayerId)
    {
        playerNameText.text = _playerName;

        playerTimeText.text = Utils.FormatTime(_playerTime).Substring(0,5);

        ghostId = _ghostPlayerId;
    }

    public void SetSelectedGhostData()
    {
        onClick.Invoke(ghostId);
    }
}
