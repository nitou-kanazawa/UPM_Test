using System;
using System.Linq;

// [参考]
//  qiita: AssemblyDefinition.asmdefをスクリプトから作成する https://qiita.com/im0039kp/items/b6ebc1e07b04e5dd9dc1

namespace Nitou.Tools.Assets {

    /// <summary>
    /// Assembly Definition（.asmdef）のJSONを表すクラス
    /// </summary>
    [Serializable]
    public sealed class JsonAssemblyDefinition {
        public string name = string.Empty;
        public string rootNamespace = string.Empty;
        public string[] references = Array.Empty<string>();
        public string[] includePlatforms = Array.Empty<string>();
        public string[] excludePlatforms = Array.Empty<string>();
        public bool allowUnsafeCode;
        public bool overrideReferences;
        public string[] precompiledReferences = Array.Empty<string>();
        public bool autoReferenced;
        public string[] defineConstraints = Array.Empty<string>();
        public string[] versionDefines = Array.Empty<string>();
        public bool noEngineReferences;

        /// <summary>
        /// useGUIDsが有効かどうかを判定するメソッド
        /// </summary>
        public bool IsUseGUIDsEnabled() {
            // すべての参照が "GUID:" で始まっているかをチェック
            return references.All(x => x.StartsWith("GUID:"));
        }


        /// <summary>
        /// Editor用データを生成する
        /// </summary>
        public static JsonAssemblyDefinition Editor(string name) {
            return new JsonAssemblyDefinition() {
                name = name,
                includePlatforms = new[] { "Editor", },
            };
        }

        /// <summary>
        /// Editor用データを生成する
        /// </summary>
        public static JsonAssemblyDefinition NitouEditor(string name) {
            return new JsonAssemblyDefinition() {
                name = name,
                references = new[] {"nitou.Runtime", "nitou.Editor", },
                includePlatforms = new[] { "Editor", },
            };
        }
    }
}