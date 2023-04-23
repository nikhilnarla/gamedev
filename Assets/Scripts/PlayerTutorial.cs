using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using TMPro;

public class PlayerTutorial : MonoBehaviour
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
    public GameObject EndGate;

    public GameObject bridge1;
    public GameObject bridge2;
    public GameObject bridge3;

    public GameObject dialogue;
    public GameObject panel;
    public GameObject endPanel;

    public GameObject gate;
    private Vector2 movePosition;
    public GameObject moveGate;
    public DoorBehaviour dbTL;

    public AudioSource DisapperingTileVisibleSound;
    public AudioClip DiapperingSound;

    public AudioSource KeyCollectSoundSource;
    public AudioClip KeyCollectSound;

    public TextMeshPro text;

    public AnalyticsManager analyticsManager;


    public Animator transition;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask buttonLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transition = GameObject.Find("Canvas").GetComponent<Animator>();
        movePosition = moveGate.transform.position;
 

        //dialogue.SetActive(true);
        StartCoroutine(WaitAndDisappearPanel(5f));
    }

    void Update()
    {
        move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * move, rb.velocity.y);
        
        if(Input.GetKeyDown("space") && IsGrounded()){
            rb.velocity =  new Vector2(rb.velocity.x, jump);
            isJumping = true;
        }

        if(Input.GetAxis("Horizontal") != 0){
            inMotion = false;
            rb.gravityScale = 1;
        }

        if(inMotion){
            rb.gravityScale = 0;
            transform.position = Vector2.MoveTowards(transform.position, target, 10*Time.deltaTime);
        }

        if(target == new Vector2(rb.transform.position.x, rb.transform.position.y)){
            rb.gravityScale = 1;
            inMotion = false;
        }

        if (Input.GetKey("i"))
        {
            dialogue.SetActive(true);
            StartCoroutine(WaitAndDisappearDialogue(5f));
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D other){

        if(other.gameObject.name.Equals("GreenTile"))
        {
            text.text = "Warning!!\nleft tiles disappear & speed slows!";
            text.color = Color.red;
            dialogue.SetActive(true);

            StartCoroutine(WaitAndDisappearDialogue(3f));

            DisapperingTileVisibleSound.clip = DiapperingSound;
            DisapperingTileVisibleSound.Play();

            bridge1.SetActive(true);
            bridge2.SetActive(true);
            bridge3.SetActive(true);
            key.SetActive(true);
        }

        if(other.gameObject.name.Equals("Key"))
        {
            Destroy(GameObject.Find("Key"));
            dbTL._isTutorialLevleYellowDoor = true;
            EndGate.SetActive(true);

            KeyCollectSoundSource.clip = KeyCollectSound;
            KeyCollectSoundSource.Play();
            //gate.transform.position = Vector2.MoveTowards( movePosition, transform.position, 0.00001f * Time.deltaTime);
            // StartCoroutine(WaitAndEnd(3f));

        }

        if(other.gameObject.name.Equals("EndGate")) {
            Destroy(GameObject.Find("EndGate"));
            SceneManager.LoadScene("TutorialTunnel");    
        }

        if (other.gameObject.name == ("Top"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jump * 3);
        }


        if (other.gameObject.name == "Green Block")
        {
            var objRenderer = GameObject.Find("Green Block").GetComponent<Renderer>();
            objRenderer.material.SetColor("_Color", Color.green);
            Debug.Log("green block");

            var Block = other.gameObject.GetComponent<Rigidbody2D>();
            Block.mass = 500.0f;
            Block.angularDrag = 0.05f;
            Block.gravityScale = 1.0f;

            text.text = "move block & press button";
        }

        if(other.gameObject.name.Equals("Bridge1") || other.gameObject.name.Equals("Bridge2") || other.gameObject.name.Equals("Bridge3") || other.gameObject.name.Equals("GreenTile"))
        {
            jump = 8f;
        }


    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.name.Equals("Bridge1") || other.gameObject.name.Equals("Bridge2") || other.gameObject.name.Equals("Bridge3"))
        {
            jump = 10f;
        }

        if(other.gameObject.name.Equals("GreenTile"))
        {
            jump = 10f;
            text.color = Color.black;
            text.text = "collect key";
        }

    }

    IEnumerator WaitAndDisappearDialogue(float time)
    {
        yield return new WaitForSeconds(time);
        dialogue.SetActive(false);
        
    }

    IEnumerator WaitAndDisappearPanel(float time)
    {
        yield return new WaitForSeconds(time);
        panel.SetActive(false);

    }


    IEnumerator WaitAndEnd(float time)
    {
        yield return new WaitForSeconds(2f);
        endPanel.SetActive(true);
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("TutorialTunnel");
    }


}
