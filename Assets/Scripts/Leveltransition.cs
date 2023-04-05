using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Leveltransition : MonoBehaviour
{
    public Animator transition;
    // public int lvlindex=0;
    public string levelName;

    public float transitionTime = 2.5f;
    

    // Update is called once per frame
    void Update()
    {
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(levelName));
    }

    IEnumerator LoadLevel(string levelName)
    {
        yield return new WaitForSeconds(transitionTime);

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime-1);

        SceneManager.LoadScene(levelName);
    }
}
