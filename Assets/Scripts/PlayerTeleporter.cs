using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    [SerializeField]
    public Transform portal1Spawn, portal2Spawn;

    public AnalyticsManager analyticsManager;
  public AudioSource GreenKeyCollectAudioSource;

    public AudioClip KeyCollectSound;
    public Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Portal1")
        {
                GreenKeyCollectAudioSource.clip = KeyCollectSound;
            GreenKeyCollectAudioSource.Play();
            transform.position = portal2Spawn.position;
            analyticsManager.SendEvent("LEVEL6 PORTAL1 USED");
        }

        if (collision.gameObject.name == "Portal2")
        {
              GreenKeyCollectAudioSource.clip = KeyCollectSound;
            GreenKeyCollectAudioSource.Play();
            transform.position = portal1Spawn.position;
            analyticsManager.SendEvent("LEVEL6 PORTAL2 USED");
        }
    }
}
