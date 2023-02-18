using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public float life = 1.5f;
    // Start is called before the first frame update

    void Awake()
    {
        Destroy(gameObject, life);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-1 * speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("collided with enemy");
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "Enemy")
        {
            
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
