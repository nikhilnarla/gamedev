using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPadScript : MonoBehaviour
{

    public Vector2 target;

    [SerializeField] private GameObject tarObj;

    // Start is called before the first frame update
    void Start()
    {
        target = tarObj.transform.position;
    }

}
