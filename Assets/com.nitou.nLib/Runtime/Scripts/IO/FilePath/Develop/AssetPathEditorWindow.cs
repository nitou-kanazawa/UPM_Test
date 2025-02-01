#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Nitou.EditorScripts{
    using Nitou.EditorShared;

    internal sealed class AssetPathEditorWindow : EditorWindow , IDevelopEditorWindow{
        
        // 対象
        [SerializeField] private AssetPath _assetPath;
        [SerializeField] private PackageDirectoryPath _packageDirectoryPath;
        private string _relativePath;

        private SerializedObject _serializedObject;
        private SerializedProperty _relativePathProperty;


        /// ----------------------------------------------------------------------------
        // Edtor Method

        [MenuItem(ToolBarMenu.Prefix.Develop + "File Path/AssetPath Window")]
        public static void Open() {
            GetWindow<AssetPathEditorWindow>("AssetPath");
        }

        private void OnEnable() {
            _assetPath = AssetPath.Empty();
            _packageDirectoryPath = new PackageDirectoryPath("com.nitou.nLib", "com.nitou.nLib");

            _serializedObject = new SerializedObject(this);
            _relativePathProperty = _serializedObject.FindProperty("_assetPath").FindPropertyRelative("_relativePath");

        }

        private void OnGUI() {
            GUILayout.Label("AssetPath Inspector", EditorStyles.boldLabel);

            DrawAssetPathProperities();

            DrawPackagePathProperities();


            GUILayout.Label("Icon Button Example", EditorStyles.boldLabel);

            // アイコンを取得
            GUIContent iconContent = EditorGUIUtility.IconContent("d_UnityEditor.SceneView");

            // アイコン付きのボタンを表示
            if (GUILayout.Button(iconContent, GUILayout.Width(40), GUILayout.Height(40))) {
                Debug.Log("Icon Button Clicked");
            }
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        private void DrawAssetPathProperities() {
            using var scope = new EditorGUILayout.VerticalScope(GUI.skin.box);

            // 相対パスの編集フィールド
            EditorGUILayout.PropertyField(_relativePathProperty, new GUIContent("Relative Path"));
            _serializedObject.ApplyModifiedProperties();

            // プロパティの変化を表示
            if (_assetPath != null) {
                EditorGUILayout.LabelField("ToRelativePath", _assetPath.ToRelativePath());
                EditorGUILayout.LabelField("ToProjectPath", _assetPath.ToProjectPath());
                EditorGUILayout.LabelField("ToAbsolutePath", _assetPath.ToAbsolutePath());
                EditorGUILayout.LabelField("Exists", _assetPath.Exists().ToString());
                EditorGUILayout.LabelField("IsFile", _assetPath.IsFile().ToString());
                EditorGUILayout.LabelField("IsDirectory", _assetPath.IsDirectory().ToString());
            }
        }

        private void DrawPackagePathProperities() {
            using var scope = new EditorGUILayout.VerticalScope(GUI.skin.box);

            // 相対パスの編集フィールド
            _relativePath = EditorGUILayout.TextField(new GUIContent("Relative Path"), _relativePath);



            // プロパティの変化を表示
            if (_assetPath != null) {
                EditorGUILayout.LabelField("ToProjectPath", _packageDirectoryPath.ToProjectPath());
                EditorGUILayout.LabelField("ToAbsolutePath", _packageDirectoryPath.ToAbsolutePath());
            }
        }
    }
}
#endif