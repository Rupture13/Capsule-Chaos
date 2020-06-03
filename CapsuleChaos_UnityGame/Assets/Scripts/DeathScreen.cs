using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeathScreen : MonoBehaviour
{
    [SerializeField]
    private UnityEvent revive = default;
    [SerializeField]
    private UnityEvent playerControl = default;

    public void RevivePlayer()
    {
        revive.Invoke();
    }

    public void AllowPlayerControlledDeath()
    {
        playerControl.Invoke();
    }
}
