using UnityEngine;
using Nitou;

namespace UnityScreenNavigator.Runtime.Core.Sheet {

    /// <summary>
    /// <see cref="Sheet"/>型の基本的な拡張メソッド集．
    /// </summary>
    public static class SheetExtensions {

        /// <summary>
        /// CanvasGroupのinterctable設定を行う拡張メソッド
        /// </summary>
        public static void SetInteractable(this Sheet sheet, bool value) {
            var canvasGroup = sheet.GetOrAddComponent<CanvasGroup>();
            canvasGroup.interactable = value;
        }

    }
}