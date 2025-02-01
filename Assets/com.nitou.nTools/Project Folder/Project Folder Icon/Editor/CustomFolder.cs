#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

// [参考]
//  unity doqument: 起動時エディタースクリプト実行 https://docs.unity3d.com/ja/2019.4/Manual/RunningEditorCodeOnLaunch.html

namespace Nitou.Tools.ProjectWindow {

    [InitializeOnLoad]
    internal class CustomFolder{

        /// <summary>
        /// コンストラクタ（静的）
        /// </summary>
        static CustomFolder() {
            //Debug.Log($"CustomFolder constructor");

            IconDictionaryCreator.BuildDictionary();
            EditorApplication.projectWindowItemOnGUI += DrawFolderIcon;
        }
        
        /// <summary>
        /// フォルダアイコンを描画する
        /// </summary>
        private static void DrawFolderIcon(string guid, Rect rect) {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var iconDictionary = IconDictionaryCreator._iconDictionary;
            var fileName = Path.GetFileName(path);

            // 評価
            if (path == "" ||
                Event.current.type != EventType.Repaint ||
                !File.GetAttributes(path).HasFlag(FileAttributes.Directory)) {
                return;
            }

            // Icon画像の取得
            (bool isExist, Texture texture) = IconDictionaryCreator.GetIconTexture(fileName);
            if (!isExist ||texture == null) {
                return;
            }

            // Icon画像の反映
            Rect imageRect;
            if (rect.height > 20) {
                imageRect = new Rect(rect.x - 1, rect.y - 1, rect.width + 2, rect.width + 2);
            } else if (rect.x > 20) {
                imageRect = new Rect(rect.x - 1, rect.y - 1, rect.height + 2, rect.height + 2);
            } else {
                imageRect = new Rect(rect.x + 2, rect.y - 1, rect.height + 2, rect.height + 2);
            }                      
            GUI.DrawTexture(imageRect, texture);
        }
    }
}
#endif
