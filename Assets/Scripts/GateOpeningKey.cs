using UnityEngine;

public class GateOpeningKey : MonoBehaviour
{
    public GameObject key;
    public AnalyticsManager analyticsManager;
   
    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.name == "Player" && key.activeSelf)
        {

            //Analytics event - key Collected
            analyticsManager.SendEvent("LEVEL1 KEYCOLLECTED");

            Debug.Log("collision key!");
            Destroy(GameObject.Find("Gate"));
            Destroy(GameObject.Find("Diamond"));   
        }
    }    
}
