using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace PCPi.scripts.Managers
{
    public class InputManager : MonoBehaviour
    {
        //[SerializeField]
        //public static float h, v;
        //[SerializeField]
        //public static Vector3 pos;
        [SerializeField]
        public static bool leftClick;
        [SerializeField]
        public static bool rightClick;
        [SerializeField]
        public static bool middleClick;
        public static Touch[] touches;
        public void Update()
        {
            
            //h = CrossPlatformInputManager.GetAxisRaw("Horizontal");
            //v = CrossPlatformInputManager.GetAxisRaw("Vertical");
            //pos = CrossPlatformInputManager.mousePosition;
            leftClick = CrossPlatformInputManager.GetButtonDown("Fire1");
            rightClick = CrossPlatformInputManager.GetButtonDown("Fire2");
            middleClick = CrossPlatformInputManager.GetButtonDown("Fire3");
        }
    }
}