using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 target;
    public GameObject fallPosition; 

    void Start()
    {
        target = fallPosition.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Player").GetComponent<PlayerMovement7>().lowerPath){
            transform.position = Vector2.MoveTowards(transform.position, target, 15*Time.deltaTime);
        }
    }
}
