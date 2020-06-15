using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPerformanceCapture : MonoBehaviour
{
    [SerializeField]
    private float captureInterval = 0.5f;

    [SerializeField]
    private PlayerInfo playerInfo = default;

    [SerializeField]
    private LevelInfo levelInfo = default;

    [SerializeField]
    private GhostInfo ghostInfo = default;

    private PlayerPerformance currentPerformance;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        currentPerformance = new PlayerPerformance(playerInfo.Player.accountId, playerInfo.Player.username, levelInfo.LevelId);

        InvokeRepeating(nameof(CaptureSnapshot), time, captureInterval);
    }

    public void StopCapture()
    {
        //Stop taking captures
        CancelInvoke();

        //Save performance capture into ScriptableObject
        ghostInfo.PlayerPerformance = currentPerformance;
    }

    private void CaptureSnapshot()
    {
        time += captureInterval;
        currentPerformance.snapshots.Add(new PerformanceSnapshot(time, transform.position, transform.rotation));
    }
}
