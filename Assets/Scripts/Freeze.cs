using UnityEngine;

public class Freeze : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Equals("MovingGround") || collision.gameObject.name.Equals("Tile1"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
