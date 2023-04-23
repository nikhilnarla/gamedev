using System.Collections;
using TMPro;
using UnityEngine;

public class TutorialMovingBlock : MonoBehaviour
{
    public GameObject movePosition;
    private Vector2 target;
    public GameObject jumpPad;
    public GameObject greenblock;
    public AudioSource YellowKeyVisisbleSource;
    public AudioClip YellowKeySound;

    public TextMeshPro text;


    public void Start()
    {
        target = movePosition.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Equals("MovingGround"))
        {
            var box = GameObject.Find("MovingGround").GetComponent<Renderer>();
            box.transform.position = Vector2.MoveTowards( target, transform.position, 0.00001f * Time.deltaTime);
        }

        if(collision.gameObject.name.Equals("Button"))
        {
            AddGravityToTiles();
            jumpPad.SetActive(true);
            greenblock.SetActive(true);
            YellowKeyVisisbleSource.clip = YellowKeySound;
            YellowKeyVisisbleSource.Play();
            text.text = "jump on jumppad";
        }
    }

    void AddGravityToTiles()
    {
        Rigidbody2D tile = null;
        for (int i = 0; i < 5; i += 1)
        {
            tile = GameObject.Find("Tile" + i).GetComponent<Rigidbody2D>();
            tile.constraints = RigidbodyConstraints2D.None;
            tile.gravityScale = 1;
        }
    }
}
