/// <summary>
/// Project: ProtoBlock Builder
/// Made for use in Projects: Unicorn Snake & Protoblocks (...more to come?)
/// Filename: BlockManager.cs
/// Created by PCPi & AltX
/// Written by: Gary Chisenhall
/// </summary>
#region /// USING
using UnityEngine;
#endregion
namespace PCPi.scripts.Managers
{
    [AddComponentMenu("PCPi/ProtoBlock Scene Manager")]
    public class BlockManager : MonoBehaviour
    {

        #region /// Properties
        private static GameObject selectedBlock;
        
        private static GameObject baseBlock;
        public static GameObject SelectedBlock { get => selectedBlock; set => selectedBlock = value; }
        public static GameObject BaseBlock { get => baseBlock; set => baseBlock = value; }
        #endregion

        #region /// Property Methods
        public static GameObject GetBaseBlock()
        {
            return BaseBlock;
        }

        public void SetBaseBlock(GameObject value)
        {
            BaseBlock = value;
        }

        public static GameObject GetSelectedBlock()
        {
            return SelectedBlock;
        }

        public void SetSelectedBlock(GameObject value)
        {
            SelectedBlock = value;
        }
        #endregion

    }
}