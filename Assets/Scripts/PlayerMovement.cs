using UnityEngine;
using UnityEngine.SceneManagement;

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

        if (other.gameObject.tag == "Trap")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (other.gameObject.name == ("Jumpingtile")){
            rb.velocity = new Vector2(rb.velocity.x,jump*3);

            //Analytics event - used JumpTile
            analyticsManager.SendEvent("LEVEL1 JUMPTILE");

        }

        if(other.gameObject.CompareTag("Ground")){
            isJumping = false;
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

         if(other.gameObject.name == "BlackPortal1")
        {
            rb.transform.position = new Vector2( 6.5f, -1.77f);
        }

        if (other.gameObject.name == "BlackPortal2")
        {
            rb.transform.position = new Vector2(-6.5f, 3.33f);
        }
        if (other.gameObject.name == "OrangePortal1")
        {
            rb.transform.position = new Vector2(6.5f, 2.68f);
                
        }
        if (other.gameObject.name == "OrangePortal2")
        {
            rb.transform.position = new Vector2(-6.5f, -1.59f);
        }
    }

}
