using UnityEngine;

public class OpenGate2 : MonoBehaviour
{
    public GameObject frozenKey;
    public DoorBehaviour dbL3GG;

     public AudioSource YellowKeyCollectAudioSource;
    public AudioClip KeyCollectSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && frozenKey.activeSelf)
        {

            YellowKeyCollectAudioSource.clip = KeyCollectSound;
            YellowKeyCollectAudioSource.Play();
            Debug.Log("Gate 2 Opened!");
            Destroy(GameObject.Find("Gate2OpenKey"));
            dbL3GG._isLevel3GreenDoorOpen = true;
            //Destroy(GameObject.Find("Gate2"));
        }
    }

}
