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
    public GameObject frozenKey;


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
        //if (showText)
        //{

        //    GameObject.Find("Popup").GetComponent<Canvas>().enabled = true;
        //}

        //if (!showText)
        //{

        //    GameObject.Find("Popup").GetComponent<Canvas>().enabled = false;
        //}

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

        if (other.gameObject.name.Equals("Button"))
        {
            Debug.Log("Pressed Button");
            AddGravityToTiles();
            yield return new WaitForSeconds(1.60f);
            Debug.Log("Remove Gravity");
            RemoveGravityToTiles();

        }

        if (other.gameObject.tag.Equals("Trap"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }

        if (other.gameObject.name == "EndGate1")
        {
            Destroy(GameObject.Find("EndGate1"));
            //analyticsManager.SendEvent("LEVEL7 END ");
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
        }

        if (other.gameObject.name.Equals("Key2"))
        {
            Destroy(GameObject.Find("Gate2"));
            Destroy(GameObject.Find("Key2"));
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
            tile.constraints = RigidbodyConstraints2D.FreezePosition;
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
