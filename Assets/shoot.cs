using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    private bool superKeyCollected = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find ("SuperKey") == null) {
             superKeyCollected = true;
         }
        if (superKeyCollected && Input.GetKeyDown(KeyCode.C))
        {
            Instantiate(bulletPrefab,bulletSpawnPoint);
        }
    }
}
