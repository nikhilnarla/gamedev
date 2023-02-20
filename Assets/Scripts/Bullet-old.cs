using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletOld : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public float life = 1.5f;
    // Start is called before the first frame update

    void Awake()
    {
        Destroy(gameObject, life);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Bullet collided with: " + other.gameObject.name);
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyMovement enemy = other.gameObject.GetComponent<EnemyMovement>();
            if (enemy != null)
            {
                enemy.DisappearForSeconds(3f);
                Destroy(gameObject);
            }
        }
    }

}



    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     Debug.Log("collided with enemy");
    //     Debug.Log(other.gameObject.name);
    //     if (other.gameObject.name == "Enemy")
    //     {
            
    //         Destroy(other.gameObject);
    //         Destroy(gameObject);
    //     }
    // }

    ///////////

    // public void DisappearForSeconds(float duration)
    // {
    //     gameObject.SetActive(false);
    //     Invoke("Appear", duration);
    // }

    // private void Appear()
    // {
    //     gameObject.SetActive(true);
    // }