using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPadScript : MonoBehaviour
{

    public Vector2 target;

    [SerializeField] private GameObject tarObj;
    public AudioSource JumpingSoundSrc;
    public AudioClip JumpingSound;

    // Start is called before the first frame update
    void Start()
    {
        target = tarObj.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Capsule"))
        {
            JumpingSoundSrc.clip = JumpingSound;
            JumpingSoundSrc.Play();
        }
    }

}
