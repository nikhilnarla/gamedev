using UnityEngine;

public class PlayerMovement6 : MonoBehaviour
{

    private Rigidbody2D rb;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private Transform portal1Spawning, portal2Spawning;
     public AnalyticsManager analyticsManager;


    public float speed;
    public float jump;
    public float move;
    public bool isJumping = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //IntializeBridgeTiles();
    }

    void Update()
    {
        move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * move, rb.velocity.y);

        if (Input.GetKeyDown("space") && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            isJumping = true;
        }

        if (Input.GetKeyDown("space") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            isJumping = true;
        }

    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
        if (other.gameObject.name == ("Portal1"))
        {
            analyticsManager.SendEvent("LEVEL6 PORTAL1 USED");
        }
        if (other.gameObject.name == ("Portal2"))
        {
            analyticsManager.SendEvent("LEVEL6 PORTAL2 USED");
        }
         if (other.gameObject.name == ("LavaCamera"))
        {
            analyticsManager.SendEvent("LEVEL6 PLAYER FELL INTO LAVA");
        }
        //   if (other.gameObject.name == ("FallingBlock1"))
        // {
        //     analyticsManager.SendEvent("LEVEL6 PLAYER USED ROTATING BLOCK 1");
        // }
        //      if (other.gameObject.name == ("FallingBlock2"))
        // {
        //     analyticsManager.SendEvent("LEVEL6 PLAYER USED ROTATING BLOCK 2");
        // }
         if (other.gameObject.name == ("Ground"))
        {
            isJumping = false;
        }
        if (other.gameObject.name == "Portal1")
        {
            rb.transform.position = portal2Spawning.transform.position;
        }
        if (other.gameObject.name == "Portal2")
        {
            rb.transform.position = portal1Spawning.transform.position;
        }
    }
}
