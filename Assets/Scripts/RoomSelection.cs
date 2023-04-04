using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RoomSelection : MonoBehaviour
{
    [SerializeField] public GameObject roomSelection;

    public void goToRoom(string sceneName) 
    { 
        SceneManager.LoadScene(sceneName);
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
