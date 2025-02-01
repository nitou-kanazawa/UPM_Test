using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// [参考]
//  qiita: C#(Unity)でのファイルパスの取得 https://qiita.com/oishihiroaki/items/1a082f3bb32f2e3d88a0
//  はなちる: 絶対パスをAssets/~に変換する https://www.hanachiru-blog.com/entry/2018/10/12/204022
//  _ : フルパスをAssetsパスに変換する方法 https://mizutanikirin.net/unity-assetspath

namespace Nitou {

    /// <summary>
    /// パス取得に関する汎用メソッド集
    /// </summary>
    public static class PathUtils {

        /// --------------------------------------------------------------------
        // 取得

        /// <summary>
        /// ファイルパスからファイル名を取得する
        /// </summary>
        public static string GetFileName(string filePath) =>
            System.IO.Path.GetFileName(filePath);

        /// <summary>
        /// ファイルパスから拡張子を取得する
        /// </summary>
        public static string GetExtension(string filePath) =>
            System.IO.Path.GetExtension(filePath);

        /// <summary>
        /// 指定したフォルダ内の全ファイルパスを取得する
        /// </summary>
        public static string[] GetFilesInFolder(string folderPath) {
            return System.IO.Directory.GetFiles(folderPath, "*");
        }


        /// --------------------------------------------------------------------
        #region パスの変換（string拡張メソッド）

        /// <summary>
        /// フルパスをアセット以下パス(Assets/..)に変換する
        /// </summary>
        public static string ToAssetsPath(this string fullPath) {
            // "Assets/"位置を取得
            int startIndex = fullPath.IndexOf("Assets/", System.StringComparison.Ordinal);
            if (startIndex == -1) {
                startIndex = fullPath.IndexOf("Assets\\", System.StringComparison.Ordinal);
            }

            // ※含まれない場合は，空文字を返す
            if (startIndex == -1) return "";

            // 加工後パスを返す
            string assetPath = fullPath.Substring(startIndex);
            return assetPath;
        }

        /// <summary>
        /// 文字列の配列を 1 つのパスに結合します
        /// </summary>
        public static string Combine(params string[] paths) {
            return paths
                .Aggregate((a, b) => Path.Combine(a, b))
                .Replace("\\", "/");
        }
        #endregion


        /// --------------------------------------------------------------------
        // Private Method

        private static string GetParentDirectory(string filepath, int n = 1) {
            string dir = filepath;
            for (int i = 0; i < n; i++) {
                dir = System.IO.Directory.GetParent(dir).FullName;
            }
            return dir.ToAssetsPath();
        }


        /// --------------------------------------------------------------------
#if UNITY_EDITOR

        /// <summary>
        /// 選択中のアセットのパスを取得する
        /// </summary>
        public static string GetSelectedAssetPath() =>
            AssetDatabase.GetAssetPath(Selection.activeInstanceID);


        /// --------------------------------------------------------------------
        #region パスの変換（string拡張メソッド）

        /// <summary>
        /// アセットパスを取得する
        /// </summary>
        public static string GetAssetPath(this ScriptableObject scriptableObject) {
            var mono = MonoScript.FromScriptableObject(scriptableObject);
            return AssetDatabase.GetAssetPath(mono).Replace("\\", "/");
        }

        /// <summary>
        /// アセットの親フォルダパスを取得する
        /// </summary>
        public static string GetAssetParentFolderPath(this ScriptableObject scriptableObject, int n = 1) {
            var filePath = scriptableObject.GetAssetPath();

            return GetParentDirectory(filePath, n);
        }
        #endregion


        /// --------------------------------------------------------------------
        // 

        /// <summary>
        /// フォルダのアセットパスを検索して取得する
        /// </summary>
        public static string GetFolderPath(string folderName, string parentFolderName) {

            // ※全ファイルを検索する実装なのに注意
            string[] guids = AssetDatabase.FindAssets(folderName);
            foreach (var guid in guids) {

                // 対象フォルダ情報
                var folderPath = AssetDatabase.GUIDToAssetPath(guid);

                // 親フォルダ情報
                var parentFolderPath = System.IO.Path.GetDirectoryName(folderPath);
                var parentFolder = System.IO.Path.GetFileName(parentFolderPath);

                // 親フォルダまで一致しているなら，確定とする
                if (parentFolder == parentFolderName) {
                    return folderPath;
                }
            }

            return "";
        }

#endif

    }
}