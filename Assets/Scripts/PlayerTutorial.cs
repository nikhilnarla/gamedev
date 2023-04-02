using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;

public class PlayerTutorial : MonoBehaviour
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
    public GameObject dialogue;

    //public GameObject getKeyDialogue;

    public AnalyticsManager analyticsManager;

    public Animator transition;

    //Dictionary<string, bool> bridgeStatus = new Dictionary<string, bool>();

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask buttonLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transition = GameObject.Find("Canvas").GetComponent<Animator>();
        //speed = 4f;
        //jump = 9f;
        dialogue.SetActive(true);
    }

    void Update()
    {
        move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * move, rb.velocity.y);
        
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
            transform.position = Vector2.MoveTowards(transform.position, target, 10*Time.deltaTime);
        }

        if(target == new Vector2(rb.transform.position.x, rb.transform.position.y)){
            rb.gravityScale = 1;
            inMotion = false;
        }

        StartCoroutine(WaitAndDisappear(3f));
        
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D other){
        //flag = false;

        //if(other.gameObject.name.Equals("Bottom"))
        //{
        //    dialogue.SetActive(true);
        //}
        //if (other.gameObject.tag == "TunnelGreenTrap" && !flag)
        //{
        //    rb.gameObject.transform.position = Tunnel2SpawnPoint.position;
        //    analyticsManager.SendEvent("LEVEL1 PLAYER KILLED BY SPIKES IN GREEN GATE TUNNEL AT POSITION:" + rb.position);
        //    flag = true;

        //}
        //if (other.gameObject.tag == "TunnelYellowTrap" && !flag)
        //{
        //    rb.gameObject.transform.position = Tunnel1SpawnPoint.position;
        //    analyticsManager.SendEvent("LEVEL1 PLAYER KILLED BY SPIKES IN YELLOW GATE TUNNEL AT POSITION:" + rb.position);
        //    flag = true;

        //}
        //if (other.gameObject.tag == "Trap" && !flag)
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //    flag = true;
        //}

        if (other.gameObject.name == ("Button 1"))
        {
            AddGravityToTiles();
        }

        if (other.gameObject.name == ("Top"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jump * 3);
        }

        //if(other.gameObject.name.Equals("SteppingTile"))
        //{
        //    Debug.Log("stepping");
        //    dialogue.SetActive(false);
        //}

        if (other.gameObject.name == "Green Block")
        {
            var objRenderer = GameObject.Find("Green Block").GetComponent<Renderer>();
            objRenderer.material.SetColor("_Color", Color.green);
            Debug.Log("green block");
            //analyticsManager.SendEvent("LEVEL1 PLAYER INTERACTED WITH THE GREEN BLOCK");
            var Block = other.gameObject.GetComponent<Rigidbody2D>();
            Block.mass = 500.0f;
            Block.angularDrag = 0.05f;
            Block.gravityScale = 1.0f;

            //var objRenderer2 = GameObject.Find("Collider Tile").GetComponent<Renderer>();
            //objRenderer2.material.SetColor("_Color", Color.green);
            //RenderKeys(true);

            //getKeyDialogue.SetActive(false);

        }

        //if (other.gameObject.name == "Collider Tile" && other.gameObject.GetComponent<Renderer>().material.color != Color.green)
        //{
        //    RenderKeys(true);
        //    var objRenderer2 = other.gameObject.GetComponent<Renderer>();
        //    objRenderer2.material.SetColor("_Color", Color.green);

            
        //}

        

        //if (other.gameObject.name == "Key 1")
        //{
        //    var gate = GameObject.Find("EntryGate");

        //    analyticsManager.SendEvent("LEVEL1 YELLOW GATE UNLOCKED");

        //    Destroy(gate);
        //    Destroy(other.gameObject);
        //}

        //if(other.gameObject.name == "Key 2")
        //{
        //    var gate = GameObject.Find("Gate");

        //    analyticsManager.SendEvent("LEVEL1 GREEN GATE UNLOCKED");

        //    Destroy(gate);
        //    Destroy(other.gameObject);
        //}

        //Enter Level1 yellow Tunnel
        //if (other.gameObject.name == ("EndGate1"))
        //{
        //    Debug.Log("Level 1 End");
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        //    Destroy(GameObject.Find("EndGate1"));

        //    analyticsManager.SendEvent("LEVEL1 YELLOW GATE USED");
        //    analyticsManager.SendEvent("LEVEL1 GAMEEND");
        //    analyticsManager.SendEvent("LEVEL3 GAMESTART");
        //}
        ////Exit Yellow Tunnel LEVEL1
        //if (other.gameObject.name == ("ExitYellowTunnelLevel1"))
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+2);
        //    Destroy(GameObject.Find("ExitYellowTunnelLevel1"));
        //}
        ////Exit Green Tunnel LEVEL1
        //if (other.gameObject.name == ("ExitGreenTunnel"))
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        //    Destroy(GameObject.Find("ExitGreenTunnel"));
        //}

        //if (other.gameObject.name == ("EndGate2"))
        //{
        //    Debug.Log("Level 1 End");
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+2);
        //    Destroy(GameObject.Find("EndGate2"));

            

        //    analyticsManager.SendEvent("LEVEL1 GREEN GATE USED");
        //    analyticsManager.SendEvent("LEVEL1 GAMEEND");
        //    analyticsManager.SendEvent("LEVEL3 GAMESTART");
        //}

    }

    //private void OnCollisionExit2D(Collision2D other){

    //    var objRenderer = GameObject.Find("Green Block").GetComponent<Renderer>();

    //    if(other.gameObject.name == "Collider Tile" && objRenderer.material.color != Color.green)
    //    {
    //        RenderKeys(false);

    //        var objRenderer2 = other.gameObject.GetComponent<Renderer>();
    //        objRenderer2.material.SetColor("_Color", Color.white);
    //    }
    //    if (other.gameObject.name.Equals("Bottom"))
    //    {
    //        dialogue.SetActive(false);
    //    }

    //}

    //void RenderKeys(bool val){
    //    var keyRenderer1 =  GameObject.Find("Key 1").GetComponent<Renderer>();
    //    var keyRenderer2 =  GameObject.Find("Key 2").GetComponent<Renderer>();

    //    keyRenderer1.enabled = val;
    //    keyRenderer1.GetComponent<BoxCollider2D>().enabled = val;

    //    keyRenderer2.enabled = val;
    //    keyRenderer2.GetComponent<BoxCollider2D>().enabled = val;

    //    getKeyDialogue.SetActive(val);
    //}

    IEnumerator WaitAndDisappear(float time)
    {
        yield return new WaitForSeconds(time);
        dialogue.SetActive(false);
    }

    void AddGravityToTiles()
    {
        Rigidbody2D tile = null;
        for (int i = 1; i < 5; i += 1)
        {

            tile = GameObject.Find("Tile" + i).GetComponent<Rigidbody2D>();
            tile.constraints = RigidbodyConstraints2D.None;
            tile.gravityScale = 1;
        }
    }

}
