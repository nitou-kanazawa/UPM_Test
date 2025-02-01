#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEditor;

// [参考]
//  Docswell: AssetPostprocessor完全に理解した https://www.docswell.com/s/henjiganai/5714J5-AssetPostprocessor#p8
//  qiita: エディター拡張で、読み込むアセットのパスをハードコードしないために https://qiita.com/tsukimi_neko/items/3d57e3808acb88e11c39
//  　→ （※AssetPostprocessorはUnity.Object？を親に持たないため，シリアライズ対象外みたい）

namespace Nitou.Tools.ProjectWindow {
    using Nitou.Tools.Shared;
    using Nitou.EditorShared;

    /// <summary>
    /// フォルダアイコン画像を管理するDictionayを生成する
    /// </summary>
    internal class IconDictionaryCreator : AssetPostprocessor {

        // リソース情報
        private const string relativeFolderPath = "Project Folder/Project Folder Icon/Icons";
        internal static Dictionary<string, Texture> _iconDictionary;


        /// ----------------------------------------------------------------------------
        // Internal Method

        /// <summary>
        /// Dictionaryの生成
        /// </summary>
        internal static void BuildDictionary() {

            var texs = AssetsLoader.LoadAll<Texture2D>(PackageInfo.PackagePath, relativeFolderPath);
            //var texs = NonResources.LoadAll<Texture2D>(relativeFolderPath, NitouTools.pacakageInfo);
            _iconDictionary = texs.ToDictionary(texture => texture.name, texture => (Texture)texture);
        }

        /// <summary>
        /// 指定したキーに対応するアイコン画像を取得する
        /// </summary>
        internal static (bool isExist, Texture texture) GetIconTexture(string fileNameKey) {

            // ファイル名が完全一致の場合
            if (_iconDictionary.ContainsKey(fileNameKey)) {
                return (true, _iconDictionary[fileNameKey]);
            }

            // 正規表現対応 (※とりあえず決め打ちの実装)
            if (fileNameKey[0] == '_' && fileNameKey.Length > 1) {
                fileNameKey = fileNameKey.Substring(1);
                if (_iconDictionary.ContainsKey(fileNameKey))
                    return (true, _iconDictionary[fileNameKey]);
            }

            return (false, null);
        }
    }
}
#endif