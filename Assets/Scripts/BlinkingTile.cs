using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingTile : MonoBehaviour
{
    public float disappearTime = 0.0f;
    public GameObject blinkingPlatform;

    // Start is called before the first frame update
    void Start()
    {
        disappearTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
        if ((Time.time - disappearTime) > 5.0)
        {
            disappearTime = Time.time;
            blinkingPlatform.SetActive(true);
            //blinkingPlatform.GetComponent<SpriteRenderer>().enabled = true;
        }
        else if((Time.time - disappearTime) > 3.0){
            blinkingPlatform.SetActive(false);
            //blinkingPlatform.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
