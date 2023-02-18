using UnityEngine;

public class GetSuperKey : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(GameObject.Find("SuperKey"));
        }
    }
}
