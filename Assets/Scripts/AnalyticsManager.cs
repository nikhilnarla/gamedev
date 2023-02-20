using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class AnalyticsManager : MonoBehaviour
{

    private static string _url;
    private static string _sessionID;
    private static float currTime;

    private void Awake()
    {
        //AnalyticsManager = this;
        _url = "https://docs.google.com/forms/u/1/d/e/1FAIpQLScvomqdDXqomed_rlvKhLmn_8Ce6Pr0sgOWEfeRXWhmClxBXA/formResponse";

        Guid guid = Guid.NewGuid();

        _sessionID = PlayerPrefs.GetString("Player ID", guid.ToString());
        PlayerPrefs.SetString("Player ID", _sessionID);

    }

    // Start is called before the first frame update
    void Start()
    {
        currTime = 0;
        SendEvent("LEVEL1 GAMESTART");
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
      form.AddField("entry.566561242",_sessionID);
      form.AddField("entry.1956343257",eventType);
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
