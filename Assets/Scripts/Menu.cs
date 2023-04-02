using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    public GameObject window;

    [System.Obsolete]
    void Start()
    {
        
        var box = GameObject.Find("Modal Panel").GetComponent<CanvasRenderer>();
        box.isMask = false;
    }


    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void Tutorial()
    {
        Debug.Log("inside");

        //var box = GameObject.Find("Modal Panel").GetComponent<GameObject>();
        //box.SetActive(true);
        window.SetActive(true);
    }

    public void Controls()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
