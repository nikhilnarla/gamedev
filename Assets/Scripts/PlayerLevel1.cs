using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerLevel1 : MonoBehaviour
{
    private Vector2 target;

    public float speed;
    public float jump;
    public float move;
    public bool isJumping = false;
    public bool blockPushed = false;
    public bool green = true;
    public bool inMotion = false;
    public bool flag = false;
    public GameObject key;
    public GameObject keyLevel1;
    public Transform Tunnel1SpawnPoint;
    public Transform Tunnel2SpawnPoint;
    

    public AnalyticsManager analyticsManager;

    Dictionary<string, bool> bridgeStatus = new Dictionary<string, bool>();

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask buttonLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 4f;
        jump = 9f;
        RenderKeys(false);
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

        if(Input.GetAxis("Horizontal") != 0){
            inMotion = false;
            rb.gravityScale = 1;
        }

        if(inMotion){
            rb.gravityScale = 0;
            transform.position = Vector2.MoveTowards(transform.position, target, 15*Time.deltaTime);
        }

        if(target == new Vector2(rb.transform.position.x, rb.transform.position.y)){
            rb.gravityScale = 1;
            inMotion = false;
        }

    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D other){
        flag = false;
        if (other.gameObject.tag == "TunnelGreenTrap" && !flag)
        {
            // analyticsManager.SendEvent("LEVEL1 PLAYER KILLED BY SPIKES IN GREEN GATE TUNNEL");
            rb.gameObject.transform.position = Tunnel2SpawnPoint.position;
            analyticsManager.SendEvent("LEVEL1 PLAYER KILLED BY SPIKES IN GREEN GATE TUNNEL AT POSITION:" + rb.position);
            flag = true;

        }
        if (other.gameObject.tag == "TunnelYellowTrap" && !flag)
        {
            // analyticsManager.SendEvent("LEVEL1 PLAYER KILLED BY SPIKES IN YELLOW GATE TUNNEL");
            rb.gameObject.transform.position = Tunnel1SpawnPoint.position;
            analyticsManager.SendEvent("LEVEL1 PLAYER KILLED BY SPIKES IN YELLOW GATE TUNNEL AT POSITION:" + rb.position);
            flag = true;

        }
        if (other.gameObject.name == ("Button 2")){
                Destroy(GameObject.Find("EntryGate"));
        }

        if (other.gameObject.name == ("Tile 2"))
        {
            analyticsManager.SendEvent("LEVEL1 JUMP PAD INTERACTED");
            inMotion = true;
            target = other.gameObject.GetComponent<JumpingPadScript>().target;
        }

        if(other.gameObject.name == "Green Block" && other.gameObject.GetComponent<Renderer>().material.color != Color.green)
        {
           var objRenderer = GameObject.Find("Green Block").GetComponent<Renderer>();
           objRenderer.material.SetColor("_Color", Color.green);
           Debug.Log("green block");
           analyticsManager.SendEvent("LEVEL1 PLAYER INTERACTED WITH THE GREEN BLOCK");
           var Block = other.gameObject.GetComponent<Rigidbody2D>();
           Block.mass = 5.0f;
           Block.angularDrag = 0.05f;
           Block.gravityScale = 1.0f;

           var objRenderer2 = GameObject.Find("Collider Tile").GetComponent<Renderer>();
           objRenderer2.material.SetColor("_Color", Color.green);
           RenderKeys(true);
        }

        if(other.gameObject.name == "Collider Tile" && other.gameObject.GetComponent<Renderer>().material.color != Color.green)
        {
            RenderKeys(true);
            var objRenderer2 = other.gameObject.GetComponent<Renderer>();
            objRenderer2.material.SetColor("_Color", Color.green);
        }

        if(other.gameObject.name == "Key 1")
        {
            var gate = GameObject.Find("EntryGate");
            analyticsManager.SendEvent("LEVEL1 YELLOW GATE UNLOCKED");

            Destroy(gate);
            Destroy(other.gameObject);
        }

        if(other.gameObject.name == "Key 2")
        {
            var gate = GameObject.Find("Gate");
            analyticsManager.SendEvent("LEVEL1 GREEN GATE UNLOCKED");


            Destroy(gate);
            Destroy(other.gameObject);
        }

        //Analytics : Temp END GAME for Analytics
        if (other.gameObject.name == ("EndGate1"))
        {
            Debug.Log("Level 1 End");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            // analyticsManager.SendEvent("EXIT GATE 1");
            //Analytics event - key Collected
            analyticsManager.SendEvent("LEVEL1 YELLOW GATE USED");
            Destroy(GameObject.Find("EndGate1"));
            analyticsManager.SendEvent("LEVEL1 GAMEEND");
            analyticsManager.SendEvent("LEVEL3 GAMESTART");
            //Desctroying end block so player can pass


        }
        if (other.gameObject.name == ("EndGate2"))
        {
            Debug.Log("Level 1 End");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            // analyticsManager.SendEvent("EXIT GATE 2");
            //Analytics event - key Collected
            analyticsManager.SendEvent("LEVEL1 GREEN GATE USED");
            Destroy(GameObject.Find("EndGate2"));
            analyticsManager.SendEvent("LEVEL1 GAMEEND");
            analyticsManager.SendEvent("LEVEL3 GAMESTART");
            //Desctroying end block so player can pass


        }

    }

    private void OnCollisionExit2D(Collision2D other){

        var objRenderer = GameObject.Find("Green Block").GetComponent<Renderer>();

        if(other.gameObject.name == "Collider Tile" && objRenderer.material.color != Color.green)
        {
            RenderKeys(false);

            var objRenderer2 = other.gameObject.GetComponent<Renderer>();
            objRenderer2.material.SetColor("_Color", Color.white);
        }

    }

    void RenderKeys(bool val){
        var keyRenderer1 =  GameObject.Find("Key 1").GetComponent<Renderer>();
        var keyRenderer2 =  GameObject.Find("Key 2").GetComponent<Renderer>();

        keyRenderer1.enabled = val;
        keyRenderer1.GetComponent<BoxCollider2D>().enabled = val;

        keyRenderer2.enabled = val;
        keyRenderer2.GetComponent<BoxCollider2D>().enabled = val;
    }

}
