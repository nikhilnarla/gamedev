using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks; 

public class PlayerMovementC : MonoBehaviour
{
    public float speed;
    public float jump = 10f;
    public float move;
    public bool isJumping = false;
    public bool blockPushed = false;

    public AudioSource YellowKeyCollectAudioSource;
    public AudioSource TileButtonAudioSource;
    public AudioClip TileButtonSound;

    public AudioSource HitGreenBlockAudioSource;
    public AudioClip HitGreenBlockSound;

    public AudioSource SpikeSourceSound;
    public AudioClip SpikeSoundClip;

    public AudioSource BridgeTileAudioSource;
    public AudioClip BridgeTileSound;

    public AudioSource PlayerJumpAudioSource;
    public AudioClip PlayerJumpSound;

    public AudioSource GameEndAudioSource;
    public AudioClip GameEndSound;

    public AudioSource PlayerSpawnSourceSound;
    public AudioClip PlayerSpawnClip;

    public bool green = true;
    public bool fandetected = false;
    public GameObject key;
    public GameObject keyLevel1;
    public Transform TunnelSpawnPoint;
    public Transform Tunnel1SpawnPoint;
    public Transform Tunnel2SpawnPoint;
    public AnalyticsManager analyticsManager;
    public GameObject player;
    public static bool eventLevelFlag = false;
    public static bool flag = false;
    public GameObject dialogue;
    public GameObject tunnelDialogue;
    public bool isFacingRight = true;

    public GameObject closedDoor;
    public GameObject EndGame;


    Dictionary<string, bool> bridgeStatus = new Dictionary<string, bool>();

    public DoorBehaviour dBL2GG;
    public DoorBehaviour dBL2YG;
    public DoorBehaviour dBL2GT;
    public DoorBehaviour dBL2YT;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Rigidbody2D rbspikes;
    [SerializeField] private Rigidbody2D rbspikes1;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask buttonLayer;

    public AudioSource AudioSource;

