using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippingTile2 : MonoBehaviour
{
    public Rigidbody2D flipBlock;
    public AnalyticsManager analyticsManager;

    public float rotationSpeed;
    private float rotZ;
    public float jumpOnTile;

    void Update(){
        rotZ -= Time.deltaTime * rotationSpeed;
        transform.rotation = Quaternion.Euler(0,0,rotZ);
    }
}
