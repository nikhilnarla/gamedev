using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Movement : MonoBehaviour
{
    public float speed;
    public float jump;
    public float move;
    public bool isJumping = false;

    Dictionary<string, bool> bridgeStatus = new Dictionary<string, bool>();

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask buttonLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        IntializeBridgeTiles();
    }

    void Update()
    {
        move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * move, rb.velocity.y);

        if(Input.GetKeyDown("space") && IsGrounded()){
            rb.velocity =  new Vector2(rb.velocity.x, jump);
            isJumping = true;
        }

    }

     private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D other){

        if(other.gameObject.name == ("Tile 5") && bridgeStatus.ContainsValue(false)){
                ShowTiles();
        }
         
         if(other.gameObject.name == ("Button")){
                AddGravityToTiles();
                DestroyBridgeTiles();
        }
    }

    private void IntializeBridgeTiles(){
        ShowTiles();
    }

    private void ShowTiles(){

        for (int i = 0; i<3; i+=1)
        {
            var bridgeTile = GameObject.Find("Bridge Tile "+i).GetComponent<Renderer>();
            bridgeTile.enabled = !bridgeTile.enabled;
            
            var collider = bridgeTile.GetComponent<BoxCollider2D>();
            collider.enabled = !collider.enabled;

            bridgeStatus["Bridge Tile "+i] = bridgeTile.enabled;
        }

    }

    void AddGravityToTiles()
    {   
        Rigidbody2D tile = null;
        for(int i=0; i<7; i+=1)
        {
            tile = GameObject.Find("Tile "+i).GetComponent<Rigidbody2D>();   
            tile.gravityScale = 1;
        }
    }

    void DestroyBridgeTiles()
    {
        for (int i = 0; i<3; i+=1)
        {
            var bridgeTile = GameObject.Find("Bridge Tile "+i);
            Destroy(bridgeTile);
        }
    }

}
