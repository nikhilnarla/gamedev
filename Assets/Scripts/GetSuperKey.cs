using UnityEngine;

public class GetSuperKey : MonoBehaviour
{
    public AnalyticsManager analyticsManager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(GameObject.Find("SuperKey"));
            //  analyticsManager.SendEvent("LEVEL6 SUPER KEY COLLECTED");
        }

        // if(collision.gameObject.tag == "SuperKey")
        // {
        //      analyticsManager.SendEvent("LEVEL6 SUPER KEY COLLECTED");
        // }
    }
}
