using UnityEngine;

public class SuperPowerGateOpening : MonoBehaviour
{
    public GameObject superKey;
    public AnalyticsManager analyticsManager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && superKey.activeSelf)
        {
            Debug.Log("Super Gate Opened!");
            Destroy(GameObject.Find("SuperPowerKey"));
            Destroy(GameObject.Find("SuperPowerGate"));
            // analyticsManager.SendEvent("LEVEL6 SUPER POWER GATE OPENING KEY COLLECTED");
        }
        // if(collision.gameObject.name == "SuperPowerKey")
        // {
        // analyticsManager.SendEvent("LEVEL6 SUPER POWER GATE OPENING KEY COLLECTED");
        // }
    }
}
