using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    private bool superKeyCollected = false;
    public float speed = 2;
    // public float bulletSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        bulletSpawnPoint = transform.Find("BulletSpawnPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find ("SuperKey") == null) {
            superKeyCollected = true;
        }

        if (superKeyCollected && Input.GetKeyDown(KeyCode.C))
        {

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            // bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 1).normalized * speed; //set bullet velocity to the left
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0f);
        }
    }
}