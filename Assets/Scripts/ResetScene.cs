using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    public AnalyticsManager analyticsManager;

    public void resetScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //Analytics event - RESET SCENE
        analyticsManager.SendEvent("LEVEL1 RESET");

        Debug.Log("Reset Event!");

    }
}
