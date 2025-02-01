#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

// [参考]
//  LIGHT11: エディタでデータを保存する場所と保存方法まとめ

namespace Nitou.ConfigManagement{

    public class ConfigUtilWindow : EditorWindow{

        private string savePath;

        [MenuItem("Tools/Data Save Location")]
        public static void Open() {
            GetWindow<ConfigUtilWindow>("Data Save Location");
        }

        private void OnEnable() {
            // 保存先の初期パスを取得します（例: Application.persistentDataPath）
            savePath = Application.persistentDataPath;
        }

        private void OnGUI() {

            DrawDataPath("persistentDataPath", Application.persistentDataPath);
            DrawDataPath("unityPreferencesFolder", UnityEditorInternal.InternalEditorUtility.unityPreferencesFolder);
        }


        /// <summary>
        /// 
        /// </summary>
        private void DrawDataPath(string label, string dataPath) {
            using var scope = new EditorGUILayout.HorizontalScope();

            // 現在の保存パスを表示
            EditorGUILayout.LabelField($"{label}:", dataPath);

            //EditorGUILayout.Space();

            // フォルダを開くボタン
            if (GUILayout.Button("Open")) {
                EditorUtility.RevealInFinder(savePath);
            }


        }

    }
}
# endif