using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;

public class PM5 : MonoBehaviour
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

    public DoorBehaviour doorBehaviour;
    public DoorBehaviour doorBehaviourLevel1Green;
    public DoorBehaviour dBL1GT;
    public DoorBehaviour dBL1YT;

    public GameObject getKeyDialogue;

    public AnalyticsManager analyticsManager;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask buttonLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //speed = 4f;
        //jump = 9f;
        RenderKeys(false);
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
            transform.position = Vector2.MoveTowards(transform.position, target, 30*Time.deltaTime);
        }

        if(target == new Vector2(rb.transform.position.x, rb.transform.position.y)){
            rb.gravityScale = 1;
            inMotion = false;
        }

        //GreenTunnelLevel1 Scene
        if (SceneManager.GetActiveScene().name == "Level1GreenTunnel")
        {
            dBL1GT._isLevel1GreenTunnel = true;
        }
        //YellowTunnelLevel1 Scene
        if (SceneManager.GetActiveScene().name == "Level1YellowTunnel")
        {
            dBL1YT._isLevel1YellowTunnel = true;
        }



    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D other){
            
            if(other.gameObject.name == "Tile 1"){
                GameObject obj = other.gameObject;
                GameObject parent = obj.transform.parent.gameObject;

                foreach (Transform child in parent.transform){
                    Debug.Log(child.gameObject.name);
                    var tile = child.gameObject.GetComponent<Rigidbody2D>();
                    tile.gravityScale = 0.3f;
                    tile.constraints = RigidbodyConstraints2D.None;
                    tile.constraints = RigidbodyConstraints2D.FreezePositionX;
                    // Destroy(child.gameObject);
                }
            }

    }

    void RenderKeys(bool val){
        var keyRenderer1 =  GameObject.Find("Key 1").GetComponent<Renderer>();
        var keyRenderer2 =  GameObject.Find("Key 2").GetComponent<Renderer>();

        keyRenderer1.enabled = val;
        keyRenderer1.GetComponent<BoxCollider2D>().enabled = val;

        keyRenderer2.enabled = val;
        keyRenderer2.GetComponent<BoxCollider2D>().enabled = val;

        getKeyDialogue.SetActive(val);
    }

}
