using UnityEngine;

public class OpenGate2 : MonoBehaviour
{
    public GameObject frozenKey;
    public DoorBehaviour dBL3GG;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && frozenKey.activeSelf)
        {
            Debug.Log("Gate 2 Opened!");
            dBL3GG._isLevel3GreenDoorOpen = true;
            Destroy(GameObject.Find("Gate2OpenKey"));
            //Destroy(GameObject.Find("Gate2"));
        }
    }

}
