using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    [SerializeField]
    public Transform portal1Spawn, portal2Spawn;

    public AnalyticsManager analyticsManager;

    public Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Portal1")
        {
            // analyticsManager.SendEvent("LEVEL6 PORTAL1 USED");
            transform.position = portal2Spawn.position;
            collision.gameObject.GetComponent<PlayerMovement6>().move = 0.5f;
        }

        if(collision.gameObject.name == "Portal2")
        {
            // analyticsManager.SendEvent("LEVEL6 PORTAL2 USED");
            transform.position = portal1Spawn.position;
            collision.gameObject.GetComponent<PlayerMovement6>().move = 2f;
        }
    }
}
