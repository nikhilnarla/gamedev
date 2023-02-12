using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jump;
    public float move;
    public bool isJumping = false;

    public AnalyticsManager analyticsManager;

    [SerializeField] private Rigidbody2D rb;
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

        if(Input.GetKeyDown("space") && IsGrounded()){
            rb.velocity =  new Vector2(rb.velocity.x, jump);
            isJumping = true;
        }
    }

     private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D other){

        if(other.gameObject.name == ("Jumpingtile")){
            rb.velocity = new Vector2(rb.velocity.x,jump*3);

            //Analytics event - used JumpTile
            analyticsManager.SendEvent("LEVEL1 JUMPTILE");

        }

        if(other.gameObject.name == ("SpecialTile")){
                showTiles();
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

    private void showTiles(){
        
    }

}
