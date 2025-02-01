using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Nitou {

    /// <summary>
    /// <see cref="ScriptableObject"/>型を対象とした汎用メソッド集．
    /// </summary>
    public static class ScriptableObjectUtils {

#if UNITY_EDITOR

        /// <summary>
        /// Assetsフォルダ内のScriptableObjectを検索する
        /// </summary>
        public static T FindScriptableObject<T>() where T : ScriptableObject {

            // 対象のファイル情報
            var guid = AssetDatabase.FindAssets("t:" + typeof(T).Name).FirstOrDefault();
            var filePath = AssetDatabase.GUIDToAssetPath(guid);

            if (string.IsNullOrEmpty(filePath)) {
                throw new System.IO.FileNotFoundException(typeof(T).Name + " does not found");

                // Log 出して return null
                //Debug_.LogWarning("Oh...");
                //return null;
            }

            var asset = AssetDatabase.LoadAssetAtPath<T>(filePath);
            return asset;
        }
#endif

    }

}
