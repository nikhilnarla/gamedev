using UnityEngine;

public class GateOpeningKey : MonoBehaviour
{
    public GameObject key;
    public DetectBlock detectBlock;
    public AnalyticsManager analyticsManager;

    void OnCollisionEnter2D(Collision2D col)
    {
        
        if ((col.gameObject.name == "Capsule" || col.gameObject.name == "Player"  ) && key.activeSelf)
        {
            Debug.Log("collision key!");
            analyticsManager.SendEvent("LEVEL1 KEYCOLLECTED");
            Destroy(GameObject.Find("Gate"));
            Destroy(GameObject.Find("Diamond"));
            detectBlock.keyFound(true);
        }
    }
}
