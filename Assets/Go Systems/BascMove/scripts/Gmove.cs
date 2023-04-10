using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gteem {
    public class Gmove
    {
        public static bool Mobile { get; set; }
        public static float vmobile { get; set; }
        public static bool IsGrwand { get; set; }
        public static void Jump(Rigidbody2D rigidbody,GameObject effect,LayerMask LayerGrawnd, float JumpForce = 5f,float LongIsGraund = 0.5f)
        {
            Animator animator = rigidbody.transform.GetComponent<Animator>();
           
      
            Vector2 feedPosition;
            IsGrwand = Physics2D.OverlapCircle(feedPosition = new Vector2(rigidbody.transform.position.x, rigidbody.transform.position.y - LongIsGraund), 0.1f, LayerGrawnd);

            if(IsGrwand == true)
            {
                rigidbody.velocity = Vector3.up * JumpForce;
                animator.SetTrigger("jump");
                effect.SetActive(true);
            }
            else
            {
                effect.SetActive(false);
            }

        }
        public static void camera(Transform camera,Vector3 OffSet,Transform Target,bool Flip = true)
        {

            if (Flip == true)
            {
                if (Target.localScale.x == 1)
                {
                    Vector3 t = new Vector3(Target.position.x + OffSet.x, Target.position.y + OffSet.y, OffSet.z);
                    camera.transform.position = Vector3.Lerp(camera.position, t, 5 * Time.deltaTime);
                }
                else if (Target.localScale.x == -1)
                {
                    Vector3 t = new Vector3(Target.position.x - OffSet.x, Target.position.y + OffSet.y, OffSet.z);
                    camera.transform.position = Vector3.Lerp(camera.position, t, 5 * Time.deltaTime);
                }
            }
            else
            {
                Vector3 t = new Vector3(Target.position.x + OffSet.x, Target.position.y + OffSet.y, OffSet.z);
                camera.transform.position = Vector3.Lerp(camera.position, t, 5 * Time.deltaTime);
            }
        }
        public static void Move(Rigidbody2D rigidbody,GameObject effect, float speed = 5f)
        {
            float x;
            if (Mobile == false)
            {
              x= Input.GetAxisRaw("Horizontal");
            }
            else
            {
                x = vmobile;
            }
            rigidbody.velocity = new Vector2(x * speed, rigidbody.velocity.y);
            if (x > 0)
            {
                rigidbody.transform.localScale = new Vector3(1, 1, 1);

            }
            else if (x < 0)
            {
                rigidbody.transform.localScale = new Vector3(-1, 1, 1);
            }
            if (x != 0)
            {
                rigidbody.GetComponent<Animator>().SetBool("run", true);
                effect.SetActive(true);
            }
            else
            {
                rigidbody.GetComponent<Animator>().SetBool("run", false);
                effect.SetActive(false);
            }
        }

        public static void Ladder(Rigidbody2D rigidbody)
        {

        }
    }
}
