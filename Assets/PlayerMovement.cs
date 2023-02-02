using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jump;
    public float move;
    public bool isJumping = false;

    public AnalyticsManager analyticsManager;

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
            
            //Analytics event - used JumpTile
            analyticsManager.SendEvent("LEVEL1 JUMPTILE");

        }

        if(other.gameObject.CompareTag("Ground")){
            isJumping = false;
        }
      
         //Analytics : Temp END GAME for Analytics
         if (other.gameObject.name == ("ENDGAME"))
         {
             Debug.Log("Level 1 End");

             //Analytics event - key Collected
             analyticsManager.SendEvent("LEVEL1 GAMEEND");

         }
    }
    
}
