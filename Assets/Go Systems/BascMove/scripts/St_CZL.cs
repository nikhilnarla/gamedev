using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gteem {
    public class St_CZL
    {
        public static float speed, offSetPositionX, offSetPositionY;
        public static bool Catch, mobileInput, input = false,output,leftM,RightM,lookIn;
        public static KeyCode InputKey, Left, Right;
        public static void Climbzipline(float Speed, float OffsetPlayerX, float OffsetPlayerY, bool DivectCatch, KeyCode input, KeyCode left, KeyCode right,bool mobile)
        {
            speed = Speed;
            offSetPositionX = OffsetPlayerX;
            offSetPositionY = OffsetPlayerY;
            Catch = DivectCatch;
            mobileInput = mobile;
            InputKey = input;
            Left = left;
            Right = right;               
            
        }

   
    }
}
