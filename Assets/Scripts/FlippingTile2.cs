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
                Collision.gameObject.GetComponent<PlayerMovement6>().jump = 5.5f;

                analyticsManager.SendEvent("LEVEL6 PLAYER IS ON ROTATING BLOCK 2");
            }
        }

    private void OnCollisionExit2D(Collision2D Collision)
        {
            if (Collision.gameObject.tag == "Player")
            {   
                Collision.gameObject.GetComponent<PlayerMovement6>().jump = 6.5f;
                
                analyticsManager.SendEvent("LEVEL6 PLAYER IS ON ROTATING BLOCK 2");
            }
        }
}
