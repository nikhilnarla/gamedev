using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemyMoveFromGround : MonoBehaviour
{
    public float speed;
    public float distance;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newX = startPosition.x + Mathf.PingPong(Time.time * speed, distance);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}



