using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Networking;
using UnityEditor;
using UnityEngine.SceneManagement;
 
// public class GetScenesNamesFromEditor
// {
//     [MenuItem( "Scenes Names/Save Scenes Names" )]
//     private static void GetScenesNames()
//     {
//         EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
//         for ( int i = 0 ; i &amp;amp;amp;lt; scenes.Length ; ++i )
//         {
//             if( scenes[i].enabled )
//                 Debug.Log( "Scene #" + i + " : " + scenes[i].path + " is enabled" );
//             else
//                Debug.LogWarning( "Scene #" + i + " : " + scenes[i].path + " is not enabled" );
//         }
//     }
// }

public class AnalyticsManager : MonoBehaviour
{

    private static string _url;
    private static string _sessionID;
    private static float currTime;
    public static string levelName;
    public static bool eventLevelFlag = false;

    private void Awake()
    {
        // Debug.Log("Easley");
        // Debug.Log("Awake:" + SceneManager.GetActiveScene().name);
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
        if(SceneManager.GetActiveScene().name == "Level6"){
            levelName = "LEVEL6";
            // SendEvent("LEVEL6 GAMESTART");
        }
        if(SceneManager.GetActiveScene().name == "Level-1 Updated"){
            levelName = "LEVEL1";
            // SendEvent("LEVEL1 GAMESTART");
        }
        if(SceneManager.GetActiveScene().name == "Level-3 Upgrade"){
            levelName = "LEVEL3";
            // SendEvent("LEVEL3 GAMESTART");
        }
        if(SceneManager.GetActiveScene().name == "Level7"){
            levelName = "LEVEL7";
            // SendEvent("LEVEL3 GAMESTART");
        }

        if(!eventLevelFlag){
        SendEvent(levelName + "GAMESTART");

        Debug.Log("Awake:" + ' ' + SceneManager.GetActiveScene().name);
        eventLevelFlag = true;
        }
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
