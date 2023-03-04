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

    private IEnumerator OnCollisionEnter2D(Collision2D other)
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
            var block = other.gameObject.GetComponent<Rigidbody2D>();
            block.angularDrag = 0.05f;
            block.gravityScale = 1.0f;
            //analyticsManager.SendEvent("LEVEL7 PLAYER INTERACTED WITH THE GREEN BLOCK");

        }

        if (other.gameObject.name.Equals("ButtonDown"))
        {

            AddGravityToTiles();

            yield return new WaitForSeconds(1.60f);
            Debug.Log("Remove Gravity");
            analyticsManager.SendEvent("LEVEL7 PLAYER HIT THE GRAVITY BUTTON FOR TILES TO FALL");

            RemoveGravityToTiles();

            Rigidbody2D tile = null;



            tile = GameObject.Find("BridgeTile 1").GetComponent<Rigidbody2D>();
            //tile.gravityScale = 0;
            tile.position = newPositionBridge1.position;

            tile = GameObject.Find("BridgeTile 2").GetComponent<Rigidbody2D>();
            //tile.gravityScale = 0;
            tile.position = newPositionBridge2.position;

            tile = GameObject.Find("BridgeTile 3").GetComponent<Rigidbody2D>();
            //tile.gravityScale = 0;
            tile.position = newPositionBridge3.position;

        }



        if (other.gameObject.tag.Equals("Trap"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            analyticsManager.SendEvent("LEVEL7 PLAYER GOT KILLED BY RED TRAPS");
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
             analyticsManager.SendEvent("LEVEL3 PLAYER KILLED BY GREEN TUNNEL TRAPS");
            //player.gameObject.transform.position = TunnelSpawnPoint.position;
        }
        if (other.gameObject.tag == "TunnelGreenTrap")
        {
             rb.gameObject.transform.position = TunnelSpawnPoint2.position;
             analyticsManager.SendEvent("LEVEL3 PLAYER KILLED BY YELLOW TUNNEL TRAPS");
            //player.gameObject.transform.position = TunnelSpawnPoint.position;
        }
        if (other.gameObject.name == "EndGate1")
        {
            Destroy(GameObject.Find("EndGate1"));
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (other.gameObject.name == "EndGate2")
        {
            Destroy(GameObject.Find("EndGate2"));
            //analyticsManager.SendEvent("LEVEL7 END");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (other.gameObject.name.Equals("Key1"))
        {
            Destroy(GameObject.Find("Gate1"));
            Destroy(GameObject.Find("Key1"));

            bridge1.SetActive(true);
            bridge2.SetActive(true);
            bridge3.SetActive(true);

            analyticsManager.SendEvent("LEVEL7 PLAYER COLLECTED KEY 1 AND GATE 1 IS OPENED");

        }

        if (other.gameObject.name.Equals("Key2"))
        {
            Destroy(GameObject.Find("Gate2"));
            Destroy(GameObject.Find("Key2"));
            analyticsManager.SendEvent("LEVEL7 PLAYER COLLECTED KEY 2 AND GATE 2 IS OPENED");
        }

        if (other.gameObject.name.Equals("Pad2"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jump * 3);
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
