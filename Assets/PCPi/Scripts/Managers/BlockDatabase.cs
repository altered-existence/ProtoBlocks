
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;

namespace PCPi.scripts
{
    public class BlockDatabase
    {
        #region ///Database Functions
        /// <summary>
        /// Database Connection String
        /// </summary>
        private static readonly string conn = "URI=file:" + Application.dataPath + "/PCPi/blocks_db.sqlite";

        /// <summary>
        /// Debug log of (TBlocks) table contents
        /// </summary>
        public static void DebugBlockList()
        {
            string sqlQuery = "SELECT * FROM TBlocks";

            RunDatabaseCommand(sqlQuery);
        }
        /// <summary>
        /// Alters table by adding a new column if it doesn't exist
        /// i.e. AddColumnToTable("TBlocks", "index", "TEXT", "");
        /// </summary>
        /// <param name="tblName"></param>
        /// <param name="columnToAdd"></param>
        /// <param name="type"></param>
        /// <param name="defaultValue"></param>
        public static void AddColumnToTable(string tblName, string columnToAdd, string type, string defaultValue)
        {
            string cmdAlterTable = "ALTER TABLE " + tblName + " ADD " + columnToAdd +" " + type + " DEFAULT " + defaultValue + " NOT NULL";

            RunDatabaseCommand(cmdAlterTable);
        }
        /// <summary>
        /// Creates table if it doesn't exist
        /// </summary>
        public static void CreateBlockTable()
        {
            string cmdCreateTable = "CREATE TABLE [IF NOT EXIST] [TBlocks]("
                                  + "[index] INTEGER NOT NULL PRIMARY KEY,"
                                  + "[strName] TEXT DEFAULT 'name' NOT NULL,"
                                  + "[intLength] INTEGER DEFAULT '1' NOT NULL,"
                                  + "[intWidth] INTEGER DEFAULT '1' NOT NULL,"
                                  + "[intHeight] INTEGER DEFAULT '1' NOT NULL,"
                                  + "[strMaterial] TEXT DEFAULT 'default.mat' NOT NULL,"
                                  + "[strHighlightedMaterial] TEXT DEFAULT 'default.mat' NOT NULL"
                                  + "[intPegCount] INTEGER DEFAULT '1' NOT NULL,";
            RunDatabaseCommand(cmdCreateTable);
        }
        /// <summary>
        /// Adds block data to database table (TBlocks)
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="length"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="material"></param>
        /// <param name="highlightMaterial"></param>
        public static void AddBlockToDatabase(
            GameObject obj,
            int length,
            int width,
            int height,
            Material material,
            Material highlightMaterial,
            int pegCount)
        {
            string cmdAdd = "INSERT INTO TBlocks(strName, intLength, intWidth, intHeight, strMaterial, strHighlightedMaterial, intPegCount) VALUES('"
                + obj.name
                + "',"
                + length
                + ","
                + width
                + ","
                + height
                + ",'"
                + material.name
                + "','"
                + highlightMaterial.name
                + "',"
                + pegCount
                + ")";

            RunDatabaseCommand(cmdAdd);
        }
        /// <summary>
        /// Retrieves table row where the index = param
        /// </summary>
        /// <param name="index"></param>
        public static void GetBlock(int index)
        {
            string sqlQuery = "select * from TBlocks where [index = " + index + "]";

            RunDatabaseCommand(sqlQuery);
        }
        /// <summary>
        /// Delete entire database table of blocks
        /// </summary>
        public static void DeleteDatabaseTableContents()
        {
            string cmdDelete = "DELETE FROM TBlocks";

            RunDatabaseCommand(cmdDelete);
        }
        /// <summary>
        /// Run Query in database
        /// </summary>
        /// <param name="command"></param>
        public static void RunDatabaseCommand(string command)
        {
            if (string.IsNullOrWhiteSpace(command))
            {
                throw new ArgumentException("message", nameof(command));
            }

            using (IDbConnection dbConn = new SqliteConnection(conn))
            {
                dbConn.Open();

                using (IDbCommand dbCmd = dbConn.CreateCommand())
                {
                    dbCmd.CommandText = command;

                    using (IDataReader reader = dbCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Debug.Log(reader.GetString(1));
                        }
                    }
                }
            }
        }
        #endregion
    }
}