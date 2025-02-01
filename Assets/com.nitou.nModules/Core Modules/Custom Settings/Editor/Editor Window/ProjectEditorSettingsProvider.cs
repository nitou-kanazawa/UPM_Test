#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace Nitou.EditorShared {

    public class ProjectEditorSettingsProvider : SettingsProvider {

        // [NOTE] 設定のパス (※第1階層は「Preferences」にする) 
        private const string SettingPath = SettingsProviderKey.ProjectSettings +"Editor";

        private Editor _editor;


        /// <summary>
        /// このメソッドが重要です
        /// 独自のSettingsProviderを返すことで、設定項目を追加します
        /// </summary>
        [SettingsProvider]
        public static SettingsProvider CreateSettingProvider() {
            // ※第三引数のkeywordsは、検索時にこの設定項目を引っかけるためのキーワード
            return new ProjectEditorSettingsProvider(SettingPath, SettingsScope.Project, null);
        }


        /// ----------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public ProjectEditorSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords) : base(path, scopes, keywords) { }

        /// <summary>
        /// 
        /// </summary>
        public override void OnActivate(string searchContext, VisualElement rootElement) {

            var preferences = ProjectEditorSettingsSO.instance;

            // ※ScriptableSingletonを編集可能にする
            preferences.hideFlags = HideFlags.HideAndDontSave & ~HideFlags.NotEditable;

            // 設定ファイルの標準のインスペクターのエディタを生成
            Editor.CreateCachedEditor(preferences, null, ref _editor);
        }

        /// <summary>
        /// 描画処理．
        /// </summary>
        public override void OnGUI(string searchContext) {

            EditorGUI.BeginChangeCheck();

            // 設定ファイルの標準インスペクタを表示
            _editor.OnInspectorGUI();

            //EditorGUILayout.LabelField("テストだよ");

            if (EditorGUI.EndChangeCheck()) {
                ProjectEditorSettingsSO.instance.Save();
            }
        }
    }
}
#endif