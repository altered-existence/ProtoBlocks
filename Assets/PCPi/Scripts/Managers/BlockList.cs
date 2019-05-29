/// <summary>
/// Project: ProtoBlock Builder
/// Made for use in Projects: Unicorn Snake & Protoblocks (...more to come?)
/// Filename: BlockList.cs
/// Created by PCPi & AltX
/// Written by: Gary Chisenhall
/// </summary>
#region /// USING
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;
#endregion
namespace PCPi.scripts
{
    /// <summary>
    /// Blocklist Class for ProtoBlock Builder
    /// Creates Lists of ProtoBlock Editor creations
    /// </summary>
    public class BlockList 
    {

        public List<Block> blocks = new List<Block>();
        private Block[] aGameObjects;
        public Block[] AGameObjects { get => aGameObjects; set => aGameObjects = value; }
        /// <summary>
        /// Block Structure
        /// </summary>
        public struct Block
        {
            public GameObject obj;
            public int pegCount;
            public int length;
            public int width;
            public int height;
            public Material material;
            public Material highlightMaterial;
            /// <summary>
            /// Block Constructor
            /// </summary>
            /// <param name="obj"></param>
            /// <param name="pegCount"></param>
            /// <param name="length"></param>
            /// <param name="width"></param>
            /// <param name="height"></param>
            /// <param name="material"></param>
            /// <param name="highlightMaterial"></param>
            public Block(
                GameObject obj,
                int pegCount,
                int length,
                int width,
                int height,
                Material material,
                Material highlightMaterial)
            {
                this.obj = obj;
                this.pegCount = pegCount;
                this.length = length;
                this.width = width;
                this.height = height;
                this.material = material;
                this.highlightMaterial = highlightMaterial;
            }
        }
        /// <summary>
        /// Creates ProtoBlock blueprint
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="pegCount"></param>
        /// <param name="length"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="material"></param>
        /// <param name="highlightMaterial"></param>
        #region /// CreateBlock
        public void CreateBlock(
            GameObject obj,
            int pegCount,
            int length,
            int width,
            int height,
            Material material,
            Material highlightMaterial)
        {
            // Create new block
            Block b = new Block
            {
                obj = obj,
                pegCount = pegCount,
                length = length,
                width = width,
                height = height,
                material = material,
                highlightMaterial = highlightMaterial
            };
            BlockDatabase.AddBlockToDatabase(
                obj,
                length,
                width,
                height,
                material,
                highlightMaterial,
                pegCount);
            // Add new block to List
            AddBlockToList(b);
            AGameObjects = ListBlocks().ToArray();
        }

        

        #endregion
        /// <summary>
        /// Adds blueprint to List
        /// </summary>
        /// <param name="b"></param>
        void AddBlockToList(Block b)
        {
            blocks.Add(b);
        }
        /// <summary>
        /// Returns Block List
        /// </summary>
        /// <returns>blocks</returns>
        public List<Block> ListBlocks()
        {
            return blocks;
        }
        /// <summary>
        /// Clears current List
        /// </summary>
        public void ClearBlocks()
        {
            AGameObjects = null;
            blocks = new List<Block>();
            BlockDatabase.DeleteDatabaseTableContents();
        }
        /// <summary>
        /// Debug log 
        /// Peg Counter
        /// </summary>
        public void PegCounter()
        {
            int count = 0;
            if (AGameObjects != null)
                foreach (Block m in AGameObjects)
                {
                    count += m.pegCount;
                }
            Debug.Log(count);
        }
    }
}