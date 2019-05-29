/// <summary>
/// Project: ProtoBlock Builder
/// Made for use in Projects: Unicorn Snake & Protoblocks (...more to come?)
/// Filename: BlockSpawnManager.cs
/// Created by PCPi & AltX
/// Written by: Gary Chisenhall
/// </summary>
#region /// USING
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
namespace AltX.Managers
{
    public class BlockSpawnManager : MonoBehaviour
    {
        static List<GameObject> spawnList = new List<GameObject>();

        private static GameObject[] SpawnPool { get; set; }

        public static void BlockDestruct(GameObject gameObject)
        {
            if (spawnList != null)
            {
                Destroy(gameObject);
            }
        }
        public static void PlaceSelectedBlock(GameObject blockToSpawn, Vector3 pos, Transform parent)
        {
            if (blockToSpawn != null)
            {
                /// Ensure only one block is placed on each other block
                if(parent.childCount < 1)
                {
                    Transform b = Instantiate<GameObject>(blockToSpawn, pos, blockToSpawn.transform.rotation, parent).transform;
                    b.transform.position = pos + new Vector3(0f, 2f, 0f);
                    b.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                    spawnList.Add(b.gameObject);
                }
            }
            SpawnPool = spawnList.ToArray();
        }
        public static void PlaceBaseBlock(GameObject baseBlock)
        {
            if (baseBlock != null)
            {
                Transform b = Instantiate<GameObject>(baseBlock, new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f)).transform;
                b.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }
}
