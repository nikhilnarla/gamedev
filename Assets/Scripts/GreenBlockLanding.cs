using UnityEngine;

public class GreenBlockLanding : MonoBehaviour
{

    public Rigidbody2D greenBlock;
    public GunShooting Cannon;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Equals("Green Block"))
        {
            greenBlock.mass = 1000000.0f;
            Cannon.Target = null;

        }
    }
}
