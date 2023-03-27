using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovementC : MonoBehaviour
{
    public float speed;
    public float jump = 10f;
    public float move = 7f;
    public bool isJumping = false;
    public bool blockPushed = false;
    public bool green = true;
    public bool fandetected = false;
    public GameObject key;
    public GameObject keyLevel1;
    public Transform TunnelSpawnPoint;
    public AnalyticsManager analyticsManager;
    public GameObject player;
    public static bool eventLevelFlag = false;
    public static bool flag = false;
    public GameObject dialogue;

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

        flag = false;

        if(other.gameObject.name == ("Tile 4") && bridgeStatus.ContainsValue(false)){
            ShowTiles();
            dialogue.SetActive(true);
            analyticsManager.SendEvent("LEVEL3 PLAYER HIT GREEN BLOCK");
        }

        if(other.gameObject.name == ("Bridge Tile 0") || other.gameObject.name == ("Bridge Tile 1") || other.gameObject.name == ("Bridge Tile 2")){
               jump = 5f;
               speed = 8f;

        }

        // if(other.gameObject.tag== ("Trap (1)") ||  other.gameObject.tag == ("Traps (2)")  || other.gameObject.tag == ("Traps") ){
        //         analyticsManager.SendEvent("LEVEL3 PLAYER KILLED BY RED TRAPS");
        // }

        if(other.gameObject.name == ("Button 1")){
                AddGravityToTiles();
                DestroyBridgeTiles();
                Destroy(GameObject.Find("Gate"));
                analyticsManager.SendEvent("LEVEL3 PLAYER HIT GREEN GATE BUTTON AND OPENED GREEN GATE RIGHT");
        }

        if(other.gameObject.name == ("Button 2")){
            Destroy(GameObject.Find("EntryGate"));
            Destroy(GameObject.Find("Button 2"));
            analyticsManager.SendEvent("LEVEL3 PLAYER HIT YELLOW GATE BUTTON AND OPENED YELLOW GATE LEFT");
        }

        if (other.gameObject.tag == "Trap" && !flag)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            analyticsManager.SendEvent("LEVEL3 PLAYER KILLED BY SPIKES");
            analyticsManager.SendEvent("LEVEL3 GAMESTART AGAIN");
            flag = true;
            //player.gameObject.transform.position = TunnelSpawnPoint.position;
        }
        if (other.gameObject.tag == "TunnelGreenTrap" & !flag)
        {
             rb.gameObject.transform.position = TunnelSpawnPoint.position;
             analyticsManager.SendEvent("LEVEL3 PLAYER KILLED BY GREEN TUNNEL SPIKES AT POSITION:"+rb.position);
            flag = true;
            //player.gameObject.transform.position = TunnelSpawnPoint.position;
        }
        if (other.gameObject.tag == "TunnelYellowTrap" & !flag)
        {
             rb.gameObject.transform.position = TunnelSpawnPoint.position;
             analyticsManager.SendEvent("LEVEL3 PLAYER KILLED BY YELLOW TUNNEL SPIKES AT POSITION:"+rb.position);
            flag = true;
            //player.gameObject.transform.position = TunnelSpawnPoint.position;
        }
          if (other.gameObject.tag == "ExitTraps" & !flag)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
             analyticsManager.SendEvent("LEVEL3 PLAYER KILLED BY SPIKES IN HARD TUNNEL");
            flag = true;
            //player.gameObject.transform.position = TunnelSpawnPoint.position;
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
        //Enter yellow Tunnel
        if (other.gameObject.name == ("EndGameYellow"))
        {
            Debug.Log("Level 2 End");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+2);
            //Analytics event - key Collected
            analyticsManager.SendEvent("LEVEL3 YELLOW GATE USED");
             //Desctroying end block so player can pass
            Destroy(GameObject.Find("EndGameYellow"));
            analyticsManager.SendEvent("LEVEL3 GAMEEND");
        }

        //Enter Green Tunnel
        if (other.gameObject.name == ("EndGameGreen"))
        {
            Debug.Log("Level 2 End");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            //Analytics event - key Collected
            analyticsManager.SendEvent("LEVEL3 GREEN GATE USED");
             //Desctroying end block so player can pass
            Destroy(GameObject.Find("EndGameGreen"));
            analyticsManager.SendEvent("LEVEL3 GAMEEND");
        }

        //Exit Yellow Tunnel Level2
        if (other.gameObject.name == ("EndGateYellowLevel2"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            //Analytics event - key Collected
            analyticsManager.SendEvent("LEVEL3 YELLOW TUNNEL EXIT");
            //Desctroying end block so player can pass
            Destroy(GameObject.Find("EndGateYellowLevel2"));
            analyticsManager.SendEvent("LEVEL3 GAMEEND");
            analyticsManager.SendEvent("LEVEL6 GAMESTART");

        }
        //Exit Yellow Tunnel
        if (other.gameObject.name == ("ExitYellowTLevel3"))
        {
            Debug.Log("Exit Yellow Tunnel Level 3");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+2);
            Destroy(GameObject.Find("ExitYellowTLevel3"));
        }

        //Enter green Tunnel
        if (other.gameObject.name == ("EndGameLevel3"))
        {
            Debug.Log("Level 2 End");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+2);

             //Desctroying end block so player can pass
            Destroy(GameObject.Find("EndGameLevel3"));
            analyticsManager.SendEvent("LEVEL3 GREEN GATE USED");
            analyticsManager.SendEvent("LEVEL3 GAMEEND");

            analyticsManager.SendEvent("LEVEL6 GAMESTART");

        }
        
        //Exit Green Tunnel
        if (other.gameObject.name == ("GATEEND"))
        {
            Debug.Log("Exit Green Tunnel Level 3");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+2);

            //Analytics event - key Collected
            // analyticsManager.SendEvent("LEVEL3 GAMEEND");
             //Desctroying end block so player can pass
            Destroy(GameObject.Find("GATEEND"));
        }

    }

    private void OnCollisionExit2D(Collision2D other){

         if(other.gameObject.name == ("Bridge Tile 0") || other.gameObject.name == ("Bridge Tile 1") || other.gameObject.name == ("Bridge Tile 2")){
            jump = 10f;
            speed = 7f;
        }

         if(other.gameObject.name == ("Tile 4"))
        {
            dialogue.SetActive(false);
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
