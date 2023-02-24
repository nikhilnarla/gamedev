using UnityEngine;

public class Portals : MonoBehaviour
{
    private Rigidbody2D enteredRigidBody;
    private float enterVelocity, exitVelocity;


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    enteredRigidBody = collision.gameObject.GetComponent<Rigidbody2D>();
    //    enterVelocity = enteredRigidBody.velocity.x;

    //    if (gameObject.name == "BlackPortal")
    //    {
    //        Debug.Log("Black Clone!");
    //        PortalControl.portalControlInstance.DisableCollider("orange");
    //        PortalControl.portalControlInstance.CreateClone("atOrange");

    //    }
    //    else if (gameObject.name == "OrangePortal")
    //    {
    //        Debug.Log("Orange Clone!");
    //        PortalControl.portalControlInstance.DisableCollider("black");
    //        PortalControl.portalControlInstance.CreateClone("atBlack");
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    exitVelocity = enteredRigidBody.velocity.x;

    //    //if (enterVelocity != exitVelocity)
    //    //{
    //    //    Destroy(GameObject.Find("Clone"));

    //    //}
    //    if (gameObject.name != "Clone")
    //    {
    //        Debug.Log("Player dead");
    //        Destroy(collision.gameObject);
    //        PortalControl.portalControlInstance.EnableColliders();
    //        GameObject.Find("Clone").name = "Player";
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {


        enteredRigidBody = collision.gameObject.GetComponent<Rigidbody2D>();
        enterVelocity = enteredRigidBody.velocity.x;

        if (gameObject.name == "BlackPortal")
        {
            Destroy(collision.gameObject);
            PortalControl.portalControlInstance.DisableCollider("orange");
            PortalControl.portalControlInstance.CreateClone("atOrange");

        }
        else if (gameObject.name == "OrangePortal")
        {
            PortalControl.portalControlInstance.DisableCollider("black");
            PortalControl.portalControlInstance.CreateClone("atBlack");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        exitVelocity = enteredRigidBody.velocity.x;

        if (enterVelocity != exitVelocity)
        {
           Destroy(GameObject.Find("Clone"));

        }
        if (gameObject.name != "Clone")
        {
            Destroy(collision.gameObject);
            PortalControl.portalControlInstance.EnableColliders();
            GameObject.Find("Clone").name = "Player";
        } else
        {
            PortalControl.portalControlInstance.EnableColliders();
            GameObject.Find("Clone").name = "Player";
        }
    }

}
