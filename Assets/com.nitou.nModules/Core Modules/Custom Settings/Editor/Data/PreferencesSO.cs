#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

// [REF]
//  hatena: エディタ拡張で「Manager」的なものに使えるScriptableSingleton https://light11.hatenadiary.com/entry/2021/03/11/201303
//  note: UnityEditor.ScriptableSingleton https://note.com/kazzy_infinity/n/n8a4f836b1c9e
//  UniDoc: ScriptableSingleton<T0> https://docs.unity3d.com/ja/2021.2/ScriptReference/ScriptableSingleton_1.html

namespace Nitou.EditorShared {

    /// <summary>
    /// エディタ用のプリファレンス設定データ．
    /// </summary>
    [FilePath(
        relativePath: "MyPreferences.dat",
        location: FilePathAttribute.Location.PreferencesFolder
    )]
    public class PreferencesSO : ScriptableSingleton<PreferencesSO> {

        public bool flag;
        public Color color;

        public void Save() => Save(saveAsText: true);


        /// ----------------------------------------------------------------------------
        #region Static
        [MenuItem("Develop/Show PreferencesSO")]
        public static void ShowLog() {

        }
        #endregion
    }
}
#endif
