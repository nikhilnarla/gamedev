using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingTile : MonoBehaviour
{   
    private float timeToFade = 2f;
    private float currentTime = 0f;
    public AudioSource BridgeTileAudioSource;
    public AudioClip BridgeTileSound;

    [SerializeField] private GameObject bridgeTile;


    void Update()
    {
        if (currentTime == timeToFade){
            var renderer = bridgeTile.GetComponent<Renderer>();
            renderer.enabled = false;
        }
        currentTime += 1;
        
    }

    private IEnumerator OnCollisionEnter2D(Collision2D other){

        if(other.gameObject.name == ("Capsule"))
        {   
            yield return new WaitForSeconds (1);

            var renderer = bridgeTile.GetComponent<Renderer>();
            renderer.enabled = false;
            BridgeTileAudioSource.clip = BridgeTileSound;
            BridgeTileAudioSource.Play();
            renderer.GetComponent<BoxCollider2D>().enabled = false;
            yield return new WaitForSeconds (1);
            renderer.enabled = true;
            renderer.GetComponent<BoxCollider2D>().enabled = true;
        }
         
    }
}
