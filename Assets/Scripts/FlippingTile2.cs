using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippingTile2 : MonoBehaviour
{
  public Rigidbody2D flipBlock;
  public AnalyticsManager analyticsManager;

  public float rotationSpeed;
  private float rotZ;

  void Update(){
      rotZ -= Time.deltaTime * rotationSpeed;
      transform.rotation = Quaternion.Euler(0,0,rotZ);
  }

  private void OnCollisionEnter2D(Collision2D Collision)
    {
        if (Collision.gameObject.tag == "Player")
        {
            analyticsManager.SendEvent("LEVEL6 PLAYER IS ON ROTATING BLOCK 2");
        }
    }
}
