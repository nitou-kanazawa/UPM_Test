#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;

// [参考]
//  qiita: 毎回使う複数のフォルダをワンクリックで作成する方法 https://qiita.com/OKsaiyowa/items/f7b2d331526e2a6938b1

namespace Nitou.Tools.ProjectWindow {
    using Nitou.EditorShared;

    /// <summary>
    /// 指定フォルダ直下に空フォルダを生成する
    /// </summary>
    public static class BasicFolderCreater {


        /// ----------------------------------------------------------------------------
        // Public Method


        /// <summary>
        /// 
        /// </summary>
        [MenuItem(ToolBarMenu.Prefix.EditorTool + "Project Window/Create AssetData Folders")]
        public static void CreateAssetDataFolders() {

            string parentFolder = "Assets/AssetData";
            if (AssetDatabase.IsValidFolder(parentFolder)) {
                return;
            }

            // アセットデータ用フォルダ
            CreateFolder(parentFolder, "Animations");
            CreateFolder(parentFolder, "Audios");
            CreateFolder(parentFolder, "Textures");
            CreateFolder(parentFolder, "Materials");
            CreateFolder(parentFolder, "Prefabs");
            CreateFolder(parentFolder, "Resources");
            CreateFolder(parentFolder, "Shaders");
        }

        /// <summary>
        /// 
        /// </summary>
        [MenuItem(ToolBarMenu.Prefix.EditorTool + "Project Window/Create Code Folders")]
        public static void CreateProjectCodeFolders() {

            string parentFolder = "Assets/_Project";
            if (AssetDatabase.IsValidFolder(parentFolder)) {
                return;
            }

            // コード用フォルダ
            CreateFolder(parentFolder, "_Composition");
            CreateFolder(parentFolder, "Entity");
            CreateFolder(parentFolder, "UseCase");
            CreateFolder(parentFolder, "Presentation");
            CreateFolder(parentFolder, "View");
            CreateFolder(parentFolder, "Foundation");
            CreateFolder(parentFolder, "LevelObjects");
            CreateFolder(parentFolder, "APIGateway");
        }

        /// <summary>
        /// ビルドデータ用のフォルダを生成する
        /// </summary>
        [MenuItem(ToolBarMenu.Prefix.EditorTool + "Project Window/Create Build Folder")]
        public static void CreateBuildFolder() {
            CreateFolder("Build/");
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        private static void CreateFolder(string parentFolderPath, string folderName = "") {
            string fullPath = $"{parentFolderPath}/{folderName}";

            if (!Directory.Exists(fullPath)) {
                Directory.CreateDirectory(fullPath);
            }

            // ※↓フォルダ更新をUnity側へ反映するため実行
            AssetDatabase.ImportAsset(fullPath);
        }

    }

}
#endif