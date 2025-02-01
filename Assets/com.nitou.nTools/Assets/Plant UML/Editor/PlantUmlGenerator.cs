#if UNITY_EDITOR
using System.IO;
using UnityEngine;
using UnityEditor;

namespace Nitou.Tools.PumlGenerator {

    public static class PlantUmlGenerator {
        
        /// <summary>
        /// Pumlファイルを生成する．
        /// </summary>
        public static void GenerateDiagram(string templatePath, string outputPath) {
            if (!File.Exists(templatePath)) {
                Debug.LogError($"Template not found at: {templatePath}");
                return;
            }

            string templateContent = File.ReadAllText(templatePath);

            // 生成ロジック：テンプレートの内容をもとにPlantUMLのコードを生成
            // ここで、必要に応じてテンプレートのプレースホルダーを置換してカスタマイズ
            string generatedContent = templateContent; // プレースホルダーを実際の内容に置換する処理を追加

            File.WriteAllText(outputPath, generatedContent);
            AssetDatabase.Refresh(); // アセットデータベースを更新して新しいファイルを表示

            Debug.Log($"PlantUML diagram generated at: {outputPath}");
        }

    }
}

#endif