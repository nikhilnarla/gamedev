using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jump;

    public float move;
    public bool isJumping=false;
    // Start is called before the first frame update
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // GameObject.Find("diamond").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * move, rb.velocity.y);

        //GetComponent<Rigidbody>().velocity = new Vector3(0, 5, 0);

        if(Input.GetKeyDown("space") && !isJumping){
            rb.velocity =  new Vector2(rb.velocity.x, jump);
            isJumping = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.name == ("Jumpingtile")){
            rb.velocity = new Vector2(rb.velocity.x,jump*3);
        }

        if(other.gameObject.CompareTag("Ground")){
            isJumping = false;
        }
    }

    // private void OnCollisionExit2D(Collision2D other){
    //     if(other.gameObject.CompareTag("Ground")){
    //         isJumping = true;
    //     }
    // }
}
