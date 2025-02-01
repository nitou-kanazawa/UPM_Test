#if UNITY_EDITOR
using UnityEngine;

namespace Nitou.Tools.ProjectWindow{
    using Nitou.EditorShared;
    using System.IO;
    using UnityEditor;

    public static class ProjectFolderCreater    {

        public static void CreateFolders(string _parentFolderPath, string _parentFolderName, IFlagContainer flags) {
            
            string parentFolder = $"{_parentFolderPath}/{_parentFolderName}";

            if (!AssetDatabase.IsValidFolder(parentFolder)) {
                AssetDatabase.CreateFolder(_parentFolderPath, _parentFolderName);
            }

            // フラグに基づいてサブフォルダを作成
            foreach (var flagName in flags.GetFlagNames()) {
                if (flags.GetFlagValue(flagName)) {
                    CreateFolder(parentFolder, flagName);
                }
            }

            AssetDatabase.Refresh();
        }


        /// <summary>
        /// 
        /// </summary>
        public static void CreateFolder(string parentFolder, string newFolder) {
            string folderPath = $"{parentFolder}/{newFolder}";
            if (!AssetDatabase.IsValidFolder(folderPath)) {
                AssetDatabase.CreateFolder(parentFolder, newFolder);
                Debug.Log($"Created folder: {GetFullPath(folderPath)}");
            } else {
                Debug.LogWarning($"Folder already exists: {GetFullPath(folderPath)}");
            }
        }

        /// <summary>
        /// フォルダのフルパスを取得
        /// </summary>
        public static string GetFullPath(string assetPath) {
            return Path.Combine(Directory.GetParent(Application.dataPath).FullName, assetPath);
        }
    }
}
#endif