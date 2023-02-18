using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippingTile1 : MonoBehaviour
{
    public Rigidbody2D flipBlock;
    public float rotationSpeed;
    private float rotZ;

    void Update(){
        rotZ += Time.deltaTime * rotationSpeed;
        transform.rotation = Quaternion.Euler(0,0,rotZ);
    }
}
