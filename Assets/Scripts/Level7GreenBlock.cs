using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level7GreenBlock : MonoBehaviour
{
    public GameObject dialogue;

   
    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Equals("Ground"))
        {
            dialogue.SetActive(true);
            StartCoroutine(WaitAndDisappear(3f));
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Ground"))
        {
            dialogue.SetActive(false) ;
        }
    }

    IEnumerator WaitAndDisappear(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        dialogue.SetActive(false);
    }
}
