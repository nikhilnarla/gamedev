using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jump;
    public float move;
    public bool isJumping = false;
    public bool blockPushed = false;
    public bool green = true;
    public bool fandetected = false;
    public GameObject key;
    public GameObject keyLevel1;

    public bool greenblockhit = true;

    public AnalyticsManager analyticsManager;

    Dictionary<string, bool> bridgeStatus = new Dictionary<string, bool>();

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask buttonLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        IntializeBridgeTiles();
    }

    void Update()
    {
        move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * move, rb.velocity.y);

        if(Input.GetKeyDown("space") && !isJumping){
            rb.velocity =  new Vector2(rb.velocity.x, jump);
            isJumping = true;
        }

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

        if(other.gameObject.name == ("Tile 5") && bridgeStatus.ContainsValue(false)){
                ShowTiles();
        }

         if(other.gameObject.name == ("Button")){
                analyticsManager.SendEvent("LEVEL2 BUTTON WAS TOUCHED BY PLAYER");
                AddGravityToTiles();
                DestroyBridgeTiles();
        }

        if (other.gameObject.tag == "Trap")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (other.gameObject.tag == "Sharp")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(other.gameObject.name == "Tile")
        {
            GameObject.Find("Tile").GetComponent<SpriteRenderer>().enabled = true;
        }
        if (other.gameObject.name == ("Jumpingtile"))
        {
            rb.velocity = new Vector2(rb.velocity.x,jump*3);
        }
        if(other.gameObject.name == ("JumpingtileLevel1"))
        {
            rb.velocity = new Vector2(rb.velocity.x,jump*3);
            analyticsManager.SendEvent("LEVEL1 JUMPTILE");
        }
        if(other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("MovingPlatform"))
        {
            isJumping = false;
        }
        if(other.gameObject.name == "GreenBlockLevel1")
        {
           var objRenderer = GameObject.Find("GreenBlockLevel1").GetComponent<Renderer>();
           objRenderer.material.SetColor("_Color", Color.green);
           if(greenblockhit){
           analyticsManager.SendEvent("LEVEL1 GREEN BLOCK DETECTED BY PLAYER");
           greenblockhit = false;
           }
           var Block = other.gameObject.GetComponent<Rigidbody2D>();
           Block.mass = 5.0F;
           Block.angularDrag = 0.05F;
           Block.gravityScale = 1.0f;

           var objRenderer2 = GameObject.Find("CollidertileLevel1").GetComponent<Renderer>();
           objRenderer2.material.SetColor("_Color", Color.green);
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
            analyticsManager.SendEvent("LEVEL2 ABLE TO ENTER BLACK PORTAL1");
        }
        if (other.gameObject.name == "BlackPortal2")
        {
            rb.transform.position = new Vector2(-6.5f, 3.33f);
             analyticsManager.SendEvent("LEVEL2 ABLE TO ENTER BLACK PORTAL2");
        }
        if (other.gameObject.name == "OrangePortal1")
        {
            rb.transform.position = new Vector2(6.5f, 2.68f);
             analyticsManager.SendEvent("LEVEL2 ABLE TO ENTER ORANGE PORTAL1");
        }
        if (other.gameObject.name == "OrangePortal2")
        {
            rb.transform.position = new Vector2(-6.5f, -1.59f);
            analyticsManager.SendEvent("LEVEL2 ABLE TO ENTER ORANGE PORTAL2");
        }

    }

    private void IntializeBridgeTiles(){
        ShowTiles();
    }

    private void ShowTiles(){

        for (int i = 0; i<3; i+=1)
        {
            var bridgeTile = GameObject.Find("Bridge Tile "+i).GetComponent<Renderer>();
            bridgeTile.enabled = !bridgeTile.enabled;

            var collider = bridgeTile.GetComponent<BoxCollider2D>();
            collider.enabled = !collider.enabled;

            bridgeStatus["Bridge Tile "+i] = bridgeTile.enabled;
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

    void DestroyBridgeTiles()
    {
        for (int i = 0; i<3; i+=1)
        {
            var bridgeTile = GameObject.Find("Bridge Tile "+i);
            Destroy(bridgeTile);
        }
    }

}
