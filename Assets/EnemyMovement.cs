using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rB;
    public float WalkSpeed = 2;

    private float Rightleft = 1;
    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
  
    void Update()
    {
        
        if (transform.position.x < -2.7)
        { 
            rB.velocity = new Vector2(WalkSpeed * Rightleft, 0);
        }
 
        if (transform.position.x > -0.6)
        {
            rB.velocity = new Vector2(WalkSpeed * Rightleft * -1, 0);  
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if ( other.gameObject.name == "Bullet")
        {
            Destroy(gameObject);
        }
    } 
    
}