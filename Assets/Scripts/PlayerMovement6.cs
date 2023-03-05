using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerMovement6 : MonoBehaviour
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
    public static bool hasGun = false;
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
        if (showText)
        {

            GameObject.Find("Popup").GetComponent<Canvas>().enabled = true;
            hasGun = true;
        }

        if (!showText)
        {

            GameObject.Find("Popup").GetComponent<Canvas>().enabled = false;
        }

        Flip();
    }

    void Flip()
    {
        if (isFacingRight && move < 0f || !isFacingRight && move > 0f)
        { // conditions to want to flip player
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

        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
        if (other.gameObject.tag == ("LavaParticle"))
        {
            analyticsManager.SendEvent("LEVEL6 PLAYER FELL INTO LAVA");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if(other.gameObject.name == ("Bridge Tile 0") || other.gameObject.name == ("Bridge Tile 1") || other.gameObject.name == ("Bridge Tile 2") || other.gameObject.name == ("Ground1") || other.gameObject.name == ("PortalTile2")){
               jump = 5f;
               speed = 3f;
        }
        // if (other.gameObject.name == ("Ground")) // from wk 5
        // {
        //     isJumping = false;
        // }
        if (other.gameObject.name == "SuperPowerKey")
        {
            Destroy(GameObject.Find("SuperPowerKey"));
            analyticsManager.SendEvent("LEVEL6 PLAYER COLLECTED SUPER POWER KEY TO ACCESS SUPER POWER");
            Destroy(GameObject.Find("SuperPowerGate"));
        }

        if (other.gameObject.name == "SuperKey")
        {
            Destroy(GameObject.Find("SuperKey"));
            analyticsManager.SendEvent("LEVEL6 PLAYER COLLECTED SUPER POWER TO ACCESS GUN TO SHOOT");
            showText = true;
            StartCoroutine(WaitAndMakeTextDisappear(3));
        }
        if (other.gameObject.name == "KeyReveal")
        {
            //Debug.Log("KR");
            Destroy(GameObject.Find("KeyReveal"));
            analyticsManager.SendEvent("LEVEL6 PLAYER COLLECTED SUPER KEY TO REVEAL GATE 2 KEY ");
            frozenKey.SetActive(true);
            GameObject.Find("Gate2OpenKey").GetComponent<SpriteRenderer>().enabled = true;
        }
        if (other.gameObject.name == "EndGate1")
        {
            Destroy(GameObject.Find("EndGate1"));
            analyticsManager.SendEvent("LEVEL6 USED GATE2 TUNNEL");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (other.gameObject.name == "EndGate2")
        {
            Destroy(GameObject.Find("EndGate2"));
            analyticsManager.SendEvent("LEVEL6 USED GATE1 RIGHT USED");
            analyticsManager.SendEvent("LEVEL6 GAMEEND");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        // if(other.gameObject.name == "LeftEnd"){
        //   //m_ObjectCollider.isTrigger = true;
        //   Debug.Log("Trigger set");
        // }
        // if(other.gameObject.name == "RightEnd"){
        //   //m_ObjectCollider = GetComponent<Collider>();
        //   //m_ObjectCollider.isTrigger = false;
        //   Debug.Log("Trigger set off");
        // }
    }

    private void OnCollisionExit2D(Collision2D other){

         if(other.gameObject.name == ("Bridge Tile 0") || other.gameObject.name == ("Bridge Tile 1") || other.gameObject.name == ("Bridge Tile 2") || other.gameObject.name == ("Ground1") || other.gameObject.name == ("PortalTile2")){
               jump = 6.5f;
               speed = 2f;
        }
    }

    IEnumerator WaitAndMakeTextDisappear(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        showText = false;
    }

}