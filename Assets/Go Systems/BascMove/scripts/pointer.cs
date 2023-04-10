using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gteem
{

    public class pointer : MonoBehaviour
    {

        public float UpAndDown = 0.5f;
        public float speed = 1;
        // public GameObject Geteffect;
        public bool DestroyWhenTouch = true;
        bool up = true, down;
        float SUD;
        Vector3 Up, pos, Down;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<Move>())
            {
                //Instantiate(Geteffect, transform);
                if (DestroyWhenTouch == true)
                {
                    Destroy(gameObject);
                }
            }
        }
        private void Update()
        {
            if (SUD != UpAndDown)
            {
                Up = new Vector3(gameObject.transform.position.x, transform.position.y + UpAndDown, transform.position.z);
                Down = new Vector3(gameObject.transform.position.x, transform.position.y - UpAndDown, transform.position.z);
                pos = transform.position;
                SUD = UpAndDown;
                print("Ddd");
            }
            if (up == true)
            {

                gameObject.transform.position = Vector3.MoveTowards(transform.position, Up, speed * Time.deltaTime);
                if (transform.position.y == Up.y)
                {
                    up = false;
                    down = true;
                }
            }
            if (down == true)
            {

                gameObject.transform.position = Vector3.MoveTowards(transform.position, Down, speed * Time.deltaTime);
                if (transform.position.y == Down.y)
                {
                    down = false;
                    up = true;
                }
            }


        }
    }
}