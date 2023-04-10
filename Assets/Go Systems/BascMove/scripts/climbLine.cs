using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gteem
{
    [GMonoBehaviourAttribute("Climb Line", true)]
    public class climbLine : GCustomMonoBehaviour
    {
        #region Vareable
        float speed, offSetPositionX, offSetPositionY;
        public Transform pointA, pointB, PointC;
        Transform PointD;
        Animator anim;
        Vector3 fixPos;
        bool lookIn;
        bool Catch;
        bool mobile;
    
        KeyCode inputCatch = KeyCode.E, Left = KeyCode.A, Right = KeyCode.D;
        // GameObject UiEButton;
        bool lockcode = true;
        bool lockInput;
        Vector3 Fix;
        Rigidbody2D rb;
        public UnityEngine.Events.UnityEvent OnStartAction, OnEndAction, OnMoveOnLine,OnTrigger,OnExitTrigger;
        #endregion
        private void Start()
        {
            Fix = PointC.position;

        }
        private void OnTriggerStay2D(Collider2D collision)
        {

            if (collision.tag == "Player")
            {
                OnTrigger.Invoke();
                lookIn = true;
                if (mobile == false)
                {
                    if (Catch == false)
                    {

                        if (Input.GetKey(inputCatch) && lockInput == false)
                        {

                            inputcontrol(collision.gameObject);
                        }

                    }
                    else
                    {
                        if (lockInput == false)
                        {
                            inputcontrol(collision.gameObject);
                        }
                    }
                }
                else
                {
                    if (lockInput == false)
                    {
                        if (Catch == true)
                        {
                           inputcontrol(collision.gameObject);
                        }
                        else if (Catch == false)
                        {
                            if (St_CZL.input == true)
                            {
                                inputcontrol(collision.gameObject);
                            }
                        }
                    }
                }
        
            }

        }
        void inputcontrol(GameObject TargetPosition)
        {

            rb = TargetPosition.GetComponent<Rigidbody2D>();
            rb.simulated = false;
            lockcode = false;

            Invoke("input", 0.1f);

            anim = TargetPosition.GetComponent<Animator>();
            anim.SetBool("climb", true);

            PointD = TargetPosition.GetComponent<Transform>();
            PointC.position = new Vector3(PointD.position.x, PointC.position.y, PointC.position.z);
            Fix = PointC.position;

            OnStartAction.Invoke();

        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            
        
            if (collision.tag == "Player")
            {
                St_CZL.input = false;
             
                OnExitTrigger.Invoke();
            }
        }
        private void Update()
        {
            mobile = Gmove.Mobile;
            St_CZL.lookIn = lookIn;
            mobile = St_CZL.mobileInput;
            Catch = St_CZL.Catch;
            if (lockcode == false)
            {

                speed = St_CZL.speed;
                offSetPositionX = St_CZL.offSetPositionX;
                offSetPositionY = St_CZL.offSetPositionY;
                inputCatch = St_CZL.InputKey;
                Left = St_CZL.Left;
                Right = St_CZL.Right;
                var pos = Fix;
                PointD.position = new Vector3(PointC.position.x + offSetPositionX, PointC.position.y + offSetPositionY, PointC.position.z);
                PointC.position = Vector3.Lerp(PointC.position, pos, speed * Time.deltaTime);

                if (mobile == false)
                {
                    if (Input.GetKeyDown(Right))
                    {
                        if (Fix.x + 0.5f < pointB.position.x)
                        {
                            Fix = new Vector3(PointC.position.x + 0.5f, PointC.position.y, PointC.position.z);
                            fixPos = Fix;
                            OnMoveOnLine.Invoke();
                            anim.Play("MoveClimb", 0);
                        }
                    }
                    if (Input.GetKeyDown(Left))
                    {
                        if (Fix.x - 0.5f > pointA.position.x)
                        {
                            Fix = new Vector3(PointC.position.x - 0.5f, PointC.position.y, PointC.position.z);
                            fixPos = Fix;
                            anim.Play("MoveClimb", 0);
                            OnMoveOnLine.Invoke();
                        }
                    }

                    if (Input.GetKeyDown(inputCatch) && lockInput == true)
                    {
                        rb.simulated = true;
                        rb.velocity = Vector3.zero;
                        Invoke("unInput", 0.3f);
                        anim.SetBool("climb", false);
                        Fix = Vector3.zero;
                        pos = Vector3.zero;
                        lockcode = true;
                        OnEndAction.Invoke();
                    }
                }
                else if (mobile == true)
                {
                    if(St_CZL.RightM == true)
                    {
                        if (Fix.x + 0.5f < pointB.position.x)
                        {
                            Fix = new Vector3(PointC.position.x + 0.5f, PointC.position.y, PointC.position.z);
                            fixPos = Fix;
                            OnMoveOnLine.Invoke();
                            anim.Play("MoveClimb", 0);
                            St_CZL.RightM = false;
                        }
                    }

                    if (St_CZL.leftM == true)
                    {
                        if (Fix.x - 0.5f > pointA.position.x)
                        {
                            Fix = new Vector3(PointC.position.x - 0.5f, PointC.position.y, PointC.position.z);
                            fixPos = Fix;
                            anim.Play("MoveClimb", 0);
                            OnMoveOnLine.Invoke();
                            St_CZL.leftM = false;
                        }
                    }
                    if (St_CZL.output == true)
                    {
                        rb.simulated = true;
                        rb.velocity = Vector3.zero; 
                        Invoke("unInput", 0.5f);
                        anim.SetBool("climb", false);
                        Fix = Vector3.zero;
                        pos = Vector3.zero;
                        lockcode = true;
                        OnEndAction.Invoke();
                        lookIn = false;
                        St_CZL.input = false;
                        St_CZL.output = false;
                    }
                }
            }
        }
    
        void input()
        {
                lockInput = true; 
        }
        void unInput()
        {
            lockInput = false; 
        }
    }
}