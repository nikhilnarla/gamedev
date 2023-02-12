using UnityEngine;

public class MovableblockLevel1 : MonoBehaviour
{

    public GameObject key;
    bool keyFound = false;
    private Rigidbody2D rb;
    public float move;
    public float speed;
    public Vector3 startPosition;
    public bool onFloor = false;

   void Start()
    {
         rb = GetComponent<Rigidbody2D>();
         startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = rb.velocity; //get the velocity
        v.y = v.y > 0 ? 0 : v.y; //set to 0 if positive
        if (v.y == 0 && !onFloor)
        {
            transform.position = startPosition;
        }
        rb.velocity = v; //apply the velocity

    }


    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.name == "Collidertile")
        {
            Debug.Log("collision!");
            key.SetActive(true);
            GameObject.Find("Diamond").GetComponent<SpriteRenderer>().enabled = true;

            if(!keyFound)
            {
            keyFound = true;
            }
        }
        else if (col.gameObject.name == "rightcollider")
        {
            Debug.Log("collision right wall!");

            rb.velocity = new Vector2(-1,0);
        }

        else if (col.gameObject.name == "Square")
        {
            onFloor = true;
        }
    }

}
