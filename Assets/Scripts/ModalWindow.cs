using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modalWindow : MonoBehaviour
{

    [SerializeField]
    private GameObject window;

    void onClose()
    {
        window.SetActive(false);
    }
}