using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemyMove1 : MonoBehaviour
{
     public float speed;
     public float currSpeed;
     public bool MoveRight = false;
     public Transform TunnelSpawnPoint;
     public AnalyticsManager analyticsManager;
     public float fastSpeed;
     private bool bulletHit = false;
     private float bulletHitStartTime = 0.0f;
     public SpriteRenderer TunnelEnemy_SpriteRenderer;
     public Sprite angrySprite;
     public Sprite normalFaceSprite;
     public GameObject player;
     bool stopMovement = false;

    public AudioSource PlayerCaughtSourceSound;
    public AudioClip PlayerCauhtClip;

    public AudioSource PlayerSpawnSourceSound;
    public AudioClip PlayerSpawnClip;

    private void Start()
    {
        currSpeed = speed;
        TunnelEnemy_SpriteRenderer = GameObject.Find("TunnelEnemy").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopMovement)
        {
            if (MoveRight)
            {
                transform.Translate(2 * Time.deltaTime * currSpeed, 0, 0);
                //transform.localScale = new Vector2(2,1);
                //MoveRight = false;
            }
            else
            {
                transform.Translate(-2 * Time.deltaTime * currSpeed, 0, 0);
                //transform.localScale = new Vector2(-2,1);
                //MoveRight = true;
            }
        }

        if ((Time.time - bulletHitStartTime) > 2.0)
        {
            //Increase speed of Tunnel Enemy.
            Debug.Log("ACTIAVTE SLOW");
            ActivateSlowSpeed();
            this.bulletHit = false;
        }
    }

    public void ActivateFastSpeed() {  
        currSpeed = fastSpeed;
        TunnelEnemy_SpriteRenderer.sprite = angrySprite;

    }

    public void BulletHit() {
        this.bulletHit = true;
        this.bulletHitStartTime = Time.time;
        ActivateFastSpeed();
    }

    public void ActivateSlowSpeed() {
        currSpeed = speed;
        TunnelEnemy_SpriteRenderer.sprite = normalFaceSprite;
    }

    private void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.name == ("LeftEnd")){
          MoveRight = true;
        }

        if(col.gameObject.name == ("RightEnd")){
          MoveRight = false;
        }

        if(col.gameObject.name == ("Player") || col.gameObject.name == ("Capsule"))
        {
            stopMovement = true;

            PlayerCaughtSourceSound.clip = PlayerCauhtClip;
            PlayerCaughtSourceSound.Play();

            StartCoroutine(WaitCoroutine());
            //col.gameObject.transform.position = TunnelSpawnPoint.position;

            if (SceneManager.GetActiveScene().name == "Tunnel 2-3")
            {
                analyticsManager.SendEvent("LEVEL3 PLAYER KILLED BY ENEMY IN YELLOW TUNNEL AT POSITION:" + GameObject.Find("Player").GetComponent<Rigidbody2D>().position);
            }
            if (SceneManager.GetActiveScene().name == "Level-3 Upgrade")
            {
                analyticsManager.SendEvent("LEVEL3 PLAYER KILLED BY ENEMY IN GREEN TUNNEL AT POSITION:" + GameObject.Find("Player").GetComponent<Rigidbody2D>().position);
            }
            if (SceneManager.GetActiveScene().name == "Level7")
            {
                analyticsManager.SendEvent("LEVEL7 PLAYER KILLED BY ENEMY IN GREEN TUNNEL AT POSITION:" + GameObject.Find("Player").GetComponent<Rigidbody2D>().position);
            }
            if (SceneManager.GetActiveScene().name == "Level7-Tunnel")
            {
                analyticsManager.SendEvent("LEVEL7 PLAYER KILLED BY ENEMY IN YELLOW TUNNEL AT POSITION:" + GameObject.Find("Player").GetComponent<Rigidbody2D>().position);
            }

            if (SceneManager.GetActiveScene().name == "Level6TunnelYellow")
            {
                analyticsManager.SendEvent("LEVEL6 PLAYER KILLED BY ENEMY IN YELLOW TUNNEL AT POSITION:" + GameObject.Find("Player").GetComponent<Rigidbody2D>().position);
            }

            if (SceneManager.GetActiveScene().name == "Level6")
            {
                analyticsManager.SendEvent("LEVEL6 PLAYER KILLED BY ENEMY IN YELLOW TUNNEL AT POSITION:" + GameObject.Find("Player").GetComponent<Rigidbody2D>().position);
            }
            if (SceneManager.GetActiveScene().name == "Level6Tunnel1")
            {

                analyticsManager.SendEvent("LEVEL6 PLAYER KILLED BY ENEMY IN GREEN TUNNEL AT POSITION:" + GameObject.Find("Player").GetComponent<Rigidbody2D>().position);
            }
        }
        if(col.gameObject.name == ("Capsule")){
            PlayerCaughtSourceSound.clip = PlayerCauhtClip;
            PlayerCaughtSourceSound.Play();

            StartCoroutine(WaitCoroutine());
            

            //if (SceneManager.GetActiveScene().name == "Tunnel 2-3")
            //{
            //    Debug.Log("Killed by Jumbo");
            //    analyticsManager.SendEvent("LEVEL3 PLAYER KILLED BY ENEMY IN YELLOW TUNNEL AT POSITION:" + GameObject.Find("Capsule").GetComponent<Rigidbody2D>().position);
            //}

          //  if (SceneManager.GetActiveScene().name == "Level-3 Upgrade"){
          //analyticsManager.SendEvent("LEVEL3 PLAYER KILLED BY ENEMY IN GREEN TUNNEL AT POSITION:"+ GameObject.Find("Capsule").GetComponent<Rigidbody2D>().position);
          // }
          //   if(SceneManager.GetActiveScene().name == "Level7"){
          //analyticsManager.SendEvent("LEVEL7 PLAYER KILLED BY ENEMY IN GREEN TUNNEL AT POSITION:"+ GameObject.Find("Capsule").GetComponent<Rigidbody2D>().position);
          // }
          //  if(SceneManager.GetActiveScene().name == "Level6"){
          //analyticsManager.SendEvent("LEVEL6 PLAYER KILLED BY ENEMY IN YELLOW TUNNEL AT POSITION:"+ GameObject.Find("Capsule").GetComponent<Rigidbody2D>().position);
          // }
        }

        if (col.gameObject.tag.Equals("Bullet"))
        {
            Destroy(col.gameObject); // make bullet disappear when it collides laser beam
            BulletHit();
        } 
    }

    IEnumerator WaitCoroutine()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        //Debug.Log("COROUTINE");
        yield return new WaitForSeconds(0.1f);
        PlayerSpawnSourceSound.clip = PlayerSpawnClip;
        PlayerSpawnSourceSound.Play();
        stopMovement = false;
        player.gameObject.transform.position = TunnelSpawnPoint.position;

    }


}
