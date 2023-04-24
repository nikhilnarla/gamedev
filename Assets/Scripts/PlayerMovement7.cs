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

    public DoorBehaviour dbL4GG;
    public DoorBehaviour dbL4YG;
    public DoorBehaviour dbL4GT;
    public DoorBehaviour dbL4YT;

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
    public Transform CannonSpawnPoint1;

    public GameObject tunnelDialogue;

    public float fallingSpeed = 1000f;

    public Transform newPositionBridge1;
    public Transform newPositionBridge2;
    public Transform newPositionBridge3;

    public GameObject bridge1;
    public GameObject bridge2;
    public GameObject bridge3;

    public bool _isSoundPlayed = false;
    public AudioSource AudioSource;
    public AudioClip DoorOpenSound;

    public static bool isFacingRight;
    Collider m_ObjectCollider;
    public GameObject closedGate;

    public AudioSource PlayerSpawnSourceSound;
    public AudioClip PlayerSpawnClip;

    public AudioSource SpikeSourceSound;
    public AudioClip SpikeSoundClip;

    public GameObject closedDoor;
    public GameObject ExitGreenTunnel;
    public GameObject EndGame;
  public AudioSource GreenKeyCollectAudioSource;
    public AudioSource YellowKeyCollectAudioSource;
    public AudioClip KeyCollectSound;


    public AudioSource SpringAudioSource;
    public AudioClip SpringDoorOpenSound;

      public AudioSource ButtonAudioSource;
    public AudioClip ButtonDoorOpenSound; 
    public AudioSource BridgeAudioSource;
    public AudioClip BridgeDoorOpenSound; 

    public AudioSource HitGreenBlockAudioSource;
    public AudioClip HitGreenBlockSound;
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

        Flip();

        if (SceneManager.GetActiveScene().name == "Level4GreenTunnel")
        {
            dbL4GT._isLevel1GreenTunnel = true;
        }
        if (SceneManager.GetActiveScene().name == "Level4YellowTunnel")
        {
            dbL4YT._isLevel1YellowTunnel = true;
            jump = 8f;
        }

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

        if(other.gameObject.name.Equals("BlindSpotEnter")) {
            Destroy(GameObject.Find("BlindSpotEnter"));
            tunnelDialogue.SetActive(true);
        }

        if(other.gameObject.name.Equals("BlindSpotExit")) {
            Destroy(GameObject.Find("BlindSpotExit"));
            tunnelDialogue.SetActive(false);
        }

        //Kill by Cannon Gun
        if (other.gameObject.CompareTag("CannonBullet"))
        {
            //Debug.Log("KILLED");
            rb.gameObject.transform.position = CannonSpawnPoint1.position;
        }

        if (other.gameObject.name == ("DoorOpen"))
        {
            //Debug.Log("DOORALERT");
            Destroy(GameObject.Find("DoorOpen"));

                AudioSource.clip = DoorOpenSound;
                AudioSource.Play();
                _isSoundPlayed = true;

            closedDoor.SetActive(true);
            GameObject.Find("DoorClose").GetComponent<Renderer>().enabled = true;
            ExitGreenTunnel.SetActive(true);
            ExitGreenTunnel.GetComponent<Renderer>().enabled = true;
        }


        if (other.gameObject.name == ("BridgeTile 1") ||
            other.gameObject.name == ("BridgeTile 2") ||
            other.gameObject.name == ("BridgeTile 3") ||
            other.gameObject.name == ("BridgeTile 4") ||
            other.gameObject.name == ("BridgeWall"))
        {
            speed = 6f;
            jump = 8f;
        } else {
            speed = 6f;
            jump = 10f;
        }

        if (other.gameObject.name.Equals("Green Block"))
        {
            Debug.Log("green block");
            var objRenderer = GameObject.Find("Green Block").GetComponent<Renderer>();
            if (!_isSoundPlayed){
            HitGreenBlockAudioSource.clip = HitGreenBlockSound;
            HitGreenBlockAudioSource.Play();
            _isSoundPlayed = true;
            }
            objRenderer.material.SetColor("_Color", Color.green);
            analyticsManager.SendEvent("LEVEL7 PLAYER HIT GREEN FALLING BLOCK");
            var block = other.gameObject.GetComponent<Rigidbody2D>();
            block.angularDrag = 0.05f;
            block.gravityScale = 1.0f;
            //analyticsManager.SendEvent("LEVEL7 PLAYER INTERACTED WITH THE GREEN BLOCK");

        }

        if (other.gameObject.name.Equals("ButtonDown"))
        {
            BridgeAudioSource.clip = BridgeDoorOpenSound;
            BridgeAudioSource.Play();
            analyticsManager.SendEvent("LEVEL7 PLAYER HIT BUTTON FOR FALLING TILES");
            lowerPath = true;
        }

        if (other.gameObject.tag.Equals("Trap"))
        {
            SpikeSourceSound.clip = SpikeSoundClip;
            SpikeSourceSound.Play();
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
            SpikeSourceSound.clip = SpikeSoundClip;
            SpikeSourceSound.Play();
            StartCoroutine(WaitCoroutineTunnelYellow());
            //analyticsManager.SendEvent("LEVEL7 PLAYER KILLED BY YELLOW TUNNEL SPIKES AT POSITION:" + GameObject.Find("Player").GetComponent<Rigidbody2D>().position);
            //player.gameObject.transform.position = TunnelSpawnPoint.position;
        }

        if (other.gameObject.tag == "TunnelGreenTrap")
        {

            SpikeSourceSound.clip = SpikeSoundClip;
            SpikeSourceSound.Play();
            StartCoroutine(WaitCoroutineTunnel());
            analyticsManager.SendEvent("LEVEL7 PLAYER KILLED BY GREEN TUNNEL SPIKES AT POSITION:" + GameObject.Find("Player").GetComponent<Rigidbody2D>().position);
            //player.gameObject.transform.position = TunnelSpawnPoint.position;
        }

       
        if (other.gameObject.name == "YellowTunnelEntry")
        {
            SceneManager.LoadScene("Level4YellowTunnel");
            Destroy(GameObject.Find("YellowTunnelEntry"));
            analyticsManager.SendEvent("LEVEL7 PLAYER ENTERED Yellow TUNNEL");
            closedGate.SetActive(true);
        }
        //Enter into Green Tunnel
        if (other.gameObject.name == "GreenTunnelEntry")
        {
            SceneManager.LoadScene("Level4GreenTunnel");
            Destroy(GameObject.Find("GreenTunnelEntry"));
            analyticsManager.SendEvent("LEVEL7 PLAYER ENTERED GREEN TUNNEL");
            closedGate.SetActive(true);
        }
        if (other.gameObject.name == "EndGate1")
        {
            Destroy(GameObject.Find("EndGate1"));
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            
        }

        //Exit Yellow Tunnel
        if (other.gameObject.name == "ExitYellowTunnelLevel7")
        {
            SceneManager.LoadScene("Level-transition4");
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            Destroy(GameObject.Find("ExitYellowTunnelLevel7"));
        }
        //Exit Green Tunnel
        if (other.gameObject.name == "ExitGreenTunnelLevel7")
        {
            //SceneManager.LoadScene("Level-transition4");
            Destroy(GameObject.Find("ExitGreenTunnelLevel7"));
            //SceneManager.LoadScene("Level-transition3");
            //Thread.Sleep(100);
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene("Level-transition4");
            
        }

        if (other.gameObject.name.Equals("Pad2"))
        {
            SpringAudioSource.clip = SpringDoorOpenSound;
            SpringAudioSource.Play();
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
            GreenKeyCollectAudioSource.clip = KeyCollectSound;
            GreenKeyCollectAudioSource.Play();
            Destroy(other.gameObject); // destroy key 1
            //Destroy(GameObject.Find("Gate1"));
            dbL4GG._isLevel4GreenDoorOpen = true;
            BridgeAudioSource.clip = BridgeDoorOpenSound;
            BridgeAudioSource.Play();
            bridge1.SetActive(true);
            bridge2.SetActive(true);
            bridge3.SetActive(true);

            analyticsManager.SendEvent("LEVEL7 PLAYER COLLECTED GREEN GATE KEY  AND GREEN GATE IS OPENED");
            

        }

        if (other.gameObject.name.Equals("Key2"))
        {
            YellowKeyCollectAudioSource.clip = KeyCollectSound;
            YellowKeyCollectAudioSource.Play();
            //Destroy(GameObject.Find("Gate2"));
            Destroy(other.gameObject); // destroy key 2
            analyticsManager.SendEvent("LEVEL7 PLAYER COLLECTED YELLOW GATE KEY AND YELLOW GATE IS OPENED");
            dbL4YG._isLevel4YellowDoorOpen = true;
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


    System.Collections.IEnumerator WaitCoroutineTunnel()
    {
        yield return new WaitForSeconds(0.1f);
        PlayerSpawnSourceSound.clip = PlayerSpawnClip;
        PlayerSpawnSourceSound.Play();

        rb.gameObject.transform.position = TunnelSpawnPoint2.position;
        //analyticsManager.SendEvent("LEVEL1 PLAYER KILLED BY SPIKES IN GREEN GATE TUNNEL AT POSITION:" + rb.position);
    }

    System.Collections.IEnumerator WaitCoroutineTunnelYellow()
    {
        yield return new WaitForSeconds(0.1f);
        PlayerSpawnSourceSound.clip = PlayerSpawnClip;
        PlayerSpawnSourceSound.Play();

        rb.gameObject.transform.position = TunnelSpawnPoint.position;
        //analyticsManager.SendEvent("LEVEL1 PLAYER KILLED BY SPIKES IN YELLOW GATE TUNNEL AT POSITION:" + rb.position);
    }
}
