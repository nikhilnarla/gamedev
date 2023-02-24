using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    [SerializeField]
    public Transform portal1Spawn, portal2Spawn;

    public Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Portal1")
        {
            transform.position = portal2Spawn.position;
        }

        if(collision.gameObject.name == "Portal2")
        {
            transform.position = portal1Spawn.position;
        }
    }

    
}
