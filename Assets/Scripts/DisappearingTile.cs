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

        if(other.gameObject.name == ("Capsule") || other.gameObject.name == ("Player"))
        {   
            yield return new WaitForSeconds (1);

            var renderer = bridgeTile.GetComponent<Renderer>();
            renderer.enabled = false;
            bridgeTile.GetComponent<BoxCollider2D>().enabled = false;
        }
         
    }

    private IEnumerator OnCollisionExit2D(Collision2D other){

        if(other.gameObject.name == ("Capsule") || other.gameObject.name == ("Player"))
        {   
            yield return new WaitForSeconds (3);

            var renderer = bridgeTile.GetComponent<Renderer>();
            renderer.enabled = true;
            bridgeTile.GetComponent<BoxCollider2D>().enabled = true;
        }
         
    }

    void ToggleTile()
    {   
        Debug.Log(bridgeTile.name);
    }

}
