using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement6 : MonoBehaviour
{

    private Rigidbody2D rb;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private Transform portal1Spawning, portal2Spawning;
     public AnalyticsManager analyticsManager;


    public float speed;
    public float jump;
    public float move;
    public bool isJumping = false;
    public bool showText = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //IntializeBridgeTiles();
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
        if (Input.GetKeyDown("left") && IsGrounded())
        {
            transform.Rotate(0, 180, 0);
        }
        // if (Input.GetKeyDown("right") && IsGrounded())
        // {
        //     transform.Rotate(0, 0, 0);
        // }
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
        if (other.gameObject.name == ("Portal1"))
        {
            analyticsManager.SendEvent("LEVEL6 PORTAL1 USED");
            rb.transform.position = portal2Spawning.transform.position;
        }
        if (other.gameObject.name == ("Portal2"))
        {
            analyticsManager.SendEvent("LEVEL6 PORTAL2 USED");
            rb.transform.position = portal1Spawning.transform.position;
        }
         if (other.gameObject.tag == ("LavaParticle"))
        {
            analyticsManager.SendEvent("LEVEL6 PLAYER FELL INTO LAVA");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        // if (other.gameObject.name == ("Ground")) // from wk 5
        // {
        //     isJumping = false;
        // }
        if(other.gameObject.name == "SuperPowerKey")
        {
            Destroy(GameObject.Find("SuperPowerKey"));
            Destroy(GameObject.Find("SuperPowerGate"));
        }

        if(other.gameObject.name == "SuperKey")
        {
            Destroy(GameObject.Find("SuperKey"));
            showText = true;
        }
    }

    void OnGUI()
    {
        if(showText) {
            // GUI.Label(new Rect(425, 20, 100, 100), "Powerup Collected, Press C to shoot bullets!","black");
            var centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.UpperCenter;
            GUI.Label(new Rect(Screen.width/2-50, Screen.height/2-25, 100, 50), "Powerup Collected, Press C to shoot bullets!", centeredStyle);
        }
    }
}
