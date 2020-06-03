using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField]
    private Transform player = default;

    private bool follow = true;

    void Update()
    {
        if (follow) { this.transform.position = player.position; }
    }

    public void SetFollowing(bool value)
    {
        follow = value;
    }
}
