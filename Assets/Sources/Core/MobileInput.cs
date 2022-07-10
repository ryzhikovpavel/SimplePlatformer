using System;
using UnityEngine;

namespace Sources
{
    public class MobileInput : MonoBehaviour
    {
        public int HorizontalAxis => LeftActive ? -1 : (RightActive ? 1 : 0);
        public bool Attack { get; private set; }
        public bool Jump { get; private set; }
        public bool Roll { get; private set; }
        
        public bool LeftActive { get; set; }
        public bool RightActive { get; set; }

        public void FireAttack()
        {
            Attack = true;
        }

        public void FireJump()
        {
            Jump = true;
        }

        public void FireRoll()
        {
            Roll = true;
        }

        private void LateUpdate()
        {
            Attack = false;
            Jump = false;
            Roll = false;
        }
    }
}