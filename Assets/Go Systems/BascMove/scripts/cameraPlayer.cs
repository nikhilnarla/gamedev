using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gteem
{
    public class cameraPlayer : MonoBehaviour
    {
        public Transform Target;
        public float Dis = -15f;
        public bool FixPosWithFlipFace;
        public Vector2 OffSetPositionCamera;
        
        // Update is called once per frame
        void Update()
        {
            Vector3 offset = new Vector3(OffSetPositionCamera.x, OffSetPositionCamera.y, Dis);
            Gmove.camera(transform, offset, Target,FixPosWithFlipFace);
        }
    }
}
