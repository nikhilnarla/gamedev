using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Gteem
{
    [GMonoBehaviourAttribute("Controler2D", true)]
    public class Move : GCustomMonoBehaviour
    {
        public float speed;
        public float jumpForce;
        public bool MobileInput = false;
        public LayerMask LayerJump;
        public KeyCode KeyJump;
        public Text TextScore;
        int score;
        bool lockCode;
        bool lockJump;
        [System.Serializable]
        public class Effect
        {
            public GameObject MoveEffect;
            public GameObject JumpEffect;
        }
        public Effect effect;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<pointer>())
            {
                ++score;
                TextScore.text = score.ToString();
            }
        }
        void Update()
        {
            Gmove.Mobile = MobileInput;
            if (lockCode == false)
            {
                Gmove.Move(gameObject.GetComponent<Rigidbody2D>(),effect.MoveEffect,speed);
            }
            if (lockJump == false)
            {
                if (Input.GetKeyDown(KeyJump))
                {
                    Gmove.Jump(gameObject.GetComponent<Rigidbody2D>(),effect.JumpEffect,LayerJump,jumpForce,1);
                }
            }
        }

        public void LockCode()
        {
            lockCode = true;
        }
        public void UnLockCode()
        {
            lockCode = false;
        }
        public void LockJump()
        {
            lockJump = true;
        }
        public void UnLockJump()
        {
            lockJump = false;
        }
        public void Jump()
        {
            Gmove.Jump(gameObject.GetComponent<Rigidbody2D>(), effect.JumpEffect, LayerJump, jumpForce, 1);
        }
        public void Right()
        {
            Gmove.vmobile = 1;
        }
        public void Left()
        {
            Gmove.vmobile = -1;
        }
        public void Out()
        {
            Gmove.vmobile = 0;
        }
    }
}