using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField]
    private Transform player = default;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.position;
    }
}
