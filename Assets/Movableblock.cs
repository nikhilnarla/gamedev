using UnityEngine;

public class Movableblock : MonoBehaviour
{

    public GameObject key;
    public AnalyticsManager analyticsManager;

    bool keyFound = false;
    
    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.name == "Collidertile")
        {
            Debug.Log("collision!");
            key.SetActive(true);
            GameObject.Find("diamond").GetComponent<SpriteRenderer>().enabled = true;

            if(!keyFound)
            {
            //Analytics event - found key
            analyticsManager.SendEvent("LEVEL1 KEYFOUND");
            keyFound = true;
            }
        }
    }

}
