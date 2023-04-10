using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gteem {

    [GMonoBehaviourAttribute("Climb Line system", true)]
    public class ClimbLineControl : GCustomMonoBehaviour
    {
        public float Speed;
        public bool mobileInput;
        public float OffsetPlayerX;
        public float OffetPlayerY;
        public bool DircectCatch;
        bool look;
  
        [Header("Input")]
        public KeyCode Input = KeyCode.E;
        public KeyCode Left = KeyCode.A;
        public KeyCode Right = KeyCode.D;
        float In=0;
        // Update is called once per frame
        void Update()
        {
            mobileInput = Gmove.Mobile;
            St_CZL.Climbzipline(Speed, OffsetPlayerX, OffetPlayerY, DircectCatch, Input, Left, Right,mobileInput);
            St_CZL.mobileInput = mobileInput;
            look = St_CZL.lookIn;
           
        }


        public void MoveLeft()
        {
            St_CZL.leftM = true;
        }

        public void MoveRight()
        {
            St_CZL.RightM = true;
        }
        public void output()
        {

            if (In == 1)
            {
                if (St_CZL.lookIn == true)
                {
                    St_CZL.output = true;
                    Invoke("inside", 0.1f);
                }
            }
            
        }
        public void inputStart()
        {
            if (St_CZL.lookIn == true)
            {
                if (In == 0)
                {
                    St_CZL.input = true;
                    Invoke("inside", 0.1f);
                }
            }
        }

        void inside()
        {
            if (In == 0)
            {
                In = 1;
            }
            else
            {
                In = 0;
            }
        }
    }
}
