using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenu;
    [SerializeField] public GameObject pauseButton;

    public void PauseGame() 
    {
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ResumeGame() 
    {
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }

    public void GoToMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void RestartLevel(string sceneName) 
    { // especially needed for tunnels level -> gets respawned back to beginning of level (not tunnel)
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}
