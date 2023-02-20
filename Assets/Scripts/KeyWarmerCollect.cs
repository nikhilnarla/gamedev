using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyWarmerCollect : MonoBehaviour
{

  private void OnCollisionEnter2D(Collision2D col)
  {
    if(col.gameObject.tag == "Player")
    {

        Debug.Log("KeyCollects");
        Destroy(GameObject.Find("KeyWarmer"));
    }
  }
}
