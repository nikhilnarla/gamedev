using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life;
    public float speed;
    private Rigidbody2D rb;

    void Awake()
    {
        Destroy(gameObject, life);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("GateKey"))
        { 
            // destroy bullet if it hits anything with like ground, wall, gatekey, etc.
            Destroy(gameObject);
        }
        else if (other.transform.IsChildOf(GameObject.Find("Portals").transform))
        {
            // destroy bullet if it hits anything that is a child of the "Portals" parent
            Destroy(gameObject);
        }
    }
}
