#if UNITY_EDITOR
using System.IO;
using UnityEngine;
using UnityEditor;

// [参考]
//  LIGHT11: スクリプトからスクリプトファイル(.cs)を生成する https://light11.hatenadiary.com/entry/2018/03/22/191516

namespace Nitou.Tools.CodeGeneration{

    /// <summary>
    /// コード生成関連のメソッドを提供するクラス
    /// </summary>
    public static class CodeGenerator{


        // 定数
        private const string CLASS_KEY = "#CLASSNAME#";
        private const string NAMESPACE_KEY = "#CLASSNAME#";
        private const string FILE_PATH_KEY = "#NAMESPACE#";

        /// <summary>
        /// コードを生成し、指定されたディレクトリにファイルとして保存します。
        /// </summary>
        public static void GenerateClass(string templatePath, string outputDirectory, CodeInfo codeInfo) {
            var templateCode = CodeTemplate.FromFile(templatePath);
            if (templateCode == null) return;

            // テンプレート内の置換
            var code = templateCode
                .Replace(CLASS_KEY, codeInfo.className)
                .Replace(NAMESPACE_KEY, codeInfo.namespaceName);
                //.Replace(FILE_PATH_KEY, filePath);

            // 出力ディレクトリが存在しない場合は作成
            if (!Directory.Exists(outputDirectory)) {
                Directory.CreateDirectory(outputDirectory);
            }

            // ファイルのパスを決定して書き込み
            string fileName = Path.Combine(outputDirectory, $"{codeInfo.className}.cs");
            File.WriteAllText(fileName, code);

            // AssetDatabaseをリフレッシュしてUnityにファイルを認識させる
            AssetDatabase.Refresh();

            Debug.Log($"クラス生成完了: {fileName}");
        }
    }
}
#endif