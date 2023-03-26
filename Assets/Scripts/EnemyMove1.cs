using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemyMove1 : MonoBehaviour
{
     public float speed;
     public bool MoveRight = false;
     public Transform TunnelSpawnPoint;
     public AnalyticsManager analyticsManager;

    // Update is called once per frame
    void Update()
    {
        if(MoveRight){
           transform.Translate(2*Time.deltaTime * speed,0,0);
           //transform.localScale = new Vector2(2,1);
           //MoveRight = false;
        }
        else{
           transform.Translate(-2*Time.deltaTime * speed,0,0);
           //transform.localScale = new Vector2(-2,1);
           //MoveRight = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.name == ("LeftEnd")){
          MoveRight = true;
        }

        if(col.gameObject.name == ("RightEnd")){
          MoveRight = false;
        }

        if(col.gameObject.name == ("Player")){
          col.gameObject.transform.position = TunnelSpawnPoint.position;

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
          col.gameObject.transform.position = TunnelSpawnPoint.position;

            if (SceneManager.GetActiveScene().name == "Tunnel 2-3")
            {
                Debug.Log("Killed by Jumbo");
                analyticsManager.SendEvent("LEVEL3 PLAYER KILLED BY ENEMY IN YELLOW TUNNEL AT POSITION:" + GameObject.Find("Capsule").GetComponent<Rigidbody2D>().position);
            }

            if (SceneManager.GetActiveScene().name == "Level-3 Upgrade"){
          analyticsManager.SendEvent("LEVEL3 PLAYER KILLED BY ENEMY IN GREEN TUNNEL AT POSITION:"+ GameObject.Find("Capsule").GetComponent<Rigidbody2D>().position);
           }
             if(SceneManager.GetActiveScene().name == "Level7"){
          analyticsManager.SendEvent("LEVEL7 PLAYER KILLED BY ENEMY IN GREEN TUNNEL AT POSITION:"+ GameObject.Find("Capsule").GetComponent<Rigidbody2D>().position);
           }
            if(SceneManager.GetActiveScene().name == "Level6"){
          analyticsManager.SendEvent("LEVEL6 PLAYER KILLED BY ENEMY IN YELLOW TUNNEL AT POSITION:"+ GameObject.Find("Capsule").GetComponent<Rigidbody2D>().position);
           }
        }
    }


}
