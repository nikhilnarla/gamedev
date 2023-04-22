using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life;
    public float speed;
    private Rigidbody2D rb;
    public EnemyMove1 TunnelEnemy;
    public bool bulletHitTunnelEnemy = false;
    private float bulletHitStartTime = 0.0f;

    private void Start()
    {
        TunnelEnemy = GameObject.Find("TunnelEnemy").GetComponent<EnemyMove1>();
    }
    void Awake()
    {
        Destroy(gameObject, life);
    }

    private void Update()
    {
        ////Debug.Log("BulletHIT FLAG"+bulletHitTunnelEnemy);
        //Debug.Log("Time Elapsed"+(Time.time - bulletHitStartTime));
        //if ((Time.time - bulletHitStartTime) > 2.0){
        //    //Increase speed of Tunnel Enemy.
        //    Debug.Log("ACTIAVTE SLOW");
        //    TunnelEnemy.ActivateSlowSpeed();
        //    this.bulletHitTunnelEnemy = false;
        //}
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
        else if (other.gameObject.CompareTag("TunnelEnemy")) {
            //Debug.Log("Hit Tunnel Enemy");
            TunnelEnemy.BulletHit();

            //this.bulletHitStartTime = Time.deltaTime;
            //if (!bulletHitTunnelEnemy) {
            //    TunnelEnemy.ActivateFastSpeed();
            //}
            //this.bulletHitTunnelEnemy = true;
            Destroy(gameObject);
        }
    }
}