    public AudioClip KeyCollectSound;
    public AudioClip DoorOpenSound;
    public bool _isSoundPlayed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        IntializeBridgeTiles();
    }

    void Update()
    {
        move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * move, rb.velocity.y);

        if (move > 0 && !isFacingRight) {
           Flip();
        }

        if (move < 0 && isFacingRight) {
           Flip();
        }

        if(Input.GetKeyDown("space") && !isJumping){
            rb.velocity =  new Vector2(rb.velocity.x, jump);
            isJumping = true;
        }

        if(Input.GetKeyDown("space") && IsGrounded()){
            rb.velocity =  new Vector2(rb.velocity.x, jump);
            isJumping = true;
        }

        if (SceneManager.GetActiveScene().name == "Level2GreenTunnel")
        {
            dBL2GT._isLevel1GreenTunnel = true;
            jump = 11f;
        }
        if (SceneManager.GetActiveScene().name == "Level2YellowTunnel")
        {
            dBL2YT._isLevel1YellowTunnel = true;
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

        // if (other.gameObject.name == ("Tile 0")) {
        //     PlayerJumpAudioSource.clip = PlayerJumpSound;
        //     PlayerJumpAudioSource.Play();
        // }
        // if (other.gameObject.name == ("Tile 1"))
        // {
        //     PlayerJumpAudioSource.clip = PlayerJumpSound;
        //     PlayerJumpAudioSource.Play();
        // }
        // if (other.gameObject.name == ("Tile 3"))
        // {
        //     PlayerJumpAudioSource.clip = PlayerJumpSound;
        //     PlayerJumpAudioSource.Play();
        // }
        // if (other.gameObject.name == ("Tile 2"))
        // {
        //     PlayerJumpAudioSource.clip = PlayerJumpSound;
        //     PlayerJumpAudioSource.Play();
        // }
        // if (other.gameObject.name == ("Tile 5"))
        // {
        //     PlayerJumpAudioSource.clip = PlayerJumpSound;
        //     PlayerJumpAudioSource.Play();
        // }
        // if (other.gameObject.name == ("Tile 4"))
        // {
        //     PlayerJumpAudioSource.clip = PlayerJumpSound;
        //     PlayerJumpAudioSource.Play();
        // }
        // if (other.gameObject.name == ("Block"))
        // {
        //     PlayerJumpAudioSource.clip = PlayerJumpSound;
        //     PlayerJumpAudioSource.Play();
        // }
        // if (other.gameObject.name == ("Block (1)"))
        // {
        //     PlayerJumpAudioSource.clip = PlayerJumpSound;
        //     PlayerJumpAudioSource.Play();
        // }

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
            EndGame.SetActive(true);
        }

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

        if (other.gameObject.name == ("Tile 4") && bridgeStatus.ContainsValue(false)){
            ShowTiles();
            // dialogue.SetActive(true);
            analyticsManager.SendEvent("LEVEL3 PLAYER HIT GREEN BLOCK");
            // Debug.Log("yassssss");
            HitGreenBlockAudioSource.clip = HitGreenBlockSound;
            HitGreenBlockAudioSource.Play();
        }

        if(other.gameObject.name == ("Bridge Tile 0") || other.gameObject.name == ("Bridge Tile 1") || other.gameObject.name == ("Bridge Tile 2")){
               jump = 5f;
               speed = 8f;


        }

        if(other.gameObject.name.Equals("BlindSpotEnter")) {
            Destroy(GameObject.Find("BlindSpotEnter"));
            tunnelDialogue.SetActive(true);
        }

        if(other.gameObject.name.Equals("BlindSpotExit")) {
            Destroy(GameObject.Find("BlindSpotExit"));
            tunnelDialogue.SetActive(false);
        }

        // if(other.gameObject.tag== ("Trap (1)") ||  other.gameObject.tag == ("Traps (2)")  || other.gameObject.tag == ("Traps") ){
        //         analyticsManager.SendEvent("LEVEL3 PLAYER KILLED BY RED TRAPS");
        // }

        if(other.gameObject.name == ("Button 1")){
            AddGravityToTiles();
            DestroyBridgeTiles();
            dBL2GG._isLevel2GreenDoorOpen = true;
            TileButtonAudioSource.clip = TileButtonSound;
            TileButtonAudioSource.Play();
            //HitButtonSource.Play();
            //Destroy(GameObject.Find("Gate"));
            analyticsManager.SendEvent("LEVEL3 PLAYER HIT GREEN GATE BUTTON AND OPENED GREEN GATE RIGHT");
        }

        if(other.gameObject.name == ("Button 2")){
            //Destroy(GameObject.Find("EntryGate"));
            dBL2YG._isLevel2YelllowDoorOpen = true;
            YellowKeyCollectAudioSource.clip = KeyCollectSound;
            YellowKeyCollectAudioSource.Play();
            Destroy(GameObject.Find("Button 2"));
            analyticsManager.SendEvent("LEVEL3 PLAYER HIT YELLOW GATE BUTTON AND OPENED YELLOW GATE LEFT");
        }

        if (other.gameObject.tag == "Trap" && !flag)
        {

            SpikeSourceSound.clip = SpikeSoundClip;
            SpikeSourceSound.Play();

            GameEndAudioSource.clip = GameEndSound;
            GameEndAudioSource.Play();
            StartCoroutine(WaitCoroutine());
            //player.gameObject.transform.position = TunnelSpawnPoint.position;
        }
        if (other.gameObject.tag == "TunnelGreenTrap" & !flag)
        {
                SpikeSourceSound.clip = SpikeSoundClip;
                SpikeSourceSound.Play();
                StartCoroutine(WaitCoroutineTunnel());
            //rb.gameObject.transform.position = TunnelSpawnPoint.position;
            // analyticsManager.SendEvent("LEVEL3 PLAYER KILLED BY GREEN TUNNEL SPIKES AT POSITION:"+rb.position);
            // flag = true;
            //player.gameObject.transform.position = TunnelSpawnPoint.position;
        }
        if (other.gameObject.tag == "TunnelYellowTrap" & !flag)
        {
            SpikeSourceSound.clip = SpikeSoundClip;
            SpikeSourceSound.Play();
            StartCoroutine(WaitCoroutineTunnelYellow());
            //player.gameObject.transform.position = TunnelSpawnPoint.position;
        }
          if (other.gameObject.tag == "ExitTraps" & !flag)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
             analyticsManager.SendEvent("LEVEL3 PLAYER KILLED BY SPIKES IN HARD TUNNEL");
            flag = true;
            //player.gameObject.transform.position = TunnelSpawnPoint.position;
        }

        if (other.gameObject.tag == "Sharp")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(other.gameObject.name == "Tile")
        {
            GameObject.Find("Tile").GetComponent<SpriteRenderer>().enabled = true;
        }
        if (other.gameObject.name == ("Jumpingtile"))
        {
            rb.velocity = new Vector2(rb.velocity.x,jump*3);
        }
        if(other.gameObject.name == ("JumpingtileLevel1"))
        {
            rb.velocity = new Vector2(rb.velocity.x,jump*3);
            analyticsManager.SendEvent("LEVEL1 JUMPTILE");
        }
        if(other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("MovingPlatform"))
        {
            isJumping = false;
        }
        if(other.gameObject.name == "GreenBlockLevel1")
        {
           var objRenderer = GameObject.Find("GreenBlockLevel1").GetComponent<Renderer>();
           objRenderer.material.SetColor("_Color", Color.green);

           var Block = other.gameObject.GetComponent<Rigidbody2D>();
           Block.mass = 5.0F;
           Block.angularDrag = 0.05F;
           Block.gravityScale = 1.0f;

           var objRenderer2 = GameObject.Find("CollidertileLevel1").GetComponent<Renderer>();
           objRenderer2.material.SetColor("_Color", Color.green);
        }
        //Analytics : Temp END GAME for Analytics
        //Enter yellow Tunnel
        if (other.gameObject.name == ("EndGameYellow"))
        {
            Debug.Log("Level 2 End");
            SceneManager.LoadScene("Level2YellowTunnel");
            //Analytics event - key Collected
            analyticsManager.SendEvent("LEVEL3 YELLOW GATE USED");
             //Desctroying end block so player can pass
            Destroy(GameObject.Find("EndGameYellow"));
            analyticsManager.SendEvent("LEVEL3 GAMEEND");
        }

        //Enter Green Tunnel
        if (other.gameObject.name == ("EndGameGreen"))
        {
            Debug.Log("Level 2 End");
            SceneManager.LoadScene("Level2GreenTunnel");
            //Analytics event - key Collected
            analyticsManager.SendEvent("LEVEL3 GREEN GATE USED");
             //Desctroying end block so player can pass
            Destroy(GameObject.Find("EndGameGreen"));
            analyticsManager.SendEvent("LEVEL3 GAMEEND");
        }

        //Exit Yellow Tunnel Level2
        if (other.gameObject.name == ("EndGateYellowLevel2"))
        {
            SceneManager.LoadScene("Level-transition2");
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            //Analytics event - key Collected
            analyticsManager.SendEvent("LEVEL3 YELLOW TUNNEL EXIT");
            //Desctroying end block so player can pass
            Destroy(GameObject.Find("EndGateYellowLevel2"));
            analyticsManager.SendEvent("LEVEL3 GAMEEND");
            
            Thread.Sleep(100);
            analyticsManager.SendEvent("LEVEL6 GAMESTART");

        }
        //Exit Yellow Tunnel
        if (other.gameObject.name == ("ExitYellowTLevel3"))
        {
            Debug.Log("Exit Yellow Tunnel Level 3");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+2);
            Destroy(GameObject.Find("ExitYellowTLevel3"));
        }

        //Enter green Tunnel
        if (other.gameObject.name == ("EndGameLevel3"))
        {
            Debug.Log("Level 2 End");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+2);

             //Desctroying end block so player can pass
            Destroy(GameObject.Find("EndGameLevel3"));
            analyticsManager.SendEvent("LEVEL3 GREEN GATE USED");
            analyticsManager.SendEvent("LEVEL3 GAMEEND");

            analyticsManager.SendEvent("LEVEL6 GAMESTART");

        }
        
        //Exit Green Tunnel
        if (other.gameObject.name == ("ENDGATEGREEN"))
        {
            Debug.Log("Exit Green Tunnel Level 3");
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+2);
            SceneManager.LoadScene("Level-transition2");
            Thread.Sleep(100);
            //Analytics event - key Collected
            // analyticsManager.SendEvent("LEVEL3 GAMEEND");
             //Desctroying end block so player can pas
        }

    }

    IEnumerator WaitCoroutine()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        Debug.Log("COROUTINE");
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //analyticsManager.SendEvent("LEVEL3 PLAYER KILLED BY SPIKES");
        //analyticsManager.SendEvent("LEVEL3 GAMESTART AGAIN");
        flag = true;
    }

    System.Collections.IEnumerator WaitCoroutineTunnel()
    {
        yield return new WaitForSeconds(0.1f);
        PlayerSpawnSourceSound.clip = PlayerSpawnClip;
        PlayerSpawnSourceSound.Play();

        rb.gameObject.transform.position = Tunnel1SpawnPoint.position;
        //analyticsManager.SendEvent("LEVEL1 PLAYER KILLED BY SPIKES IN GREEN GATE TUNNEL AT POSITION:" + rb.position);
        flag = true;
    }

    System.Collections.IEnumerator WaitCoroutineTunnelYellow()
    {
        yield return new WaitForSeconds(0.1f);
        PlayerSpawnSourceSound.clip = PlayerSpawnClip;
        PlayerSpawnSourceSound.Play();

        rb.gameObject.transform.position = Tunnel2SpawnPoint.position;
        analyticsManager.SendEvent("LEVEL1 PLAYER KILLED BY SPIKES IN YELLOW GATE TUNNEL AT POSITION:" + rb.position);
        flag = true;
    }


    private void OnCollisionExit2D(Collision2D other){

         if(other.gameObject.name == ("Bridge Tile 0") || other.gameObject.name == ("Bridge Tile 1") || other.gameObject.name == ("Bridge Tile 2")){
            jump = 10f;
            speed = 7f;
        }

         if(other.gameObject.name == ("Tile 4"))
        {
            dialogue.SetActive(false);

        }

    }

    private void IntializeBridgeTiles(){
        ShowTiles();
    }

    private void ShowTiles(){

        for (int i = 0; i<3; i+=1)
        {
            var bridgeTile = GameObject.Find("Bridge Tile "+i).GetComponent<Renderer>();
            bridgeTile.enabled = !bridgeTile.enabled;

            var collider = bridgeTile.GetComponent<BoxCollider2D>();
            collider.enabled = !collider.enabled;

            bridgeStatus["Bridge Tile "+i] = bridgeTile.enabled;
        }

    }

    void AddGravityToTiles()
    {
        Rigidbody2D tile = null;
        for(int i=0; i<6; i+=1)
        {
            tile = GameObject.Find("Tile "+i).GetComponent<Rigidbody2D>();
            tile.gravityScale = 1;
        }
    }

    void DestroyBridgeTiles()
    {
        for (int i = 0; i<3; i+=1)
        {
            var bridgeTile = GameObject.Find("Bridge Tile "+i);
            Destroy(bridgeTile);
        }
    }

    // void RemoveTraps(){
    //     var traps = GameObject.FindGameObjectsWithTag("Trap");

    //     foreach(GameObject trap in traps){
    //         Destroy(trap);
    //     }
    // }

}
