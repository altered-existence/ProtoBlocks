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
        //private InputManager inputManager;
        private List<GameObject> objsList = new List<GameObject>();
        float timerSeconds = 3f;

        private GameObject[] objs = new GameObject[25];

        private void Start()
        {
            //inputManager = gameObject.GetComponent<InputManager>();
        }
        
        public void Update()
        {
            BlockController blockController;
            GameObject obj;
            bool isBase;
            Vector3 mouseScreen = CrossPlatformInputManager.mousePosition;
            mouseScreen.z = 0.5f;
            Vector3 rayOrigin = Camera.main.ScreenToWorldPoint(mouseScreen);
            Vector3 rayDirection = Camera.main.transform.TransformDirection(Vector3.forward);

            Ray ray = new Ray(rayOrigin, rayDirection);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                obj = hit.collider.gameObject;
                objs[0] = obj;
                blockController = obj.GetComponent<BlockController>();
                isBase = blockController.GetBaseValue();
                blockController.BlockToSpawn = BlockManager.GetSelectedBlock();

                if (hit.collider.gameObject != objs[objs.Length - 1])
                {
                    //Debug.DrawRay(rayOrigin, rayDirection, Color.red);
                    blockController.BlockToSpawn = BlockManager.GetSelectedBlock();
                    obj.GetComponent<Renderer>().material = blockController.highlightMaterial;
                    objsList.Add(obj);
                }
                else
                {
                    StartCoroutine(Timer(blockController));
                }
                if (CrossPlatformInputManager.GetButton("Fire1") && hit.collider != null)
                {
                    if (GetIsBuildMode())
                    {
                        BlockSpawnManager.PlaceSelectedBlock(blockController.BlockToSpawn, blockController.transform.position, blockController.transform);
                    }
                    if (GetIsPaintMode())
                    {
                        blockController.PaintedMaterial = PaintManager.GetBlockPaintMaterial();
                        blockController.gameObject.GetComponent<Renderer>().material = blockController.PaintedMaterial;
                        blockController.defaultMaterial = blockController.PaintedMaterial;
                    }
                    if (GetIsEditMode())
                    {
                        if (!isBase)
                        {
                            BlockSpawnManager.BlockDestruct(hit.collider.gameObject);
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
        }

        private void LateUpdate()
        {
            //Vector3 touchPosition;

            //for (int i = 0; i < Input.touchCount; i++)
            //{
            //    touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
            //}
        }
        public IEnumerator Timer(BlockController blockController)
        {
            this.timerSeconds -= Time.deltaTime;

            if (this.timerSeconds > 0)
            {
                SetDefaultMaterial(blockController);
                yield return timerSeconds;
            }
            yield return null;
        }
        private void SetDefaultMaterial(BlockController blockController)
        {
            objs = GetObjs().ToArray();
            //Debug.DrawRay(rayOrigin, rayDirection, Color.yellow);
            for (int i = 0; i > objs.Length - 1; i--)
            {
                objs[i].GetComponent<Renderer>().material = blockController.defaultMaterial;
            }
            objsList.Clear();
        }
        public List<GameObject> GetObjs()
        {
            return objsList;
        }

    }
}