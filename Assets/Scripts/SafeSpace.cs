using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeSpace : MonoBehaviour
{
    public GameObject Player;
    Rigidbody m_Rigidbody;
    Collider m_ObjectCollider;
    Renderer rend;


    void Start()
    {
      m_Rigidbody = Player.GetComponent<Rigidbody>();
      m_ObjectCollider = Player.GetComponent<Collider>();
      rend = Player.GetComponent<Renderer>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
         if(col.gameObject.name == ("Player")){
          //Debug.Log("PLAYER FOUND IN SAFE SPACE 1");
          Physics2D.IgnoreLayerCollision(9,10,true);
          Color customColor = new Color(0.3f, 0.9f, 0.1f, 1.0f);
          rend.material.color = customColor;
         }
    }

    void OnTriggerExit2D(Collider2D col)
    {
         if(col.gameObject.name == ("Player")){
          //Debug.Log("PLAYER FOUND IN SAFE SPACE 1");
          Physics2D.IgnoreLayerCollision(9,10,false);
          rend.material.SetColor("_Color", Color.white);
         }
    }

}
