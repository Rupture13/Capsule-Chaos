using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APICaller : MonoBehaviour
{
    public string port;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            //Debug.Log("API Call sent.");
            //StartCoroutine(GetRequest("https://localhost:" + port + "/weatherforecast"));
            StartCoroutine(PostRequest("https://localhost:" + port + "/weatherforecast", "hello there"));
        }
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                //Debug.Log(pages[page] + ": Error: " + webRequest.error);

                
            }
            else
            {
                //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                
                byte[] result = webRequest.downloadHandler.data;
                string weatherJSON = System.Text.Encoding.Default.GetString(result);
                //Debug.Log(weatherJSON);
            }
        }
    }

    IEnumerator PostRequest(string uri, string postData)
    {
        //WWWForm form = new WWWForm();
        //form.AddField("Date", DateTime.Now.ToString());
        //form.AddField("TemperatureC", 20);
        //form.AddField("TemperatureF", 32 + (int)(20 / 0.5556));
        //form.AddField("Summary", postData);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, postData))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                //Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);

                byte[] result = webRequest.downloadHandler.data;
                string weatherJSON = System.Text.Encoding.Default.GetString(result);
                //Debug.Log(weatherJSON);
            }
        }
    }
}
