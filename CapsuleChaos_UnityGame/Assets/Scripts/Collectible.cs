using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField]
    private int value = 1;

    [SerializeField]
    private float RotateSpeed = 1.0f;
    private Vector3 direction;

    private void Start()
    {
        direction = new Vector3(0, ((Random.Range(0, 2) != 0) ? 1 : -1) * RotateSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        var playerScore = other.gameObject.GetComponent<PlayerScore>();
        if (!playerScore) { return; }

        playerScore.AddScore(value);
        this.value = 0;
        GameObject.Destroy(gameObject);
    }
}
