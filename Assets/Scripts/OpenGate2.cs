using UnityEngine;

public class OpenGate2 : MonoBehaviour
{
    public GameObject frozenKey;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && frozenKey.activeSelf)
        {
            Debug.Log("Gate 2 Opened!");
            Destroy(GameObject.Find("Gate2OpenKey"));
            Destroy(GameObject.Find("Gate2"));
        }
    }

}
