using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameMaster gm;

    void Start() {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>(); // gm attached to game object
    }
    void OnTriggerEnter2D(Collider2D other) { // called as soon checkpoint collides w another object in the scene
        if (other.CompareTag("Player")) { // make sure it's player character that collided w object
            gm.lastCheckPointPos = transform.position;
        }
    }
}
