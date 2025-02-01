#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Nitou.Tools.ProjectWindow {
    using Nitou.EditorShared;


    public class FolderCreationWindow : EditorWindow {


        private string _parentFolderName = "_Project";
        private string _parentFolderPath = "Assets";

        private bool _createComposition = true;
        private bool _createEntity = true;
        private bool _createUseCase = true;
        private bool _createPresentation = true;
        private bool _createView = true;
        private bool _createFoundation = true;
        private bool _createLevelObjects = true;
        private bool _createAPIGateway = true;

        [MenuItem("Tools/Project Folder Creator")]
        public static void ShowWindow() {
            GetWindow<FolderCreationWindow>("Project Folder Creator");
        }

        private void OnGUI() {
            GUILayout.Label("Parent Folder Settings", EditorStyles.boldLabel);
            _parentFolderName = EditorGUILayout.TextField("Parent Folder Name", _parentFolderName);
            _parentFolderPath = EditorGUILayout.TextField("Parent Folder Path", _parentFolderPath);

            GUILayout.Space(10);

            GUILayout.Label("Select Folders to Create", EditorStyles.boldLabel);
            _createComposition = EditorGUILayout.Toggle("Composition", _createComposition);
            _createEntity = EditorGUILayout.Toggle("Entity", _createEntity);
            _createUseCase = EditorGUILayout.Toggle("Use Case", _createUseCase);
            _createPresentation = EditorGUILayout.Toggle("Presentation", _createPresentation);
            _createView = EditorGUILayout.Toggle("View", _createView);
            _createFoundation = EditorGUILayout.Toggle("Foundation", _createFoundation);
            _createLevelObjects = EditorGUILayout.Toggle("Level Objects", _createLevelObjects);
            _createAPIGateway = EditorGUILayout.Toggle("API Gateway", _createAPIGateway);

            GUILayout.Space(10);

            if (GUILayout.Button("Create Folders")) {
                CreateFolders();
            }
        }

        private void CreateFolders() {
            string parentFolder = $"{_parentFolderPath}/{_parentFolderName}";

            if (!AssetDatabase.IsValidFolder(parentFolder)) {
                AssetDatabase.CreateFolder(_parentFolderPath, _parentFolderName);
            }

            if (_createComposition) CreateFolder(parentFolder, "_Composition");
            if (_createEntity) CreateFolder(parentFolder, "Entity");
            if (_createUseCase) CreateFolder(parentFolder, "UseCase");
            if (_createPresentation) CreateFolder(parentFolder, "Presentation");
            if (_createView) CreateFolder(parentFolder, "View");
            if (_createFoundation) CreateFolder(parentFolder, "Foundation");
            if (_createLevelObjects) CreateFolder(parentFolder, "LevelObjects");
            if (_createAPIGateway) CreateFolder(parentFolder, "APIGateway");

            AssetDatabase.Refresh();
        }

        private void CreateFolder(string parentFolder, string newFolder) {
            string folderPath = $"{parentFolder}/{newFolder}";
            if (!AssetDatabase.IsValidFolder(folderPath)) {
                AssetDatabase.CreateFolder(parentFolder, newFolder);
                Debug.Log($"Created folder: {folderPath}");
            } else {
                Debug.LogWarning($"Folder already exists: {folderPath}");
            }
        }

    }
}
#endif