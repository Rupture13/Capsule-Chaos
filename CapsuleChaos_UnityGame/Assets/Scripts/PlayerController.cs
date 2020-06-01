using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float gravityChangeSpeed = 1.0f;

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

    private Vector2 halfResolutionSize;


    private void Start()
    {
        halfResolutionSize = ((RectTransform)canvas.transform).anchoredPosition;
    }

    void Update()
    {
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
    }
}
