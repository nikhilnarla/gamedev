using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using System.Threading;
using System.Threading.Tasks; 

public class PlayerMovement6 : MonoBehaviour
{

    private Rigidbody2D rb;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask groundLayer;

    public AnalyticsManager analyticsManager;

    public AudioSource GreenKeyCollectAudioSource;

    public AudioClip KeyCollectSound;

    public AudioSource BridgeTileAudioSource;
    public AudioClip BridgeTileSound;

    public float speed;
    public float jump;
    public float move;
    public bool isJumping = false;
    public bool showText = false;
    public bool flag = false;
    public GameObject frozenKey;
    public GameObject shootDialogue;

    public AudioSource GunCollectorSourceSound;
    public AudioClip GunCollectorSoundClip;

    public AudioSource SpCollectorSourceSound;
    public AudioClip SpCollectorSoundClip;
    public GameObject tunnelDialogue;
    public Transform CannonSpawnPoint;
    public GameObject player;
    public GameObject closedDoor;
    public GameObject TunnelEndGate;

    public DoorBehaviour dBL3GT;

    public static bool isFacingRight;
    public static bool hasGun = false;
    Collider m_ObjectCollider;
    public GameObject closedGate;

    public AudioSource AudioSource;
    public AudioClip DoorOpenSound;
    public bool _isSoundPlayed = false;
    public bool isLavaSoundPlayed = false;

    public AudioSource LavaDeathSoundSorce;
    public AudioClip LavaDeathSoundClip;

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


        Flip();

        if (SceneManager.GetActiveScene().name == "Level3GreenTunnel")
        {
            dBL3GT._isLevel1GreenTunnel = true;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)){
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
        //Kill by Cannon Gun
        if (other.gameObject.CompareTag("CannonBullet"))
        {
            //Debug.Log("KILLED");
            rb.gameObject.transform.position = CannonSpawnPoint.position;
        }
        flag = false;


        if (other.gameObject.name == ("DoorOpen")) {
            //Debug.Log("DOORALERT");
            Destroy(GameObject.Find("DoorOpen"));

            if (!_isSoundPlayed)
            {
                AudioSource.clip = DoorOpenSound;
                AudioSource.Play();
                _isSoundPlayed = true;
            }

            closedDoor.SetActive(true);
            closedDoor.GetComponent<Renderer>().enabled = true;
            TunnelEndGate.SetActive(true);
            TunnelEndGate.GetComponent<Renderer>().enabled = true;
        }

        //Tunnel Brown Door
        if (other.gameObject.Equals("BrownDoorClose")){
            Destroy(GameObject.Find("BrownDoorClose"));
            if (!_isSoundPlayed)
            {
                AudioSource.clip = DoorOpenSound;
                AudioSource.Play();
                _isSoundPlayed = true;
            }
            GameObject.Find("BrownDoorOpen").SetActive(true);
            GameObject.Find("BrownDoorOpen").GetComponent<Renderer>().enabled = true;
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
        if (other.gameObject.tag == ("LavaParticle") && !flag)
        {
            analyticsManager.SendEvent("LEVEL6 PLAYER FELL INTO LAVA");
            if (!isLavaSoundPlayed)
            {
                isLavaSoundPlayed = true;
                LavaDeathSoundSorce.clip = LavaDeathSoundClip;
                LavaDeathSoundSorce.Play();
            }
            StartCoroutine(WaitLavaDeathSceneReload());
        }

        if(other.gameObject.name == ("Bridge Tile 0") || other.gameObject.name == ("Bridge Tile 1") || other.gameObject.name == ("Bridge Tile 2") 
            || other.gameObject.name == ("Ground1") || other.gameObject.name == ("PortalTile2") 
            || other.gameObject.name == ("Wall5")){
               jump = 6f;
               speed = 3.5f;
        } else if(other.gameObject.name == ("FallingBlock1") || other.gameObject.name == ("FallingBlock2") || other.gameObject.name == ("FallingBlock3") 
            || other.gameObject.name == ("FallingBlock4")){
               jump = 6f;
               speed = 3.5f;
        } else {
            jump = 8f;
            speed = 5f;
        }


        if (other.gameObject.name == "SuperPowerKey")
        {
            Destroy(GameObject.Find("SuperPowerKey"));
            SpCollectorSourceSound.clip = SpCollectorSoundClip;
            SpCollectorSourceSound.Play();
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

        if(other.gameObject.name.Equals("BlindSpotEnter")) {
            Destroy(GameObject.Find("BlindSpotEnter"));
            tunnelDialogue.SetActive(true);
        }

        if(other.gameObject.name.Equals("BlindSpotExit")) {
            Destroy(GameObject.Find("BlindSpotExit"));
            tunnelDialogue.SetActive(false);
        }

        if (other.gameObject.name == "SuperKey")
        {
            Destroy(GameObject.Find("SuperKey"));
               GunCollectorSourceSound.clip = GunCollectorSoundClip;
                GunCollectorSourceSound.Play();
            analyticsManager.SendEvent("LEVEL6 PLAYER COLLECTED SUPER POWER TO ACCESS GUN TO SHOOT");
            showText = true;

            shootDialogue.SetActive(true);

            StartCoroutine(WaitAndMakeTextDisappear(5));
        }
        if (other.gameObject.name == "KeyReveal")
        {
            //Debug.Log("KR");
            Destroy(GameObject.Find("KeyReveal"));
            GreenKeyCollectAudioSource.clip = KeyCollectSound;
            GreenKeyCollectAudioSource.Play();
            analyticsManager.SendEvent("LEVEL6 PLAYER COLLECTED SUPER KEY TO REVEAL YELLOW GATE KEY ");
            frozenKey.SetActive(true);
            GameObject.Find("Gate2OpenKey").GetComponent<SpriteRenderer>().enabled = true;
        }
        if (other.gameObject.name == "EndGate1")
        {
            Destroy(GameObject.Find("EndGate1"));
            analyticsManager.SendEvent("LEVEL6 USED GREEN GATE TUNNEL");
            analyticsManager.SendEvent("LEVEL6 GAMEEND");
            SceneManager.LoadScene("Level3YellowTunnel");
        }
        if (other.gameObject.name == "EndGate2")
        {
            Destroy(GameObject.Find("EndGate2"));
            analyticsManager.SendEvent("LEVEL6 USED YELLOW GATE TUNNEL");
            analyticsManager.SendEvent("LEVEL6 GAMEEND");
            SceneManager.LoadScene("Level3GreenTunnel");
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

        if(other.gameObject.name == "Key2")
        {
            Destroy(GameObject.Find("Key2"));
        }

        if(other.gameObject.CompareTag("Trap"))
        {
            SceneManager.LoadScene("Level5");
        }

    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag.Equals("Zipline"))
        {
            
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Zipline"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
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
        flag = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerMovement6.hasGun = false; // lose gun when player dies
        analyticsManager.SendEvent("LEVEL6 GAMESTART AGAIN");
    }

}
