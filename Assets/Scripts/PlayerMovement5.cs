using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using System.Threading;
using System.Threading.Tasks;

public class PlayerMovement5 : MonoBehaviour
{
    private Vector2 target;
    private Rigidbody2D rb;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask groundLayer;

    public AnalyticsManager analyticsManager;

    public bool inMotion = false;
    public float speed;
    public float jump;
    public float move;
    public bool isJumping = false;
    public bool showText = false;
    public bool flag = false;
    public GameObject frozenKey;
    public GameObject lavaPlatform;
    public GameObject shootDialogue;
    public GameObject rotatingPlatform;

    public DoorBehaviour dBL5GT;
    public DoorBehaviour dBL5YT;

    public static bool isFacingRight;
    public static bool hasGun = false;
    Collider m_ObjectCollider;
    public GameObject closedGate;

    public AudioSource AudioSource;
    public AudioClip DoorOpenSound;
    public bool _isSoundPlayed = false;

    public AudioSource LavaDeathSoundSorce;
    public AudioClip LavaDeathSoundClip;

    public AudioSource DeathBySpikeSorce;
    public AudioClip DeathBySpikeClip;

    public bool isLavaSoundPlayed = false;

    public AudioSource GreenKeyCollectAudioSource;
    public AudioSource YellowKeyCollectAudioSource;
    public AudioClip KeyCollectSound;

    public AudioSource JumpAudioSource;
    public AudioClip JumpOpenSound;

    public AudioSource LavaButtonAudioSource;
    public AudioClip LavaButtonOpenSound;

    public AudioSource ZiplinOnOffAudioSource;
    public AudioClip ZiplineOnOffOpenSound;
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

        if (Input.GetKeyDown("space") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            isJumping = true;
        }

        if (showText)
        {
            hasGun = true;
        }

        if(rb.position.x > 0.9 && rb.position.x < 3.4)
        {
            float xPos = rb.position.x;
            Debug.Log("Rigidbody X position is: " + xPos);
            rb.gravityScale = -1;
        }
        else{
            rb.gravityScale = 1;
        }        

        if (inMotion)
        {

            rb.gravityScale = 0;
            transform.position = Vector2.MoveTowards(transform.position, target, 30 * Time.deltaTime);
        }

        if (target == new Vector2(rb.transform.position.x, rb.transform.position.y))
        {
            rb.gravityScale = 1;
            inMotion = false;
        }


        Flip();

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
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
        flag = false;

        //Tunnel Brown Door
        if (other.gameObject.name.Equals("BrownExitDoorClosed") || other.gameObject.name.Equals("DoorKnobClosed"))
        {
            Destroy(GameObject.Find("BrownExitDoorClosed"));
            Destroy(GameObject.Find("DoorKnobClosed"));

            if (!_isSoundPlayed)
            {
                AudioSource.clip = DoorOpenSound;
                AudioSource.Play();
                _isSoundPlayed = true;
            }

            GameObject.Find("BrownExitDoorOpen").GetComponent<Renderer>().enabled = true;
            GameObject.Find("DoorKnobOpen").GetComponent<Renderer>().enabled = true;
        }
        
        if (other.gameObject.name.Equals("Tile 2"))
        {
            // JumpAudioSource.clip = JumpOpenSound;
            // JumpAudioSource.Play();
            inMotion = true;
            target = other.gameObject.GetComponent<JumpingPadScript>().target;
            JumpAudioSource.clip = JumpOpenSound;
            JumpAudioSource.Play();
        }


        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
        if (other.gameObject.tag == ("LavaParticle"))
        {
            if (!isLavaSoundPlayed)
            {
                isLavaSoundPlayed = true;
                LavaDeathSoundSorce.clip = LavaDeathSoundClip;
                LavaDeathSoundSorce.Play();
            }
            StartCoroutine(WaitLavaDeathSceneReload());
        }

        if (other.gameObject.name == ("Bridge Tile 0") || other.gameObject.name == ("Bridge Tile 1") || other.gameObject.name == ("Bridge Tile 2") || other.gameObject.name == ("Ground1") || other.gameObject.name == ("PortalTile2"))
        {
            jump = 7f;
            speed = 4f;
        }

        if (other.gameObject.name == ("RotatingPlatform1") || other.gameObject.name == ("RotatingPlatform2") || other.gameObject.name == ("LavaTile") || other.gameObject.name == ("PortalPlatform"))
        {
            jump = 5f;
            speed = 6f;
        }

        if (other.gameObject.name == "SuperPowerKey")
        {
            Destroy(GameObject.Find("SuperPowerKey"));
            analyticsManager.SendEvent("LEVEL6 PLAYER COLLECTED SUPER POWER KEY TO ACCESS SUPER POWER");
            Destroy(GameObject.Find("SuperPowerGate"));
        }
        if (other.gameObject.name == "YellowTunnelEntry")
        {
            Destroy(GameObject.Find("YellowTunnelEntry"));
            analyticsManager.SendEvent("LEVEL6 USED YELLOW GATE TUNNEL");
            //analyticsManager.SendEvent("LEVEL6 GAMEEND");
            closedGate.SetActive(true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);

        }

