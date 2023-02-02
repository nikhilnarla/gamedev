using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jump;
    public float move;
    public bool isJumping = false;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * move, rb.velocity.y);

        if(Input.GetKeyDown("space") && !isJumping){
            rb.velocity =  new Vector2(rb.velocity.x, jump);
            isJumping = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.name == ("Jumpingtile")){
            rb.velocity = new Vector2(rb.velocity.x,jump*3);
        }

        if(other.gameObject.CompareTag("Ground")){
            isJumping = false;
        }
    }
}
