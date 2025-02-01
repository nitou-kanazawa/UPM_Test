#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Nitou.EditorShared {
    using Nitou.Shared;

    /// <summary>
    /// <see cref="Resources"/>ライクに非Resourcesフォルダのアセットを読み込むためのクラス（※AssetDatabaseのラッパー）
    /// </summary>
    public class AssetsLoader {

        /// ----------------------------------------------------------------------------
        #region Public Method (単体ロード)

        /// <summary>
        /// ファイルのアセットパス(拡張子も含める)と型を設定し、Objectを読み込む．
        /// </summary>
        public static T Load<T>(AssetPath assetPath)
            where T : Object {
            return AssetDatabase.LoadAssetAtPath<T>(assetPath.ToString());
        }

        /// <summary>
        /// ファイルのアセットパス(拡張子も含める)と型を設定し、Objectを読み込む．
        /// </summary>
        public static Object Load(AssetPath assetPath) {
            return Load<Object>(assetPath);
        }

        /// <summary>
        /// ファイルのアセットパス(拡張子も含める)と型を設定し、Objectを読み込む．
        /// </summary>
        public static T Load<T>(PackageDirectoryPath packagePath, string relativePath, string fileName)
            where T : Object {
            var path = PathUtils.Combine(packagePath.ToProjectPath(), relativePath, fileName);
            return AssetDatabase.LoadAssetAtPath<T>(path);
        }

        #endregion


        /// ----------------------------------------------------------------------------
        #region Public Method (複数ロード)

        /// <summary>
        /// ディレクトリのパス(Assetsから)と型を設定し、Objectを読み込む．存在しない場合は空のListを返す．
        /// </summary>
        public static List<T> LoadAll<T>(AssetPath directoryPath) where T : Object {

            if (!directoryPath.IsDirectory()) {
                Debug_.LogWarning($"The specified directory ({directoryPath}) does not exist.");
                return new List<T>();
            }

            return LoadAll_Internal<T>(directoryPath.ToProjectPath());
        }

        /// <summary>
        /// ディレクトリのパス(Assetsから)と型を設定し、Objectを読み込む．存在しない場合は空のListを返す．
        /// </summary>
        public static List<Object> LoadAll(AssetPath directoryPath) {
            return LoadAll<Object>(directoryPath);
        }

        public static List<T> LoadAll<T>(PackageDirectoryPath packagePath, string relativePath) where T : Object {

            var path = PathUtils.Combine(packagePath.ToProjectPath(), relativePath);
            return LoadAll_Internal<T>( path);
        }

        #endregion


        /// ----------------------------------------------------------------------------
        #region Private Method

        public static T Load<T>(string assetPath) where T : Object {
            return AssetDatabase.LoadAssetAtPath<T>(assetPath.ToString());
        }

        private static List<T> LoadAll_Internal<T>(string directoryPath) where T : Object {

            // 指定したディレクトリに入っている全ファイルを取得(子ディレクトリも含む)
            var filePaths = Directory.EnumerateFiles(directoryPath, "*", SearchOption.AllDirectories);

            // 取得したファイルの中からアセットだけリストに追加する
            var assetList = new List<T>();
            foreach (string filePath in filePaths) {
                T asset = AssetDatabase.LoadAssetAtPath<T>(filePath);
                assetList.AddIfNotNull(asset);
            }
            return assetList;
        }

        #endregion
    }
}
#endif