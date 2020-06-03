using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float gravityChangeSpeed = 1.0f;

    [SerializeField]
    private float maxFreeFallTime = 5f;

    [SerializeField]
    private Transform playerFollower = default;

    [SerializeField]
    private Transform gravityCenter = default;
    [SerializeField]
    private Transform gravityDirection = default;

    [SerializeField]
    private Canvas canvas = default;
    [SerializeField]
    private RectTransform UIStart = default;
    [SerializeField]
    private RectTransform UICurrent = default;
    [SerializeField]
    private RectTransform UITrail = default;
    [SerializeField]
    private RectTransform UITrailPivot = default;

    [SerializeField]
    private Image vignette = default;
    [SerializeField]
    private Gradient vignetteColourOverTime = default;

    private Vector2 halfResolutionSize;

    private int collisionAmount;
    private float freefallTime;
    private bool dead = false;
    private bool allowSepuku = true;


    private Vector3 originalPos;
    private Quaternion originalRot;
    private Rigidbody rb;

    [SerializeField]
    private UnityEvent death = default;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalPos = transform.position;
        originalRot = playerFollower.rotation;
        halfResolutionSize = ((RectTransform)canvas.transform).anchoredPosition;
        collisionAmount = 0;
    }

    void Update()
    {
        if (dead) { return; }

        if (allowSepuku && Input.GetMouseButtonDown(1))
        {
            StartDeath();
        }

        if (Input.GetMouseButtonDown(0))
        {
            UIStart.anchoredPosition = ((Vector2)Input.mousePosition) - halfResolutionSize;
            UIStart.gameObject.SetActive(true);
            return;
        }
        if (Input.GetMouseButton(0))
        {
            var deltaPos = ((Vector2)Input.mousePosition) - halfResolutionSize - UIStart.anchoredPosition;
            UICurrent.anchoredPosition = deltaPos;
            UITrail.sizeDelta = new Vector2(UITrail.sizeDelta.x, deltaPos.magnitude - 25);
            var newRotation = new Vector3(0, 0, (Mathf.Atan2(deltaPos.y, deltaPos.x) * 180) / Mathf.PI - 90f);
            
            var rot = UITrailPivot.rotation;
            rot.eulerAngles = newRotation;
            UITrailPivot.rotation = rot;

            var rot2 = UICurrent.rotation;
            rot2.eulerAngles = newRotation;
            UICurrent.rotation = rot2;

            //Debug.Log($"Vertical: {deltaPos.y} | Horizontal: {deltaPos.x}");

            //var gravityRot = gravityCenter.rotation;

            deltaPos = new Vector2(deltaPos.x / (halfResolutionSize.x * 2) * gravityChangeSpeed, deltaPos.y / (halfResolutionSize.y * -2) * gravityChangeSpeed);

            gravityCenter.Rotate(Vector3.right, deltaPos.y);
            gravityCenter.Rotate(Vector3.forward, deltaPos.x);
            Physics.gravity = gravityDirection.position;

            playerFollower.Rotate(Vector3.right, deltaPos.y);
            playerFollower.Rotate(Vector3.forward, deltaPos.x);
        }
        if (Input.GetMouseButtonUp(0))
        {
            UIStart.gameObject.SetActive(false);
            UICurrent.anchoredPosition = Vector2.zero;
        }

        if (collisionAmount == 0)
        {
            freefallTime += Time.deltaTime;

            vignette.color = vignetteColourOverTime.Evaluate(freefallTime / maxFreeFallTime);

            if (freefallTime >= maxFreeFallTime)
            {
                StartDeath();
            }
        }
    }

    private void StartDeath()
    {
        dead = true;
        allowSepuku = false;
        playerFollower.GetComponent<PlayerFollower>().SetFollowing(false);
        UIStart.gameObject.SetActive(false);
        death.Invoke();
    }

    public void StopDeath()
    {
        dead = false;
        freefallTime = 0;
        transform.position = originalPos;
        transform.rotation = Quaternion.identity;
        gravityCenter.rotation = Quaternion.identity;
        playerFollower.rotation = originalRot;
        Physics.gravity = new Vector3(0, -9.81f, 0);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        playerFollower.GetComponent<PlayerFollower>().SetFollowing(true);
    }

    public void AllowSepuku()
    {
        allowSepuku = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        ++collisionAmount;
        vignette.color = vignetteColourOverTime.Evaluate(0);
    }

    private void OnCollisionExit(Collision collision)
    {
        --collisionAmount;

        if (collisionAmount == 0)
        {
            freefallTime = 0;
        }
    }
}
