using UnityEngine;

public class gateopeneningkey : MonoBehaviour
{
    public GameObject key;
   
    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.name == "Capsule" && key.activeSelf)
        {
            Debug.Log("collision key!");
            Destroy(GameObject.Find("gate"));
            
        }
    }    
}
