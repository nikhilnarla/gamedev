using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingTile : MonoBehaviour
{    

    [SerializeField] private GameObject bridgeTile;

    void Update()
    {
    
    }

    private IEnumerator OnCollisionEnter2D(Collision2D other){

        if(other.gameObject.name == ("Capsule"))
        {   
            yield return new WaitForSeconds (2);

            var renderer = bridgeTile.GetComponent<Renderer>();
            renderer.enabled = false;
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
         
    }

    private IEnumerator OnCollisionExit2D(Collision2D other){

        if(other.gameObject.name == ("Capsule"))
        {   
            yield return new WaitForSeconds (4);

            var renderer = bridgeTile.GetComponent<Renderer>();
            renderer.enabled = true;
            other.collider.enabled = true;
        }
         
    }

    void ToggleTile()
    {   
        Debug.Log(bridgeTile.name);
        // bridgeTile.enabled = false;
        // bridgeTile..GetComponent<BoxCollider2D>().enabled = false;
    }

}
