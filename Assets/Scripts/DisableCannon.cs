using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCannon : MonoBehaviour
{
    public GunShooting Cannon;
    public GameObject CannonBase;
    public GameObject CannonShooter;
    public GameObject CannonAlarm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionExit2D(Collision2D other) {

        if (other.gameObject.name.Equals("Green Block"))
        {
            Cannon.Range = 1;
            CannonBase.GetComponent<Renderer>().material.color = Color.gray;
            CannonShooter.GetComponent<Renderer>().material.color = Color.gray;
            CannonAlarm.GetComponent<Renderer>().material.color = Color.gray;

            // enable Collider2D components
            CannonBase.GetComponent<Collider2D>().enabled = true;
            CannonShooter.GetComponent<Collider2D>().enabled = true;
        }

    }
}
