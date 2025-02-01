#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Nitou.Tools.PumlGenerator {
    using Nitou.Tools.Shared;

    public static class PlantUmlGeneratorMenu {
        
        private const string MenuPath = "Assets/Create/PlantUML/";

        // リソース情報
        private const string RelativeFolderPath = "Assets/Plant UML/Editor/Templates";


        [MenuItem(MenuPath + "Class Diagram", priority = -100)]
        private static void GenerateClassDiagram() {
            GenerateDiagramFromTemplate("Template_ClassDiagram.puml");
        }

        [MenuItem(MenuPath + "Sequence Diagram", priority = -100)]
        private static void GenerateSequenceDiagram() {
            GenerateDiagramFromTemplate("Template_SequenceDiagram.puml");
        }


        private static void GenerateDiagramFromTemplate(string templateName) {
            string templatePath = GetTemplatePath(templateName);
            string outputPath = GetSelectedFolderPath(templateName);

            if (string.IsNullOrEmpty(outputPath)) {
                Debug.LogError("No valid folder selected.");
                return;
            }

            PlantUmlGenerator.GenerateDiagram(templatePath, outputPath);
        }

        /// <summary>
        /// 
        /// </summary>
        private static string GetTemplatePath(string templateName) {


            // テンプレートのパスを返す。ユーザーが独自のテンプレートを追加できるようにディレクトリを設定
            return Path.Combine(PackageInfo.PackagePath.ToProjectPath(), RelativeFolderPath, templateName);
        }

        /// <summary>
        /// 
        /// </summary>
        private static string GetSelectedFolderPath(string fileName) {
            // 選択中のフォルダのパスを取得
            string folderPath = AssetDatabase.GetAssetPath(Selection.activeObject);

            if (string.IsNullOrEmpty(folderPath) || !AssetDatabase.IsValidFolder(folderPath)) {
                folderPath = "Assets"; // デフォルトのフォルダ
            }

            return Path.Combine(folderPath, fileName);
        }
    }
}

#endif