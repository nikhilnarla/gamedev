using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jump;
    public float move;
    public bool isJumping = false;
    public bool blockPushed = false;
    public bool green = true;
    public bool fandetected = false;
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
              GameObject.Find("Jumpingtile").GetComponent<SpriteRenderer>().enabled = true;
              analyticsManager.SendEvent("LEVEL1 JUMPTILE");

        }

        if(other.gameObject.CompareTag("Ground")){
            isJumping = false;
        }

        if(other.gameObject.name == "GreenBlock"){
           var objRenderer = GameObject.Find("GreenBlock").GetComponent<Renderer>();
           objRenderer.material.SetColor("_Color", Color.green);

           var Block = other.gameObject.GetComponent<Rigidbody2D>();
           Block.mass = 5.0F;
           Block.angularDrag = 0.05F;
           Block.gravityScale = 1.0f;

           var objRenderer2 = GameObject.Find("Collidertile").GetComponent<Renderer>();
           objRenderer2.material.SetColor("_Color", Color.green);
        }
    }



}
