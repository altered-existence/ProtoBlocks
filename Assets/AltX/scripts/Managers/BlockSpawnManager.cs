﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AltX.Manager
{
    public class BlockSpawnManager : MonoBehaviour
    {
        public void BlockDestruct(GameObject gameObject)
        {
                Destroy(gameObject);
        }
        public static void PlaceSelectedBlock(GameObject blockToSpawn, Vector3 pos, Transform parent)
        {
            if (blockToSpawn != null)
            {
                Transform b = Instantiate<GameObject>(blockToSpawn, pos, new Quaternion(0f, 0f, 0f, 0f), parent).transform;
                b.localScale = new Vector3(1f, 1f, 1f);
            }
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
