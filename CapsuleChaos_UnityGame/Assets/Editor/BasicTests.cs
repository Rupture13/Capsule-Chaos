using NUnit.Framework;
using UnityEngine;

public class BasicTests : MonoBehaviour
{
    private GameObject go;
    private PlayerScore playerScore;

    [SetUp]
    public void SetUp()
    {
        go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        playerScore = go.AddComponent<PlayerScore>();
        playerScore.score = 0;      //0 score
        playerScore.timer = 20.00f; //20 seconds
    }

    [Test]
    public void TestPlayerScoreScoreAdd()
    {
        try
        {
            playerScore.AddScore(1);
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log($"Expected NullReference in testing: '{e.Message}'");
        }
        
        Assert.AreEqual(1, go.GetComponent<PlayerScore>().score);
    }

    [Test]
    public void TestPlayerScoreTimer()
    {
        Assert.AreEqual(20.00f, go.GetComponent<PlayerScore>().timer);
        Assert.AreEqual(2000, playerScore.GetTimeInteger());
    }

    [Test]
    public void TestFormatTimeUtility()
    {
        var expectedTimeFormat = "00:20:00";
        Assert.AreEqual(expectedTimeFormat, Utils.FormatTime(playerScore.timer));
    }
}