        if (other.gameObject.name == "SuperKey")
        {
            Destroy(GameObject.Find("SuperKey"));
            analyticsManager.SendEvent("LEVEL6 PLAYER COLLECTED SUPER POWER TO ACCESS GUN TO SHOOT");
            showText = true;

            shootDialogue.SetActive(true);

            StartCoroutine(WaitAndMakeTextDisappear(5));
        }
        if (other.gameObject.name == "KeyReveal")
        {
            //Debug.Log("KR");
            Destroy(GameObject.Find("KeyReveal"));
            analyticsManager.SendEvent("LEVEL6 PLAYER COLLECTED SUPER KEY TO REVEAL YELLOW GATE KEY ");
            frozenKey.SetActive(true);
            GameObject.Find("Gate2OpenKey").GetComponent<SpriteRenderer>().enabled = true;
        }
        if (other.gameObject.name == "ExitGreenTunnelLevel7")
        {
            Destroy(GameObject.Find("ExitGreenTunnelLevel7"));
            //analyticsManager.SendEvent("LEVEL6 USED GREEN GATE TUNNEL");
            //analyticsManager.SendEvent("LEVEL6 GAMEEND");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (other.gameObject.name == "Tunnel6EndGate1")
        {
            Destroy(GameObject.Find("EndGate1"));
            analyticsManager.SendEvent("LEVEL6 USED GREEN GATE TUNNEL");
            analyticsManager.SendEvent("LEVEL6 GAMEEND");
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            // analyticsManager.SendEvent("LEVEL7 GAMESTART");
            SceneManager.LoadScene("Level-transition3");
            Thread.Sleep(100);
        }

        if (other.gameObject.name == "Tunnel6EndGate2")
        {
            Destroy(GameObject.Find("EndGate1"));
            analyticsManager.SendEvent("LEVEL6 USED YELLOW GATE TUNNEL");
            analyticsManager.SendEvent("LEVEL6 GAMEEND");
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            // analyticsManager.SendEvent("LEVEL7 GAMESTART");
            SceneManager.LoadScene("Level-transition3");
            Thread.Sleep(100);
        }

        if (other.gameObject.name == "Key2")
        {
            Destroy(GameObject.Find("Key2"));
        }

        if (other.gameObject.CompareTag("Trap"))
        {
            DeathBySpikeSorce.clip = DeathBySpikeClip;
            DeathBySpikeSorce.Play();
            SceneManager.LoadScene("Level5");
        }
        //Lava Key
        if (other.gameObject.name == "LavaButton")
        {

            LavaButtonAudioSource.clip = LavaButtonOpenSound;
            LavaButtonAudioSource.Play();
            Destroy(GameObject.Find("LavaPLatformLeftDisappearing"));
            Destroy(GameObject.Find("LavaPLatformRightDisappearing"));
            Destroy(GameObject.Find("LavaPLatformLeft (1)"));
            Destroy(GameObject.Find("LavaPLatformLeftDisappearing"));

            //Destroy(GameObject.Find("LavaPLatformLeft (1)"));
            lavaPlatform.SetActive(true);
            GameObject.Find("LavaPLatformLeft (2)").GetComponent<Renderer>().enabled = true;
        }

        //Open Green gate
        if (other.gameObject.name == "LavaKey")
        {
            GreenKeyCollectAudioSource.clip = KeyCollectSound;
            GreenKeyCollectAudioSource.Play();
            Destroy(GameObject.Find("LavaKey"));
            dBL5GT._isLevel5GreenDoorOpen = true;
            rotatingPlatform.SetActive(true);
            GameObject.Find("RotatingPlatform1").GetComponent<Renderer>().enabled = true;

        }
        //Open Yellow gate
        if (other.gameObject.name == "YellowGateKey")
        {
            YellowKeyCollectAudioSource.clip = KeyCollectSound;
            YellowKeyCollectAudioSource.Play();
            Destroy(GameObject.Find("YellowGateKey"));
            dBL5YT._isLevel5YellowDoorOpen = true;
        }
        if (other.gameObject.name == "EndGate2")
        {
            Destroy(GameObject.Find("EndGate2"));
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }

        

    }


    private void OnCollisionExit2D(Collision2D other)
    {

        if (other.gameObject.name == ("PortalPlatform"))
        {
            jump = 8f;
            speed = 5f;
        }

        if (other.gameObject.name == ("Bridge Tile 0") || other.gameObject.name == ("Bridge Tile 1") || other.gameObject.name == ("Bridge Tile 2") || other.gameObject.name == ("Ground1") || other.gameObject.name == ("PortalTile2"))
        {
            jump = 8f;
            speed = 5f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Zipline"))
        {

            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
            GameObject.Find("zipline-rope").GetComponent<SpriteRenderer>().material.color = Color.green;
            ZiplinOnOffAudioSource.clip = ZiplineOnOffOpenSound;
            ZiplinOnOffAudioSource.Play();
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Zipline"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            GameObject.Find("zipline-rope").GetComponent<SpriteRenderer>().material.color = new Color(0.823f, 0.706f, 0.549f);
            ZiplinOnOffAudioSource.clip = ZiplineOnOffOpenSound;
            ZiplinOnOffAudioSource.Play();
        }
    }

    IEnumerator WaitAndMakeTextDisappear(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        shootDialogue.SetActive(false);
        showText = false;
    }



    IEnumerator WaitLavaDeathSceneReload()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
