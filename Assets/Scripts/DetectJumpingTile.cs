using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectJumpingTile : MonoBehaviour
{
    public GameObject jumpingTile;
    public GameObject jumpDetector;

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.name == "Capsule")
        {
            Debug.Log("jumping Tile!");
            jumpingTile.SetActive(true);
        }
    }
}
