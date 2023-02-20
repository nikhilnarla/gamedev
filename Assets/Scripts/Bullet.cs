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
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyMovement enemy = other.gameObject.GetComponent<EnemyMovement>();
            if (enemy != null)
            {
                Destroy(gameObject);
                // enemy.DisappearForSeconds(3f);
                Destroy(other.gameObject);
            }
        }
    }
}
