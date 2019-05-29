
using UnityEngine;
using PCPi.scripts.Managers;

namespace AltX.Managers
{
    public class GameManager : MonoBehaviour
    {
        private static bool isBuildMode;
        private static bool isPaintMode;
        private static bool isEditMode;

        public static bool GetIsEditMode()
        {
            return isEditMode;
        }

        public void SetIsEditMode(bool value)
        {
            isEditMode = value;
        }

        public static bool GetIsBuildMode()
        {
            return isBuildMode;
        }

        public void SetIsBuildMode(bool value)
        {
            isBuildMode = value;
        }

        public static bool GetIsPaintMode()
        {
            return isPaintMode;
        }

        public void SetIsPaintMode(bool value)
        {
            isPaintMode = value;
        }
        private void Start()
        {
            isBuildMode = true;
            isPaintMode = false;
            isEditMode = false;
        }
    }
}

