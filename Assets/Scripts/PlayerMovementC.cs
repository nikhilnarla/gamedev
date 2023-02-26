using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovementC : MonoBehaviour
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
    public Transform TunnelSpawnPoint;
    public AnalyticsManager analyticsManager;
    public GameObject player;

    Dictionary<string, bool> bridgeStatus = new Dictionary<string, bool>();

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Rigidbody2D rbspikes;
    [SerializeField] private Rigidbody2D rbspikes1;
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

        if(other.gameObject.name == ("Tile 4") && bridgeStatus.ContainsValue(false)){
                ShowTiles();
        }

        if(other.gameObject.name == ("Bridge Tile 0") || other.gameObject.name == ("Bridge Tile 1") || other.gameObject.name == ("Bridge Tile 2")){
               jump = 3f;
               speed = 4f;
        }

        if(other.gameObject.name == ("Button 1")){
                AddGravityToTiles();
                DestroyBridgeTiles();
                Destroy(GameObject.Find("Gate"));
        }

        if(other.gameObject.name == ("Button 2")){
                Destroy(GameObject.Find("EntryGate"));
        }

        if (other.gameObject.tag == "Trap")
        {
             player.gameObject.transform.position = TunnelSpawnPoint.position;
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
            Debug.Log("Level 2 End");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

            //Analytics event - key Collected
            analyticsManager.SendEvent("LEVEL2 GAMEEND");
             //Desctroying end block so player can pass
            Destroy(GameObject.Find("EndGame"));

        }
        if (other.gameObject.name == ("EndGameLevel3"))
        {
            Debug.Log("Level 2 End");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+2);

            //Analytics event - key Collected
            analyticsManager.SendEvent("LEVEL2 GAMEEND");
             //Desctroying end block so player can pass
            Destroy(GameObject.Find("EndGameLevel3"));

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

    private void OnCollisionExit2D(Collision2D other){

         if(other.gameObject.name == ("Bridge Tile 0") || other.gameObject.name == ("Bridge Tile 1") || other.gameObject.name == ("Bridge Tile 2")){
               jump = 7f;
               speed = 4f;
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
        for(int i=0; i<6; i+=1)
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

    // void RemoveTraps(){
    //     var traps = GameObject.FindGameObjectsWithTag("Trap");

    //     foreach(GameObject trap in traps){
    //         Destroy(trap);
    //     }
    // }

}
