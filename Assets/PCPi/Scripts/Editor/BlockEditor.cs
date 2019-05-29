/// <summary>
/// Project: ProtoBlock Builder
/// Made for use in Projects: Unicorn Snake & Protoblocks (...more to come?)
/// Filename: BlockEditor.cs
/// Created by PCPi & AltX
/// Written by: Gary Chisenhall
/// </summary>
/// 
#region /// USING
using UnityEngine;
using UnityEditor;
using PCPi.scripts.Managers;
#endregion
namespace PCPi.scripts.Editor
{
    /// <summary>
    /// BlockEditor
    /// ProtoBlock Builder
    /// </summary>
    public class BlockEditor : EditorWindow
    {
        #region /// Constants
        private const string TITLE = "ProtoBlock Builder";
        private const string DEFAULT_PATH = "Assets/PCPi";
        private const string DEFAULT_PREFAB_PATH = "/Prefabs/";
        private const string DEFAULT_PREFAB_FILENAME = "Square_ProtoBlock.prefab";
        private const string DEFAULT_MATERIALS_PATH = "/Materials/";
        private const string DEFAULT_MATERIAL_FILENAME = "Red.mat";
        #endregion

        #region /// Local Variables
        private string[] startButtonText = { "Create List", "Create Block" };
        private string[] tag = { "Block", "Base" };
        private GameObject manager;
        private GameObject go;
        private BlockList blockList;
        private Object obj;
        private Object defaultObject;
        private Material defaultMaterial;
        private Material material;
        private Material highlightMaterial;
        private Vector3 scaler;
        private int i = 0;
        private int pegCount;
        private int length;
        private int width;
        private int height;
        private float scale = 1;
        private float offsetFactor;
        private bool debugger = false;
        private bool isHighlightable = false;
        private bool isBase = false;
        #endregion
        private void Awake()
        {
            defaultObject = (GameObject)Resources.Load(DEFAULT_PATH + DEFAULT_PREFAB_PATH + DEFAULT_PREFAB_FILENAME);
            defaultMaterial = (Material)Resources.Load(DEFAULT_PATH + DEFAULT_MATERIALS_PATH + DEFAULT_MATERIAL_FILENAME);
        }
        private void Update()
        {
                #region /// Height Lock
                /// Comment line to use height adjustment
                height = 1;
                /// Use at own risk!...
                #endregion
                #region /// Peg calculator
                pegCount = length * width;
                #endregion
                #region /// Scaler
                scaler = new Vector3(scale, scale, scale);
                #endregion
                AssignMaterials();
        }
        
