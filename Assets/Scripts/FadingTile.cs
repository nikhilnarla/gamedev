using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingTile : MonoBehaviour
{
    [SerializeField] private GameObject bridgeTile;

    void Update()
    {
    
    }

    private IEnumerator OnCollisionEnter2D(Collision2D other){

        if(other.gameObject.name == ("Capsule"))
        {   
            yield return new WaitForSeconds (1);

            var renderer = bridgeTile.GetComponent<Renderer>();
            renderer.enabled = false;
            renderer.GetComponent<BoxCollider2D>().enabled = false;
            yield return new WaitForSeconds (1);
            renderer.enabled = true;
            renderer.GetComponent<BoxCollider2D>().enabled = true;
        }
         
    }
}
