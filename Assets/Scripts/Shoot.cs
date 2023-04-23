using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    //private bool superKeyCollected = false;
    public float speed;
    // public float bulletSpeed = 10;

    public AudioSource GunSourceSound;
    public AudioClip GunClip;

    // Start is called before the first frame update
    void Start()
    {
        bulletSpawnPoint = transform.Find("BulletSpawnPoint");
    }

    // Update is called once per frame
    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        Debug.Log(currentScene);

        // check if in level 3,4,5 to prevent shooting in level 1,2
        if (currentScene.name == "Level3" || currentScene.name == "Level4" || currentScene.name == "Level5" ||
            currentScene.name == "Level3GreenTunnel" || currentScene.name == "Level3YellowTunnel" || 
            currentScene.name == "Level4GreenTunnel" || currentScene.name == "Level4YellowTunnel") {
            if (PlayerMovement6.hasGun && Input.GetKeyDown(KeyCode.C)) 
            {
                // velocity + where bullet travels depending on which side the player is facing
                Debug.Log("Player is facing: " + PlayerMovement6.isFacingRight);
                float bulletSpeed = PlayerMovement6.isFacingRight ? speed : -speed;

                // check if it is in level 4
                if (currentScene.name == "Level4" || currentScene.name == "Level4GreenTunnel" || currentScene.name == "Level4YellowTunnel") {
                    Debug.Log("Player is facing: " + PlayerMovement7.isFacingRight);
                    bulletSpeed = PlayerMovement7.isFacingRight ? speed : -speed;
                } 
                else if (currentScene.name == "Level5") {
                    bulletSpeed = PlayerMovement5.isFacingRight ? speed : -speed;
                }

                GunSourceSound.clip = GunClip;
                GunSourceSound.Play();
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0f);
            }
        }
    }  
}