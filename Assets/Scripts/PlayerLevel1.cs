using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class PlayerLevel1 : MonoBehaviour
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
    public bool isFacingRight = true;
    public GameObject closedDoor;
    public GameObject ExitGreenTunnel;

    public DoorBehaviour doorBehaviour;
    public DoorBehaviour doorBehaviourLevel1Green;
    public DoorBehaviour dBL1GT;
    public DoorBehaviour dBL1YT;
    public DoorBehaviour dbTutorialLevel;

    public GameObject getKeyDialogue;

    public AnalyticsManager analyticsManager;


    Dictionary<string, bool> bridgeStatus = new Dictionary<string, bool>();

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask buttonLayer;

    public AudioSource AudioSource;
    public AudioClip DoorOpenSound;

    public AudioSource GreenKeyCollectAudioSource;
    public AudioSource YellowKeyCollectAudioSource;
    public AudioClip KeyCollectSound;
    public AudioSource HitGreenBlockAudioSource;
    public AudioClip HitGreenBlockSound;

    public AudioSource SpikeSourceSound;
    public AudioClip SpikeSoundClip;
    public AudioSource GameEndAudioSource;
    public AudioClip GameEndSound;
    public AudioSource PlayerSpawnSourceSound;
    public AudioClip PlayerSpawnClip;

    public bool _isSoundPlayed = false;

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

        if(move != 0){
            inMotion = false;
            rb.gravityScale = 1;
        }

        if (move > 0 && !isFacingRight) {
        
           Flip();
        //    getKeyDialogue.SetActive(true);
        }

        if (move < 0 && isFacingRight) {
           Flip();
        //    getKeyDialogue.SetActive(false);
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
        if (SceneManager.GetActiveScene().name == "TutorialTunnel"){
            dbTutorialLevel._isTutorialLevel = true;
        }
    }

    void Flip() {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1; // changes sprite direction
        gameObject.transform.localScale = currentScale;

        isFacingRight = !isFacingRight;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D other){
        flag = false;

        if (other.gameObject.name == ("DoorOpen"))
        {
            //Debug.Log("DOORALERT");
            Destroy(GameObject.Find("DoorOpen"));

            if (!_isSoundPlayed)
            {
                AudioSource.clip = DoorOpenSound;
                AudioSource.Play();
                _isSoundPlayed = true;
            }

            closedDoor.SetActive(true);
            GameObject.Find("DoorClose").GetComponent<Renderer>().enabled = true;
            ExitGreenTunnel.SetActive(true);
        }

        //BrownDoor Green Tunnel Level1
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

        if(other.gameObject.name.Equals("TunnelEnd")) {
            SceneManager.LoadScene("Menu");
        }

        if (other.gameObject.name.Equals("Bottom"))
        {
            dialogue.SetActive(true);
        }
        if (other.gameObject.tag == "TunnelGreenTrap" && !flag)
        {
            SpikeSourceSound.clip = SpikeSoundClip;
            SpikeSourceSound.Play();
            StartCoroutine(WaitCoroutineTunnel());
        }
        //  if (other.gameObject.tag == "TunnelLaser")
        // {
        //     rb.gameObject.transform.position = Tunnel1SpawnPoint.position;
        //     // analyticsManager.SendEvent("LEVEL1 PLAYER KILLED BY SPIKES IN GREEN GATE TUNNEL AT POSITION:" + rb.position);
        //     // flag = true;

      // }
        if (other.gameObject.tag == "TunnelYellowTrap" && !flag)
        {
            SpikeSourceSound.clip = SpikeSoundClip;
            SpikeSourceSound.Play();
            StartCoroutine(WaitCoroutineTunnelYellow());

        }
        if (other.gameObject.tag == "Trap" && !flag)
        {

            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            SpikeSourceSound.clip = SpikeSoundClip;
            SpikeSourceSound.Play();

            GameEndAudioSource.clip = GameEndSound;
            GameEndAudioSource.Play();
            StartCoroutine(WaitCoroutine());
        }

        if (other.gameObject.name == ("Button 2")){
            Destroy(GameObject.Find("EntryGate"));
        }

        if (other.gameObject.name == ("Tile 2") || other.gameObject.name == ("Top"))
        {
            analyticsManager.SendEvent("LEVEL1 JUMP PAD INTERACTED");
            inMotion = true;
            target = other.gameObject.GetComponent<JumpingPadScript>().target;
            AudioSource.clip = DoorOpenSound;
            AudioSource.Play();
        }

        if(other.gameObject.name == "Green Block" && other.gameObject.GetComponent<Renderer>().material.color != Color.green)
        {
            var objRenderer = GameObject.Find("Green Block").GetComponent<Renderer>();
            objRenderer.material.SetColor("_Color", Color.green);
            Debug.Log("green block");
            analyticsManager.SendEvent("LEVEL1 PLAYER INTERACTED WITH THE GREEN BLOCK");
            var Block = other.gameObject.GetComponent<Rigidbody2D>();
            Block.mass = 5.0f;
            Block.angularDrag = 0.05f;
            Block.gravityScale = 1.0f;

            var objRenderer2 = GameObject.Find("Collider Tile").GetComponent<Renderer>();
            objRenderer2.material.SetColor("_Color", Color.green);
            RenderKeys(true);

            HitGreenBlockAudioSource.clip = HitGreenBlockSound;
            HitGreenBlockAudioSource.Play();

            getKeyDialogue.SetActive(false);

        }

        if (other.gameObject.name == "Collider Tile" && other.gameObject.GetComponent<Renderer>().material.color != Color.green)
        {
            RenderKeys(true);
            var objRenderer2 = other.gameObject.GetComponent<Renderer>();
            objRenderer2.material.SetColor("_Color", Color.green);

            
        }
        if (other.gameObject.name == "Key 1")
        {
            Destroy(other.gameObject);
            doorBehaviour._isDoorOpen = true;
            YellowKeyCollectAudioSource.clip = KeyCollectSound;
            YellowKeyCollectAudioSource.Play();
            //var gate = GameObject.Find("EntryGate");
            //analyticsManager.SendEvent("LEVEL1 YELLOW GATE UNLOCKED");
            //Destroy(gate);
            //Destroy(other.gameObject);
        }
        //Close Yellow Gate.
        if (other.gameObject.name == "`")
        {
            Destroy(other.gameObject);
            doorBehaviour._isLevel2GreenDoorClose = true;
        }
        if (other.gameObject.name == "Key 2")
        {
            Destroy(other.gameObject);
            doorBehaviourLevel1Green._isLevel1GreenDoorOpen = true;
            GreenKeyCollectAudioSource.clip = KeyCollectSound;
            GreenKeyCollectAudioSource.Play();
            //var gate = GameObject.Find("Gate");
            //analyticsManager.SendEvent("LEVEL1 GREEN GATE UNLOCKED");
            //Destroy(gate);
            //Destroy(other.gameObject);
        }
        //Close Green Gate.
        if (other.gameObject.name == "CloseGreenGate")
        {
            Destroy(other.gameObject);
            doorBehaviourLevel1Green._isLevel1GreenDoorClose = true;
        }

        //Enter Level1 yellow Tunnel
        if (other.gameObject.name == ("EndGate1"))
        {
            Debug.Log("Level 1 End");
            SceneManager.LoadScene("Level1YellowTunnel");
            Destroy(GameObject.Find("EndGate1"));

            analyticsManager.SendEvent("LEVEL1 YELLOW GATE USED");
            analyticsManager.SendEvent("LEVEL1 GAMEEND");
            analyticsManager.SendEvent("LEVEL3 GAMESTART");
        }
        //Exit Yellow Tunnel LEVEL1
        if (other.gameObject.name == ("ExitYellowTunnelLevel1"))
        {
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+2);
            Destroy(GameObject.Find("ExitYellowTunnelLevel1"));
            SceneManager.LoadScene("Level-transition1");
            Thread.Sleep(100);
        }
        //Exit Green Tunnel LEVEL1
        if (other.gameObject.name == ("ExitGreenTunnel"))
        {
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            Destroy(GameObject.Find("ExitGreenTunnel"));
            SceneManager.LoadScene("Level-transition1");
            Thread.Sleep(100);
        }

        if (other.gameObject.name == ("EndGate2"))
        {
            Debug.Log("Level 1 End");
            SceneManager.LoadScene("Level1GreenTunnel");
            Destroy(GameObject.Find("EndGate2"));

            

            analyticsManager.SendEvent("LEVEL1 GREEN GATE USED");
            analyticsManager.SendEvent("LEVEL1 GAMEEND");
            analyticsManager.SendEvent("LEVEL3 GAMESTART");
        }

    }

    System.Collections.IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        flag = true;
    }

    System.Collections.IEnumerator WaitCoroutineTunnel()
    {
        yield return new WaitForSeconds(0.1f);
        PlayerSpawnSourceSound.clip = PlayerSpawnClip;
        PlayerSpawnSourceSound.Play();
        rb.gameObject.transform.position = Tunnel2SpawnPoint.position;
        analyticsManager.SendEvent("LEVEL1 PLAYER KILLED BY SPIKES IN GREEN GATE TUNNEL AT POSITION:" + rb.position);
        flag = true;
    }

    System.Collections.IEnumerator WaitCoroutineTunnelYellow()
    {
        yield return new WaitForSeconds(0.1f);
        rb.gameObject.transform.position = Tunnel1SpawnPoint.position;
        analyticsManager.SendEvent("LEVEL1 PLAYER KILLED BY SPIKES IN YELLOW GATE TUNNEL AT POSITION:" + rb.position);
        flag = true;
    }

    private void OnCollisionExit2D(Collision2D other){

        var objRenderer = GameObject.Find("Green Block").GetComponent<Renderer>();

        if(other.gameObject.name == "Collider Tile" && objRenderer.material.color != Color.green)
        {
            RenderKeys(false);

            var objRenderer2 = other.gameObject.GetComponent<Renderer>();
            objRenderer2.material.SetColor("_Color", Color.white);
        }
        if (other.gameObject.name.Equals("Bottom"))
        {
            dialogue.SetActive(false);
        }

    }

    void RenderKeys(bool val){
        var keyRenderer1 =  GameObject.Find("Key 1").GetComponent<Renderer>();
        var keyRenderer2 =  GameObject.Find("Key 2").GetComponent<Renderer>();

        keyRenderer1.enabled = val;
        keyRenderer1.GetComponent<BoxCollider2D>().enabled = val;

        keyRenderer2.enabled = val;
        keyRenderer2.GetComponent<BoxCollider2D>().enabled = val;

  
        // getKeyDialogue.SetActive(val);
    }



}
