using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerTrigger : MonoBehaviour
{

  public GameObject Player;
  Collider m_ObjectCollider;

    void OnTriggerEnter2D(Collider2D col)
    {
         if(col.gameObject.name == ("Player")){
           Debug.Log("PLAYER -- TRIGGER -- FOUND");
           m_ObjectCollider = Player.GetComponent<Collider>();
           m_ObjectCollider.isTrigger = true;
         }
    }
}
