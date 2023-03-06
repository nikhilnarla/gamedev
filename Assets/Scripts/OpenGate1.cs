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
            Destroy(GameObject.Find("Wall10"));
            analyticsManager.SendEvent("LEVEL6 GREEN GATE OPEN KEY COLLECTED AND GREEN GATE  OPENED"); // analytics
        }
    }
}
