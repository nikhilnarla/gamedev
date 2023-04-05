using UnityEngine;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
    public void CloseControls()
    {
        Debug.Log("controls");
        SceneManager.LoadScene("Menu");
    }
}
