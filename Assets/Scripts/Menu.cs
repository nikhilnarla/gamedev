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
        SceneManager.LoadScene("Level1");
    }

    public void Tutorial()
    {
        Debug.Log("inside");
        SceneManager.LoadScene("Tutorial1");

        //var box = GameObject.Find("Modal Panel").GetComponent<GameObject>();
        //box.SetActive(true);
        //window.SetActive(true);
    }

    public void Controls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void RoomSelection()
    {
        SceneManager.LoadScene("Rooms");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Menu");
    }
}
