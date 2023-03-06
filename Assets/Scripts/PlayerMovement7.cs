using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerMovement7 : MonoBehaviour
{

    private Rigidbody2D rb;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask groundLayer;

    public AnalyticsManager analyticsManager;


    public float speed;
    public float jump;
    public float move;
    public bool isJumping = false;
    public bool showText = false;
    public bool lowerPath = false;
    public static bool eventanal = false;
    public GameObject frozenKey;
    public float freezeTime = 1.60f;
    public Transform TunnelSpawnPoint;
    public Transform TunnelSpawnPoint2;

    public float fallingSpeed = 1000f;

    public Transform newPositionBridge1;
    public Transform newPositionBridge2;
    public Transform newPositionBridge3;

    public GameObject bridge1;
    public GameObject bridge2;
    public GameObject bridge3;

    public static bool isFacingRight;
    Collider m_ObjectCollider;
    public GameObject closedGate;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_ObjectCollider = GetComponent<Collider>();

        //IntializeBridgeTiles();

        // Check the initial facing direction of the player based on the X component of the transform's scale
        if (transform.localScale.x > 0)
        {
            isFacingRight = true;
        }
        else
        {
            isFacingRight = false;
        }
    }

    void Update()
    {
        move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * move, rb.velocity.y);

        if (Input.GetKeyDown("space") && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            isJumping = true;
        }

        if (Input.GetKeyDown("space") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            isJumping = true;
        }

        Flip();
    }

    void Flip() {
        if (isFacingRight && move < 0f || !isFacingRight && move > 0f) { // conditions to want to flip player
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f; // x component of player's local scale
            transform.localScale = localScale;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.name == ("BridgeTile 1") ||
            other.gameObject.name == ("BridgeTile 2") ||
            other.gameObject.name == ("BridgeTile 3") ||
            other.gameObject.name == ("BridgeTile 4") ||
            other.gameObject.name == ("BridgeWall"))
        {
            speed = 3f;
            jump = 5f;
        }

        if (other.gameObject.name.Equals("Green Block"))
        {
            Debug.Log("green block");
            var objRenderer = GameObject.Find("Green Block").GetComponent<Renderer>();
            objRenderer.material.SetColor("_Color", Color.green);
            analyticsManager.SendEvent("LEVEL7 PLAYER HIT GREEN FALLING BLOCK");
            var block = other.gameObject.GetComponent<Rigidbody2D>();
            block.angularDrag = 0.05f;
            block.gravityScale = 1.0f;
            //analyticsManager.SendEvent("LEVEL7 PLAYER INTERACTED WITH THE GREEN BLOCK");

        }

        if (other.gameObject.name.Equals("ButtonDown"))
        {
            analyticsManager.SendEvent("LEVEL7 PLAYER HIT BUTTON FOR FALLING TILES");
            lowerPath = true;
        }

        if (other.gameObject.tag.Equals("Trap"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            analyticsManager.SendEvent("LEVEL7 PLAYER GOT KILLED BY SPIKES");
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }

        if (other.gameObject.CompareTag("TunnelLaser"))
        {
            rb.gameObject.transform.position = TunnelSpawnPoint.position;
            analyticsManager.SendEvent("LEVEL7 PLAYER KILLED BY YELLOW TUNNEL LASER");
        }
        if (other.gameObject.tag == "TunnelYellowTrap")
        {
             rb.gameObject.transform.position = TunnelSpawnPoint.position;
            analyticsManager.SendEvent("LEVEL7 PLAYER KILLED BY YELLOW TUNNEL SPIKES AT POSITION:" + GameObject.Find("Player").GetComponent<Rigidbody2D>().position);
            //player.gameObject.transform.position = TunnelSpawnPoint.position;
        }

        if (other.gameObject.tag == "TunnelGreenTrap")
        {
             rb.gameObject.transform.position = TunnelSpawnPoint2.position;
            analyticsManager.SendEvent("LEVEL7 PLAYER KILLED BY GREEN TUNNEL SPIKES AT POSITION:" + GameObject.Find("Player").GetComponent<Rigidbody2D>().position);
            //player.gameObject.transform.position = TunnelSpawnPoint.position;
        }

        if (other.gameObject.name == "EndGate1")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
              analyticsManager.SendEvent("LEVEL7 GREEN GATE USED");
            Destroy(GameObject.Find("EndGate1"));
            analyticsManager.SendEvent("LEVEL7 GAMEEND");
        }
        if (other.gameObject.name == "YellowTunnelEntry")
        {
            Destroy(GameObject.Find("YellowTunnelEntry"));
            analyticsManager.SendEvent("LEVEL7 PLAYER ENTERED GREEN TUNNEL");
            closedGate.SetActive(true);
        }

        if (other.gameObject.name == "EndGate2")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            analyticsManager.SendEvent("LEVEL7 YELLOW GATE USED");
            Destroy(GameObject.Find("EndGate2"));
            analyticsManager.SendEvent("LEVEL7 GAMEEND");
            
        }

        if (other.gameObject.name.Equals("Pad2"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jump * 3);
            analyticsManager.SendEvent("LEVEL7 PLAYER USED JUMPPAD");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // use OnTrigger so box colliders won't stop the player's movement
        // isTrigger checked for key 1 and key 2
        if (other.gameObject.name.Equals("Key1"))
        {
            Destroy(GameObject.Find("Gate1"));
            Destroy(other.gameObject); // destroy key 1

            bridge1.SetActive(true);
            bridge2.SetActive(true);
            bridge3.SetActive(true);

            analyticsManager.SendEvent("LEVEL7 PLAYER COLLECTED GREEN GATE KEY  AND GREEN GATE IS OPENED");

        }

        if (other.gameObject.name.Equals("Key2"))
        {
            Destroy(GameObject.Find("Gate2"));
            Destroy(other.gameObject); // destroy key 2
            analyticsManager.SendEvent("LEVEL7 PLAYER COLLECTED YELLOW GATE KEY AND YELLOW GATE IS OPENED");
        }
    }

    void AddGravityToTiles()
    {
        Rigidbody2D tile = null;
        for (int i = 1; i < 4; i += 1)
        {
            tile = GameObject.Find("BridgeTile " + i).GetComponent<Rigidbody2D>();
            tile.gravityScale = 1;
        }
    }

    void RemoveGravityToTiles()
    {
        Rigidbody2D tile = null;
        for (int i = 1; i < 4; i += 1)
        {
            tile = GameObject.Find("BridgeTile " + i).GetComponent<Rigidbody2D>();
            tile.gravityScale = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {

        if (other.gameObject.name == ("BridgeTile 1") ||
            other.gameObject.name == ("BridgeTile 2") ||
            other.gameObject.name == ("BridgeTile 3") ||
            other.gameObject.name == ("BridgeTile 4") ||
            (other.gameObject.name == ("BridgeWall")))
        {
            speed = 6f;
            jump = 10f;

        }
    }
}
