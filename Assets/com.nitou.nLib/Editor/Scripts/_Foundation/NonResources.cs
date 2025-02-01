#if UNITY_EDITOR
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// [参考]
//  kanのメモ帳: Resources以外からアセットをロードする便利クラス https://kan-kikuchi.hatenablog.com/entry/NonResources

namespace Nitou.EditorShared {

    /// <summary>
    /// <see cref="AssetDatabase"/>のラッパー
    /// </summary>
    public static class NonResources {

        /// ----------------------------------------------------------------------------
        // Method (単体ロード)

        /// <summary>
        /// ファイルのアセットパス(拡張子も含める)と型を設定し、Objectを読み込む．
        /// </summary>
        public static T Load<T>(string path) where T : Object {
            return AssetDatabase.LoadAssetAtPath<T>(path);
        }

        /// <summary>
        /// ファイルのパス(Assetsから、拡張子も含める)と型を設定し、Objectを読み込む．
        /// </summary>
        public static T Load<T>(string assetName, string relativePath) where T : Object {
            return AssetDatabase.LoadAssetAtPath<T>($"{relativePath}/{assetName}");
        }

        public static T Load<T>(string assetName, string relativePath, PackageFolderInfo packageInfo) where T : Object {
            string path = GetAssetPathInPackage(relativePath, assetName, packageInfo);
            return (path != null) ? AssetDatabase.LoadAssetAtPath<T>(path) : null;
        }


        /// ----------------------------------------------------------------------------
        // Method (複数ロード)

        /// <summary>
        /// ディレクトリのパス(Assetsから)と型を設定し、Objectを読み込む。存在しない場合は空のListを返す
        /// </summary>
        public static List<T> LoadAll<T>(string directoryPath) where T : Object {
            var assetList = new List<T>();

            // 指定したディレクトリに入っている全ファイルを取得(子ディレクトリも含む)
            string[] filePathArray = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);

            // 取得したファイルの中からアセットだけリストに追加する
            foreach (string filePath in filePathArray) {
                T asset = Load<T>(filePath);
                if (asset != null) {
                    assetList.Add(asset);
                }
            }

            return assetList;
        }

        /// <summary>
        /// ディレクトリのパス(Assetsから)を設定し、Objectを読み込む。存在しない場合は空のListを返す
        /// </summary>
        public static List<T> LoadAll<T>(string relativePath, PackageFolderInfo packageInfo) where T : Object {
            return LoadAll<T>(GetDirectoryPathInPackage(relativePath, packageInfo));
        }



        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// 
        /// </summary>
        private static string GetAssetPathInPackage(string relativePath, string assetName, PackageFolderInfo packageInfo) {

            var fullRelativePath = $"{relativePath}/{assetName}";

            // 配布先でのパス
            var upmPath = $"Packages/{packageInfo.upmFolderName}/{fullRelativePath}";
            // 開発プロジェクト内でのパス
            var normalPath = $"Assets/{packageInfo.normalFolderName}/{fullRelativePath}";

            // 
            if (File.Exists(Path.GetFullPath(upmPath))) {
                return upmPath;
            }
            // 
            else if (File.Exists(Path.GetFullPath(normalPath))) {
                return normalPath;
            }
            // 
            else {
                Debug.LogError($"File not found in both UPM and normal paths: \n" +
                    $"  [{upmPath}] and \n" +
                    $"  [{normalPath}]");
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static string GetDirectoryPathInPackage(string relativePath, PackageFolderInfo packageInfo) {
            // 配布先でのパス
            var upmPath = $"Packages/{packageInfo.upmFolderName}/{relativePath}";
            // 開発プロジェクト内でのパス
            var normalPath = $"Assets/{packageInfo.normalFolderName}/{relativePath}";

            // 
            if (Directory.Exists(Path.GetFullPath(upmPath))) {
                return upmPath;
            }
            // 
            else if (Directory.Exists(Path.GetFullPath(normalPath))) {
                return normalPath;
            }
            // 
            else {
                Debug.LogError($"Directory not found in both UPM and normal paths: \n" +
                    $"  [{upmPath}] and \n" +
                    $"  [{normalPath}]");
                return null;
            }
        }

    }


    public struct PackageFolderInfo {
        
        public readonly string upmFolderName;
        public readonly string normalFolderName;

        public PackageFolderInfo(string upmFolderName = "com.nitou.nLib", string normalFolderName = "nLib") {
            this.upmFolderName = upmFolderName;
            this.normalFolderName = normalFolderName;
        }
    }
}

#endif