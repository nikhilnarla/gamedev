using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rB;
    public float WalkSpeed = 2;
    public AnalyticsManager analyticsManager;
    private float Rightleft = 1;

    private bool disappeared = false;
    private float disappearDuration = 3f;

    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
  
    void Update()
    {
        if (!disappeared)
        {
            SetVelocity();
        }
    }

    private void SetVelocity() {
        //Debug.Log("move back and forth");
        if (transform.position.x < -2.5)
        { 
            rB.velocity = new Vector2(WalkSpeed * Rightleft, 0);
        }
 
        if (transform.position.x > -0.6)
        {
            rB.velocity = new Vector2(WalkSpeed * Rightleft * -1, 0);  
        }
    }


     public AudioSource EnemyDeathAudioSource;
    public AudioClip EnemyDeathSound;

     public AudioSource PlayerDeathAudioSource;
    public AudioClip PlayerDeathSound;



    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Bullet"))
        {
            EnemyDeathAudioSource.clip = EnemyDeathSound;
            EnemyDeathAudioSource.Play();
            Destroy(other.gameObject);
            Destroy(gameObject); // merged from week 5 branch
            analyticsManager.SendEvent("LEVEL6 ENEMY DESTROYED WITH BULLET");
            
            // disappeared = true;
            // gameObject.SetActive(false);
            // Invoke("Appear", disappearDuration); // disappear for 3 seconds
        } 
        else if (other.gameObject.tag.Equals("Player"))
        {
            // if player is hit by enemy, then it respawns to back at beginning of level
            PlayerDeathAudioSource.clip = PlayerDeathSound;
            PlayerDeathAudioSource.Play();
            analyticsManager.SendEvent("LEVEL6 PLAYER KILLED BY ENEMY AFTER COLLIDING");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            analyticsManager.SendEvent("LEVEL6 GAMESTART AGAIN");
            if (SceneManager.GetActiveScene().name == "Level6")
            {
                PlayerMovement6.hasGun = false; // lose gun when player dies in level 6
            }
        }
    } 

    // public void DisappearForSeconds(float duration)
    // {
    //     disappeared = true;
    //     gameObject.SetActive(false);
    //     Invoke("Appear", duration);
    // }

    private void Appear()
    {
        disappeared = false;
        gameObject.SetActive(true);
        // makes sure that enemy moves back and forth when it respawns
        transform.position = new Vector3(-1.3f, transform.position.y, transform.position.z);
        rB.velocity = new Vector2(WalkSpeed * Rightleft, 0);
    }

    private void OnDestroy()
    {
        if (disappeared)
        {
            CancelInvoke("Appear");
        }
    }
}