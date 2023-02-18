using UnityEngine;

public class SuperPowerGateOpening : MonoBehaviour
{
    public GameObject superKey;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && superKey.activeSelf)
        {
            Debug.Log("Super Gate Opened!");
            Destroy(GameObject.Find("SuperPowerKey"));
            Destroy(GameObject.Find("SuperPowerGate"));
        }
    }
}
