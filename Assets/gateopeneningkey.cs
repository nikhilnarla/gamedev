using UnityEngine;

public class gateopeneningkey : MonoBehaviour
{
    public GameObject key;
    public AnalyticsManager analyticsManager;
   
    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.name == "Capsule" && key.activeSelf)
        {

            //Analytics event - key Collected
            analyticsManager.SendEvent("LEVEL1 KEYCOLLECTED");

            Debug.Log("collision key!");
            Destroy(GameObject.Find("gate"));
            Destroy(GameObject.Find("diamond"));   
        }
    }    
}
