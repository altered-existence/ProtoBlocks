using UnityEngine;
using ProtoAccessLibrary;
using PCPi.scripts.Managers;
using System.IO;

namespace AltX.Managers
{
    public class GameManager : MonoBehaviour
    {
        public string persistentDataPath { get; set; }
        public string streamingAssetsPath { get; set; }
        public bool databaseExists { get; set; }
        public string databaseConnectionString { get; set; }
        public string databaseFilePath { get; set; }
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
            persistentDataPath = Application.persistentDataPath;
            streamingAssetsPath = Application.streamingAssetsPath;
            databaseFilePath = persistentDataPath;
            databaseConnectionString = "Data Source=" + databaseFilePath + "ProtoDB.db;Version=3;";
            isBuildMode = true;
            isPaintMode = false;
            isEditMode = false;
            OutputInfo();
        }
        public void OutputInfo()
        {
            Debug.Log("Persistant Data: " + persistentDataPath);
            Debug.Log("Streaming Assets: " + streamingAssetsPath);
        }
        public void CheckForDBFile()
        {
            if (!File.Exists(databaseFilePath + "ProtoDB.db"))
            {
                databaseExists = false;
                Debug.Log("!!! Database File NOT Found !!!");
            }
            else
            {
                databaseExists = true;
                Debug.Log("Database File Found!");
            }
        }

    }
}

