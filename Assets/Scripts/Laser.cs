using UnityEngine;

public class Laser : MonoBehaviour
{
    public float rotateSpeed;
    public float move;


    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Horizontal");
        //transform.Rotate(0,0, rotateSpeed); // laser rotates 360 degrees
        //transform.Rotate(rotateSpeed, 0 ,0);
        transform.Rotate(new Vector3(0, 0, rotateSpeed), Space.Self);
    }
}
