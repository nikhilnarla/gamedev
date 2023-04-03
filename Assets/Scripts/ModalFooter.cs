using UnityEngine;

public class ModalFooter : MonoBehaviour
{

    [SerializeField]
    private GameObject window;

    public void Close()
    {
        Debug.Log("controls");
        
        window.SetActive(false);
        
    }
}
