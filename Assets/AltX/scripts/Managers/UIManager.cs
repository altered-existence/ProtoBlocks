using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AltX.Managers
{
    public class UIManager : MonoBehaviour
    {
        public string[] modes = new string[3] { "Build", "Paint", "Remove" };
        public Text modeText;
        public void Update()
        {
            if (GameManager.GetIsBuildMode())
            {
                modeText.text = modes[0];
            }
            else if (GameManager.GetIsPaintMode())
            {
                modeText.text = modes[1];
            }
            else if (GameManager.GetIsEditMode())
            {
                modeText.text = modes[2];
            }
        }
    }
}