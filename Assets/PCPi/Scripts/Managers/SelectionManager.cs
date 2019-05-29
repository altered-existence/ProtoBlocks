/// <summary>
/// Project: ProtoBlock Builder
/// Made for use in Projects: Unicorn Snake & Protoblocks (...more to come?)
/// Filename: SelectionManager.cs
/// Created by PCPi & AltX
/// Written by: Gary Chisenhall
/// </summary>
#region /// USING
using System;
using UnityEngine;
using AltX.Managers;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using System.Collections.Generic;
#endregion
namespace PCPi.scripts.Managers
{
    public class SelectionManager : GameManager
    {
        private List<GameObject> objsList = new List<GameObject>();
        float timerSeconds = 3f;

        private GameObject[] objs = new GameObject[50];

        public List<GameObject> ObjsList { get => objsList; set => objsList = value; }

        public void Update()
        {
            InitiateSelection();
        }

        private void InitiateSelection()
        {
            BlockController blockController;
            GameObject obj;
            bool isBase;
            Vector3 mouseScreen = CrossPlatformInputManager.mousePosition;
            Vector3 rayOrigin = Camera.main.ScreenToWorldPoint(mouseScreen);
            Vector3 rayDirection = Camera.main.transform.TransformDirection(Vector3.forward);

            Ray ray = new Ray(rayOrigin, rayDirection);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                obj = hit.collider.gameObject;
                blockController = obj.GetComponent<BlockController>();
                isBase = blockController.GetBaseValue();

                if (hit.collider.tag == "target")
                {
                    blockController.BlockToSpawn = BlockManager.GetBaseBlock();
                }
                else
                {
                    blockController.BlockToSpawn = BlockManager.GetSelectedBlock();
                }
                if (hit.collider.gameObject)
                {
                    Debug.DrawRay(rayOrigin, rayDirection, Color.red);
                    obj.GetComponent<Renderer>().material = blockController.highlightMaterial;
                    ObjsList.Add(obj);
                }
                if (CrossPlatformInputManager.GetButton("Fire1") && hit.collider != null)
                {
                    PerformModeAction(blockController, isBase, hit);
                }
            }
        }

        private static void PerformModeAction(BlockController blockController, bool isBase, RaycastHit hit)
        {
            if (GetIsBuildMode())
            {
                PlaceBlock(blockController);
            }
            if (GetIsPaintMode())
            {
                PaintActiveBlock(blockController);
            }
            if (GetIsEditMode())
            {
                DestroyActiveBlock(hit, isBase);
            }
        }

        private static void DestroyActiveBlock(RaycastHit hit, bool isBase)
        {
            if (!isBase)
            {
                if (hit.collider.tag != "target")
                {
                    BlockSpawnManager.BlockDestruct(hit.collider.gameObject);
                }
            }
        }

        private static void PlaceBlock(BlockController blockController)
        {
            BlockSpawnManager.PlaceSelectedBlock(blockController.BlockToSpawn, blockController.transform.position, blockController.transform);
        }

        private static void PaintActiveBlock(BlockController blockController)
        {
            blockController.PaintedMaterial = PaintManager.GetBlockPaintMaterial();
            blockController.gameObject.GetComponent<Renderer>().material = blockController.PaintedMaterial;
            blockController.defaultMaterial = blockController.PaintedMaterial;
        }

        private void LateUpdate()
        {
            StartCoroutine(Timer());
        }
        public IEnumerator Timer()
        {
            this.timerSeconds -= Time.deltaTime;

            if (this.timerSeconds > 0)
            {
                yield return timerSeconds;
            }
            else
            {
                SetDefaultMaterial();
                timerSeconds = 1f;
                yield return null;
            }
        }
        private void SetDefaultMaterial()
        {
            BlockController blockController;
            /// Ensure all highlighted objects in list are in array for default setting to happen
            objs = GetObjs().ToArray();
            /// Iterate through all highlighted objects
            for (int i = 0; i < objs.Length - 1; i++)
            {
                if (objs[i] != null)
                {
                    blockController = objs[i].GetComponent<BlockController>();
                    objs[i].GetComponent<Renderer>().material = blockController.defaultMaterial;
                }
            }
            /// Clear list to reset action
            ObjsList.Clear();
        }
        public List<GameObject> GetObjs()
        {
            return ObjsList;
        }

    }
}