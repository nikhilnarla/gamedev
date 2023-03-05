using UnityEngine;

public class Laser : MonoBehaviour
{
    public float rotateSpeed;
    public float move;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // void Update()
    // {
    //     move = Input.GetAxis("Horizontal");
    //     transform.Rotate(new Vector3(0, 0, rotateSpeed), Space.Self);
    // }
    void Update()
    {
        Debug.Log(Time.deltaTime);
        move = Input.GetAxis("Horizontal");
        //transform.Rotate(0,0, rotateSpeed); // laser rotates 360 degrees
        //transform.Rotate(rotateSpeed, 0 ,0);
        // Multiply rotation speed with Time.deltaTime
        transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime), Space.Self);
    }
}
