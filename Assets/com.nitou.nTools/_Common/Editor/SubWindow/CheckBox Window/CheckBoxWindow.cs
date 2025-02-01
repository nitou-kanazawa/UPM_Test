#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nitou.Tools.Shared {

    /// <summary>
    /// チェックボックスウインドウを呼び出すための静的クラス
    /// </summary>
    public static class CheckBoxWindow {

        /// <summary>
        /// ウインドウを開く
        /// </summary>
        public static void Open(
            string title,
            IReadOnlyList<ICheckBoxWindowData> dataList,
            Action<IReadOnlyList<ICheckBoxWindowData>> onOk
        ) {
            // ウインドウの実体
            // var window = EditorWindow.GetWindow<CheckBoxWindowInstance>();
            var window = ScriptableObject.CreateInstance<CheckBoxWindowInstance>();
            window.ShowAuxWindow();     // ※補助ウインドウとして表示
            window.Open(title, dataList, onOk);
        }
    }
}
#endif