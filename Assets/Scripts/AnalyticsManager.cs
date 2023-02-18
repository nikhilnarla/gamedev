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
        _url = "https://docs.google.com/forms/u/2/d/e/1FAIpQLSdygvCLA1t6o1dtJM3YBtcksQ2kRJMX0sXOpOK54Nz7t60xAg/formResponse";

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
      form.AddField("entry.954345209",_sessionID);
      form.AddField("entry.1718950968",eventType);
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
