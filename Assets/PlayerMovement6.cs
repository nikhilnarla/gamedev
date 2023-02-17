using UnityEngine;

public class PlayerMovement6 : MonoBehaviour
{

    private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public float speed;
    public float jump;
    public float move;
    public bool isJumping = false;
    public bool showText = false;

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
        if (other.gameObject.name == "Portal1")
        {
            rb.transform.position = new Vector2(4.7f, 2.8f);
        }
        if (other.gameObject.name == "Portal2")
        {
            rb.transform.position = new Vector2(-2.8f, -0.671f);
        }

        if(other.gameObject.name == "SuperPowerKey")
        {
            Destroy(GameObject.Find("SuperPowerKey"));
            Destroy(GameObject.Find("SuperPowerGate"));
        }

        if(other.gameObject.name == "SuperKey")
        {
            Destroy(GameObject.Find("SuperKey"));
            showText = true;
        }

        
    }
    void OnGUI()
     {
        if(showText)
            GUI.Label(new Rect(425, 20, 100, 100), "Powerup Collected, Press C to shoot bullets!","black");
     }
}
