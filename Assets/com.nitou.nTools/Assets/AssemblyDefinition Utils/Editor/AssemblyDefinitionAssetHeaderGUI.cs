#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Nitou.Tools.Assets {
    using Nitou.Tools.Shared;

    /// <summary>
    /// Assembly Definition (.asmdef)のインスペクターに操作GUIを追加するクラス
    /// </summary>
    [InitializeOnLoad]
    internal static class AssemblyDefinitionAssetHeaderGUI {

        /// <summary>
        /// チェックウインドウへの表示用データ
        /// </summary>
        private sealed class Data : ICheckBoxWindowData {

            public string GUID { get; }
            public AssemblyDefinitionAsset AssemblyDefinitionAsset { get; }
            public string Name { get; }
            public bool IsValid { get; }
            public bool IsChecked { get; set; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public Data(string guid, string[] referenceGUIDArray) {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);

                GUID = guid;
                AssemblyDefinitionAsset = AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>(assetPath);
                IsValid = AssemblyDefinitionAsset != null;
                Name = IsValid ? AssemblyDefinitionAsset.name : string.Empty;
                IsChecked = referenceGUIDArray.Contains(guid);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        static AssemblyDefinitionAssetHeaderGUI() {
            Editor.finishedDefaultHeaderGUI -= OnGUI;
            Editor.finishedDefaultHeaderGUI += OnGUI;
        }

        /// <summary>
        /// チェックボックスで参照アセンブリファイルを複数選択できるウインドウを表示
        /// </summary>
        /// <param name="editor"></param>
        private static void OnGUI(Editor editor) {
            var assemblyDefinitionImporter = editor.target as AssemblyDefinitionImporter;
            if (assemblyDefinitionImporter == null) return;

            var assetPath = assemblyDefinitionImporter.assetPath;
            var json = File.ReadAllText(assetPath);
            var jsonAssemblyDefinition = JsonUtility.FromJson<JsonAssemblyDefinition>(json);


            // useGUIDs が無効かどうかをチェック
            bool useGUIDsDisabled = !jsonAssemblyDefinition.IsUseGUIDsEnabled();

            // GUIの有効/無効を設定
            GUI.enabled = !useGUIDsDisabled;


            // 操作ボタン
            if (GUILayout.Button("Select References")) {

                var referenceGUIDArray = jsonAssemblyDefinition.references
                    .Select(x => x.Replace("GUID:", ""))
                    .ToArray();

                // Project内の全asmdefファイル
                var dataArray = AssetDatabase
                        .FindAssets($"t:{nameof(AssemblyDefinitionAsset)}")
                        .Select(x => new Data(x, referenceGUIDArray))
                        .Where(x => x.IsValid)
                        .OrderBy(x => x.Name)
                        .ToArray();

                // 
                CheckBoxWindow.Open(
                    title: "Select References",
                    dataList: dataArray,
                    onOk: OnOk
                );

                // コールバック （※asmdefファイルの参照情報を更新）
                void OnOk(IReadOnlyList<ICheckBoxWindowData> _) {
                    var oldReferences = jsonAssemblyDefinition.references.ToArray();

                    var newReferences = dataArray
                            .Where(x => x.IsChecked)
                            .Select(x => $"GUID:{x.GUID}")
                            .ToArray()
                        ;

                    if (oldReferences.SequenceEqual(newReferences)) return;

                    jsonAssemblyDefinition.references = newReferences;

                    var newJson = JsonUtility.ToJson(jsonAssemblyDefinition, true);

                    File.WriteAllText(assetPath, newJson, Encoding.UTF8);
                    AssetDatabase.Refresh();
                }
            }

        }

    }
}
#endif