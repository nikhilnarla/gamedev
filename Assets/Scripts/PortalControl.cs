using UnityEngine;

public class PortalControl : MonoBehaviour
{

    public static PortalControl portalControlInstance;

    [SerializeField]
    private GameObject blackPortal, orangePortal;

    [SerializeField]
    private Transform blackPortalSpawiningPoint, orangePortalSpawningPoint;

    private Collider2D blackPortalCollider, orangePortalCollider;

    [SerializeField]
    public GameObject clone;

    // Start is called before the first frame update
    void Start()
    {
        portalControlInstance = this;
        blackPortalCollider = blackPortal.GetComponent<Collider2D>();
        orangePortalCollider = orangePortal.GetComponent<Collider2D>();
    }

    public void CreateClone(string whereToCreate)
    {
        if(whereToCreate == "atBlack")
        {
            var instantiateClone = Instantiate(clone, blackPortalSpawiningPoint.position, Quaternion.identity);
            instantiateClone.gameObject.name = "Clone";


        }
        else if (whereToCreate == "atOrange")
        {
            Debug.Log("orange clone created");
            var instclone = Instantiate(clone, orangePortalSpawningPoint.position, Quaternion.identity);
            instclone.gameObject.name = "Clone";

        }
    }

    public void DisableCollider(string colliderToDissable)
    {
        if(colliderToDissable == "orange")
        {
            orangePortalCollider.enabled = false;

        } else if(colliderToDissable == "black")
        {
            blackPortalCollider.enabled = false;
        }
    }

    public void EnableColliders()
    {
        orangePortalCollider.enabled = true;
        blackPortalCollider.enabled = true;
    }

}
