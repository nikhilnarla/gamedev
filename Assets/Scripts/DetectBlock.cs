using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBlock : MonoBehaviour
{
  bool Capsule = false;
  bool block = false;
  public GameObject diamond;
  public GameObject greenTile;
  private bool KeyFound = false;

  public void keyFound(bool val){
    KeyFound = val;
  }

  void OnCollisionEnter2D(Collision2D col)
  {
    if (col.gameObject.name == "Capsule")
    {
      Capsule = true;

    }
    if(col.gameObject.name == "GreenBlock"){
      block = true;
    }

    if(Capsule || block){
      // Get the Renderer component from GameObject
      var objRenderer = greenTile.GetComponent<Renderer>();
      objRenderer.material.SetColor("_Color", Color.green);

      if(!KeyFound){
      diamond.SetActive(true);
      GameObject.Find("Diamond").GetComponent<SpriteRenderer>().enabled = true;
    }
    }
  }

  void OnCollisionExit2D(Collision2D col)
  {
    if(col.gameObject.name == "Capsule"){
      Capsule = false;
    }
    if(col.gameObject.name == "GreenBlock"){
      block = false;
    }


    if(!Capsule && !block){
      // Get the Renderer component from GameObject
      var objRenderer = greenTile.GetComponent<Renderer>();
      objRenderer.material.SetColor("_Color", Color.white);

      if(!KeyFound){
      GameObject.Find("Diamond").GetComponent<SpriteRenderer>().enabled = false;
      diamond.SetActive(false);
      }
    }
  }
}
