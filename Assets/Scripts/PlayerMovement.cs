using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jump;
    public float move;
    public bool isJumping = false;
    public GameObject key;

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

        //if (other.gameObject.tag == "KeyFinder")

        //{
        //    Debug.Log("keyfinder");
        //    //key.SetActive(true);
        //    GameObject.Find("Diamond").GetComponent<SpriteRenderer>().enabled = true;

        //    Invoke("setKeyActive", 3.0f);
        //}


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
    }

    //public void setKeyActive()
    //{
    //    //key.SetActive(false);
    //    GameObject.Find("Diamond").GetComponent<SpriteRenderer>().enabled = false;
    //}

}