        [MenuItem("Tools/PCPi/ProtoBlock Builder")]
        private static void ShowWindow()
        {
            GetWindow<BlockEditor>("Block Editor");
        }
        void OnGUI()
        {
            GetBuilderGUI();
        }
        #region /// Build GUI Window
        /// <summary>
        /// Begin GUI
        /// </summary>
        private void GetBuilderGUI()
        {
            SetGUIColorScheme();

            GUILayout.Label(TITLE, EditorStyles.largeLabel);

            PegHelperText();

            DisplayBlockProperties();
            
            if (GUILayout.Button(startButtonText[i], GUILayout.MaxWidth(100f)))
            {
                i = 1;
                if (blockList != null)
                {
                    if (obj != null && pegCount > 0)
                        blockList.CreateBlock(
                            (GameObject)obj,
                            pegCount,
                            length,
                            width,
                            length,
                            material,
                            highlightMaterial);
                }
                else
                {
                    blockList = new BlockList();
                }
            }
            if (blockList != null)
            {
                if (blockList.AGameObjects != null)
                {
                    scale = EditorGUILayout.FloatField("Spawn Scale", scale);
                    if (GUILayout.Button("Build Block"))
                    {
                        BuildProto();

                    }
                    if (GUILayout.Button("Prefab It!"))
                    {
                        PrefabCreatedBlock();
                    }
                    DebugBuilder();
                }
            }
            GUILayout.Label("Debug", EditorStyles.miniBoldLabel);
            if (GUILayout.Toggle(debugger, GUIContent.none))
            {
                debugger = true;
            }
            else
            {
                debugger = false;
            }
        }
        #endregion
        /// <summary>
        /// Displays ProtoBlock Propeties for editing
        /// </summary>
        private void DisplayBlockProperties()
        {
            if(i == 1)
            {
                length = EditorGUILayout.IntField("Length of Block", length);
                width = EditorGUILayout.IntField("Width of Block", width);
                height = EditorGUILayout.IntField("Height of Block", height);
                offsetFactor = EditorGUILayout.FloatField("Amount to offset Block", offsetFactor);
                using (new HorizontalBlock())
                {
                    GUILayout.Label("ProtoBlock", EditorStyles.centeredGreyMiniLabel, GUILayout.Width(170f));
                    obj = EditorGUILayout.ObjectField(obj, typeof(GameObject), true);
                }
                using (new HorizontalBlock())
                {
                    GUILayout.Label("Main Material", EditorStyles.centeredGreyMiniLabel, GUILayout.Width(170f));
                    material = EditorGUILayout.ObjectField(material, typeof(Material), true) as Material;
                }

                GUILayout.Label("Highlighter?", EditorStyles.miniBoldLabel, GUILayout.Width(170f));
                if (GUILayout.Toggle(isHighlightable, GUIContent.none))
                {
                    isHighlightable = true;
                    GetHighlighterMaterial();
                }
                else
                {
                    highlightMaterial = material;
                    isHighlightable = false;
                }
                using (new HorizontalBlock())
                {
                    GUILayout.Label("Make it a BASE Block?", EditorStyles.miniBoldLabel, GUILayout.Width(170f));
                    if (GUILayout.Toggle(isBase, GUIContent.none))
                    {
                        isBase = true;
                    }
                    else
                    {
                        isBase = false;
                    }
                }
            }
        }
        /// <summary>
        /// Set color theme
        /// </summary>
        private static void SetGUIColorScheme()
        {
            GUI.backgroundColor = Color.Lerp(Color.red, Color.LerpUnclamped(Color.black, Color.cyan, 10f), 1f);
            GUI.contentColor = Color.red;
        }
        /// <summary>
        /// Create helper text header
        /// </summary>
        private void PegHelperText()
        {
            GUILayout.TextArea("The current ProtoBlock created will have " + pegCount.ToString() + " peg" + ((pegCount < 1 || pegCount > 1) ? "s" : ""), EditorStyles.helpBox);
        }
        /// <summary>
        /// Assign materials to current _protoblock to be created
        /// </summary>
        private void AssignMaterials()
        {
            go = obj as GameObject;

            if (material)
            {
                go.GetComponent<Renderer>().material = material;
                obj = go as Object;
                AssignHighlighterMaterial();
            }
        }
        /// <summary>
        /// Create highlight material option GUI
        /// </summary>
        private void GetHighlighterMaterial()
        {
            using (new HorizontalBlock())
            {
                GUILayout.Label("Selected Material", EditorStyles.centeredGreyMiniLabel, GUILayout.Width(170f));
                highlightMaterial = EditorGUILayout.ObjectField(highlightMaterial, typeof(Material), true) as Material;
            }
        }
        /// <summary>
        /// Assign highlight material to current _protoblock to be created
        /// </summary>
        private void AssignHighlighterMaterial()
        {
            if (go.GetComponent<BlockSettings>())
            {
                go.GetComponent<BlockSettings>().highlightMaterial = highlightMaterial;
            }
        }
        /// <summary>
        /// Builds currently created _protoblock in scene
        /// </summary>
        private void BuildProto()
        {
            if (blockList != null)
            {
                if (obj != null && pegCount > 0 || blockList.AGameObjects != null)
                    if (blockList.AGameObjects.Length >= 0)
                        BlockBuilder.BuildProtoBlock(
                            blockList.AGameObjects[blockList.AGameObjects.Length - 1],
                            length,
                            width,
                            height,
                            material,
                            highlightMaterial,
                            scaler,
                            offsetFactor,
                            isBase);
            }
        }
        /// <summary>
        /// Prefabs last ProtoBlock created in scene view 
        /// </summary>
        private static void PrefabCreatedBlock()
        {
            if(BlockBuilder.newPrefab)
                BlockBuilder.CreatePrefab(BlockBuilder.newPrefab);
        }
        /// <summary>
        /// Debug used for dev of new features coming...
        /// </summary>
        private void DebugBuilder()
        {
            if (debugger)
            {
                GUILayout.Label("Quantity Of Blocks In List", EditorStyles.miniBoldLabel);
                //GUILayout.TextArea(blockList.ListBlocks().Count.ToString(), EditorStyles.helpBox);
                if (GUILayout.Button("Peg Count"))
                {
                    if (blockList != null)
                    {
                        blockList.PegCounter();
                    }
                }
                if (GUILayout.Button("Clear Block List"))
                {
                    if (blockList != null)
                    {
                        i = 0;
                        blockList.ClearBlocks();
                        UnityEngine.Debug.Log("All Blocks Cleared");
                    }
                }
            }
        }

    }
}