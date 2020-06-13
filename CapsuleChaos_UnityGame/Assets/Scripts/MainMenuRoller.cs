using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuRoller : MonoBehaviour
{
    [SerializeField]
    private float deathHeight = default;
    private Vector3 startPos;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < deathHeight)
        {
            transform.position = startPos;
        }
    }
}
