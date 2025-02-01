#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

// [REF]
//  qiita: Unityで独自の設定のUIを提供できるSettingsProviderの紹介と設定ファイルの保存について https://qiita.com/sune2/items/a88cdee6e9a86652137c

namespace Nitou.EditorShared {

    public class PreferencesProvider : SettingsProvider{

        // 設定のパス (※第1階層は「Preferences」にする)
        private const string SettingPath = SettingsProviderKey.Preference + "My Preferences";

        private Editor _editor;


        /// <summary>
        /// このメソッドが重要です
        /// 独自のSettingsProviderを返すことで、設定項目を追加します
        /// </summary>
        [SettingsProvider]
        public static SettingsProvider CreateSettingProvider() {
            return new PreferencesProvider(SettingPath, SettingsScope.User, null);
        }


        /// ----------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public PreferencesProvider(string path, SettingsScope scopes, IEnumerable<string> keywords) : base(path, scopes, keywords) {}

        /// <summary>
        /// 
        /// </summary>
        public override void OnActivate(string searchContext, VisualElement rootElement) {

            var preferences = PreferencesSO.instance;
            
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
                PreferencesSO.instance.Save();
            }
        }
    }
}
#endif