using UnityEngine;

public class GateOpeningKeyLevel1 : MonoBehaviour
{
    public GameObject keyLevel1;
    public DetectBlockLevel1 detectBlockLevel1;
    public AnalyticsManagerLevel1 analyticsManagerLevel1;

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.name == "Player" && keyLevel1.activeSelf)
        {
            Debug.Log("collision key!");
            detectBlockLevel1.keyFound(true);
            analyticsManagerLevel1.SendEvent("LEVEL1 KEYCOLLECTED");
            Destroy(GameObject.Find("GateLevel1"));
            Destroy(GameObject.Find("DiamondLevel1"));
        }
    }
}
