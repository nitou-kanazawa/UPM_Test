using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Nitou.Tools{
    using Nitou.EditorShared;
    using System.IO;

    public class DataClassGeneratorWindow : EditorWindow{

        private string _directoryPath = "";
        private string _resultText = "";

        [MenuItem(ToolBarMenu.Prefix.Develop + "Test Window")]
        public static void Open() {
            GetWindow<DataClassGeneratorWindow>();
        }


        public void OnGUI() {
            GUILayout.Label("Asset Loader", EditorStyles.boldLabel);

            _directoryPath = EditorGUILayout.TextField(_directoryPath);

            if (GUILayout.Button("Get Path")) {

                // ƒpƒX
                //var path = AssetPath.FromRelativePath(_directoryPath);
                //Debug_.Log(path.ToProjectPath());
                //Debug_.Log(path.ToAbsolutePath());
                //AssetsLoader.LoadAll<MonoScript>(path);

                var packageDirectoryPath = new PackageDirectoryPath("com.nitou.nLib", "com.nitou.nLib");
                var results = AssetsLoader.LoadAll<MonoScript>(packageDirectoryPath, "Editor");

                Debug_.ListLog(results.Select(x =>x.name).ToList());

            }

            GUILayout.Label("Result:", EditorStyles.boldLabel);
            GUILayout.TextArea(_resultText, GUILayout.Height(100));
        }
    }
}
