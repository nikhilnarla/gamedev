using UnityEngine;

public class OpenGate1 : MonoBehaviour
{
    public AnalyticsManager analyticsManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Gate 1 Opened!");
            Destroy(GameObject.Find("Gate1OpenKey"));
            Destroy(GameObject.Find("Gate1"));
            analyticsManager.SendEvent("LEVEL6 GATE1 OPENED");
        }
    }
}
