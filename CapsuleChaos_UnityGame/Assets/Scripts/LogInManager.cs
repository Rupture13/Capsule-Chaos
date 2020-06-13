using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LogInManager : MonoBehaviour
{
    [SerializeField]
    private string APIGWbaseUrl = "https://localhost";
    [SerializeField]
    private int APIGWport = 5010;

    [SerializeField]
    private PlayerInfo playerInfo = default;

    [SerializeField]
    private TMP_InputField inputUsername = default;
    [SerializeField]
    private TMP_InputField inputPassword = default;

    private Color neutralColour;
    private Color wrongColour;

    [SerializeField]
    private GameObject loginButtonText = default;
    [SerializeField]
    private GameObject loadingIcon = default;

    [SerializeField]
    private UnityEvent onLoginSuccess = default;

    private bool isWaiting;

    private void Start()
    {
        isWaiting = false;
        neutralColour = new Color(1, 1, 1, 0.831f);
        wrongColour = new Color(1, 0.713f, 0.713f, 0.831f);
    }

    public void StartLogin()
    {
        if (isWaiting) { return; }
        loginButtonText.SetActive(false);
        loadingIcon.SetActive(true);

        isWaiting = true;
        StartCoroutine(SendLoginWebRequest(inputUsername.text, inputPassword.text, OnLoginSuccess, OnLoginFail));
    }

    private void OnLoginSuccess(PlayerInformation currentPlayer)
    {
        isWaiting = false;
        playerInfo.Player = currentPlayer;
        
        loginButtonText.SetActive(true);
        loadingIcon.SetActive(false);
        inputUsername.GetComponent<Image>().color = neutralColour;
        inputPassword.GetComponent<Image>().color = neutralColour;
        onLoginSuccess.Invoke();
    }

    private void OnLoginFail(string error)
    {
        isWaiting = false;

        Debug.LogError(error);
        loginButtonText.SetActive(true);
        loadingIcon.SetActive(false);
        inputUsername.GetComponent<Image>().color = wrongColour;
        inputPassword.GetComponent<Image>().color = wrongColour;
    }

    IEnumerator SendLoginWebRequest(string username, string password, System.Action<PlayerInformation> onSuccess, System.Action<string> onFail)
    {
        using (UnityWebRequest req = UnityWebRequest.Get($"{APIGWbaseUrl}:{APIGWport}/api/accounting/Accounts/Login?username={username}&password={password}"))
        {
            yield return req.SendWebRequest();
            while (!req.isDone) { yield return null; }
            if (req.isNetworkError)
            {
                onFail(req.error);
            }
            else if (req.responseCode != 0 && req.responseCode != (long)System.Net.HttpStatusCode.OK)
            {
                onFail(req.responseCode.ToString());
            }
            else
            {
                byte[] result = req.downloadHandler.data;
                string playerJson = System.Text.Encoding.Default.GetString(result);
                PlayerInformation info = JsonUtility.FromJson<PlayerInformation>(playerJson);
                onSuccess(info);
            }
        }
    }
}
