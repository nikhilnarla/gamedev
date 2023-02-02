using UnityEngine;

public class Movableblock : MonoBehaviour
{

    public GameObject key;
    
    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.name == "Collidertile")
        {
            Debug.Log("collision!");
            key.SetActive(true);
            GameObject.Find("diamond").GetComponent<SpriteRenderer>().enabled = true;

        }
    }

}
