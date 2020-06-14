using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GhostPerformance : MonoBehaviour
{
    [SerializeField]
    private GhostInfo ghostInfo = default;
    private List<PerformanceSnapshot> ghostPoints;

    private float timer;
    private float timeInterval;

    void Start()
    {
        if (ghostInfo.PlayerPerformance.playerId == -1)
        {
            //No ghost selected, end ghost
            GameObject.Destroy(this.gameObject);
        }

        ghostPoints = ghostInfo.PlayerPerformance.snapshots;

        if (ghostPoints.Count < 2)
        {
            //Not enough points, end ghost
            GameObject.Destroy(this.gameObject);
            return;
        }

        GetComponent<MeshRenderer>().enabled = true;

        timer = 0;
        timeInterval = ghostPoints[0].timestamp;
    }

    void Update()
    {
        timer += Time.deltaTime;

        int currentPoint = Mathf.FloorToInt(timer * (1/timeInterval));
        int nextPoint = Mathf.CeilToInt(timer * (1 / timeInterval));

        if (nextPoint == ghostPoints.Count)
        {
            //Performance completed, end ghost
            GameObject.Destroy(this.gameObject);
            return;
        }

        //Lerp position and rotation of Ghost capsule for smooth movement
        transform.position = Vector3.Lerp(ghostPoints[currentPoint].location, ghostPoints[nextPoint].location, (timer % timeInterval)/timeInterval);
        transform.rotation = Quaternion.Lerp(ghostPoints[currentPoint].rotation, ghostPoints[nextPoint].rotation, (timer % timeInterval) / timeInterval);
    }
}