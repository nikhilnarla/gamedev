using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.position.x > 0.9 && rb.position.x < 3.4)
        {
            float xPos = rb.position.x;
            Debug.Log("Rigidbody X position is: " + xPos);
            rb.gravityScale = -1;
        }
        else{
            rb.gravityScale = 1;
        }        
    }
}
