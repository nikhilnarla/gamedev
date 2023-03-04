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
        Debug.Log("move back and forth");
        if (transform.position.x < -2.9)
        { 
            rB.velocity = new Vector2(WalkSpeed * Rightleft, 0);
        }
 
        if (transform.position.x > -1.8)
        {
            rB.velocity = new Vector2(WalkSpeed * Rightleft * -1, 0);  
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Bullet"))
        {
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
            analyticsManager.SendEvent("LEVEL6 PLAYER KILLED BY ENEMY AFTER COLLIDING");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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