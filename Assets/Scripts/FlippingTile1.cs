using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippingTile1 : MonoBehaviour
{
    public Rigidbody2D flipBlock;
    public float rotationSpeed;
    private float rotZ;
    public AnalyticsManager analyticsManager;


    void Update(){
        rotZ += Time.deltaTime * rotationSpeed;
        transform.rotation = Quaternion.Euler(0,0,rotZ);
    }
     private void OnCollisionEnter2D(Collision2D Collision)
    {
        if (Collision.gameObject.tag == "Player")
        {
            analyticsManager.SendEvent("LEVEL6 PLAYER IS ON ROTATING BLOCK 1");
        }
    }
}
