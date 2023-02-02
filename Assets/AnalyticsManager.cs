// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AnalyticsManager : MonoBehaviour
{

    private static string _url;
    private static string _sessionID;
    private static float currTime;

    //public GameObject redPlatform;
    //public GameObject bluePlatform;

    //public int redCube;
    //public int blueCube;
    //public int redPlatform;
    //public int bluePlatform;

    private void Awake()
    {
        //AnalyticsManager = this;
        _url = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfsDeCYjE6xs9le06cgd-Lz6-HzY4IaoAKxNWatBD6Oa7Dx6A/formResponse";

        Guid guid = Guid.NewGuid();

        _sessionID = PlayerPrefs.GetString("Player ID", guid.ToString());
        PlayerPrefs.SetString("Player ID", _sessionID);

    }

    // Start is called before the first frame update
    void Start()
    {
        currTime = 0;
        SendEvent("Game Start");
    }

    // Update is called once per frame
    void Update()
    {
      currTime += (1 * Time.deltaTime);
      if (currTime >= 15)
      {
        //analyticsManager.SendEvent("Time Interval");
        currTime = 0;
      }
    }

    public void SendEvent(string eventType){
      WWWForm form = new WWWForm();
      form.AddField("entry.464988709",_sessionID);
      form.AddField("entry.1821099035",eventType);
      StartCoroutine(SendData(form));
    }

    IEnumerator SendData(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(_url, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Event reported");
            }
        }
    }
}