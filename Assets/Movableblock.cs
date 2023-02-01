using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movableblock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.name == "Collidertile")
        {
            Debug.Log("collision!");
            GameObject.Find("diamond").GetComponent<SpriteRenderer>().enabled = true;
            
        }
    }

}
