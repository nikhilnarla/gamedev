using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 8f;
    public float jump = 16f;
    public float move;
    private bool isFacingRight = true;

    public AnalyticsManager analyticsManager;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Rigidbody2D tile;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask buttonLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * move, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (IsOnButton())
        {
            tile.velocity =  new Vector2(0.0f, -1.0f);
        }
        else if  (!IsOnButton() && tile.position.y < 4.2f)   
        {
            //Debug.Log(tile.position.y);
            tile.velocity =  new Vector2(0.0f, 1.0f);
        }
        else
        {       
                tile.velocity = new Vector2(0.0f, 0.0f);
                tile.position = new Vector2(tile.position.x, 4.2f);
        }


        Flip();
        
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(move * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && move < 0f || !isFacingRight && move > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private bool IsOnButton()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, buttonLayer);
    }

    private void OnCollisionEnter2D(Collision2D other){

        if(other.gameObject.name == ("Jumpingtile")){
            rb.velocity = new Vector2(rb.velocity.x,jump*3);

            //Analytics event - used JumpTile
            analyticsManager.SendEvent("LEVEL1 JUMPTILE");

        }

         //Analytics : Temp END GAME for Analytics
         if (other.gameObject.name == ("EndGame"))
         {
            Debug.Log("Level 1 End");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

            //Analytics event - key Collected
            analyticsManager.SendEvent("LEVEL1 GAMEEND");
             //Desctroying end block so player can pass
            Destroy(GameObject.Find("EndGame"));

         }
    }

}
