using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementLaser : MonoBehaviour
{
    public float speed;
    public float jump;
    public float move;
    private float horizontal;
    private bool isFacingRight = true;
    public bool isJumping = false;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    bool obtainedBlueKey = false;

    // public float defaultFriction = 0f; // default friction when not sticking to tiles
    // public float highFriction = 100f; // high friction when sticking to tiles

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); // returns value of -1,0,+1 depending on the direction we are moving
        rb.velocity = new Vector2(speed * move, rb.velocity.y); // Move the player left or right based on input

        if(Input.GetKeyDown("space") && !isJumping){
            rb.velocity =  new Vector2(rb.velocity.x, jump);
            isJumping = true;
        }

        if(Input.GetKeyDown("space") && IsGrounded()){
            rb.velocity =  new Vector2(rb.velocity.x, jump);
            isJumping = true;
        }

        // If the player is on the ground, reset isJumping to false
        if (IsGrounded())
        {
            isJumping = false;
        }

        Flip();
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y); // x component velocity, y component velocity  
    }

    private bool IsGrounded() {
        Collider2D groundCollider = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        return groundCollider;
    }   

    private void Flip() {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) { // conditions to want to flip player
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f; // x component of player's local scale
            transform.localScale = localScale;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
        if (other.gameObject.name == "Obstacle")
        {
            // if player is hit by obstacle, then it respawns to back at beginning of level
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if(other.gameObject.name == "Blue Key")
        {
           Destroy(GameObject.Find("Blue Key"));
           // something happens when blue key is obtained
           obtainedBlueKey = true;
        }
    }

    void AddGravityToTiles()
    {
        Rigidbody2D tile = null;
        for(int i=0; i<7; i+=1)
        {
            tile = GameObject.Find("Tile "+i).GetComponent<Rigidbody2D>();
            tile.gravityScale = 1;
        }
    }

}
