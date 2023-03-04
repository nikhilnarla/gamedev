using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove1 : MonoBehaviour
{
     public float speed;
     public bool MoveRight = false;
     public Transform TunnelSpawnPoint;

    // Update is called once per frame
    void Update()
    {
        if(MoveRight){
           transform.Translate(2*Time.deltaTime * speed,0,0);
           transform.localScale = new Vector2(2,1);
           //MoveRight = false;
        }
        else{
           transform.Translate(-2*Time.deltaTime * speed,0,0);
           transform.localScale = new Vector2(-2,1);
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
        }
    }


}
