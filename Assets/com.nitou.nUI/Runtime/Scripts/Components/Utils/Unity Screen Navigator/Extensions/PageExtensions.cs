using UnityEngine;
using Nitou;

namespace UnityScreenNavigator.Runtime.Core.Page{

    /// <summary>
    /// <see cref="Page"/>型の基本的な拡張メソッド集
    /// </summary>
    public static class PageExtensions {

        /// <summary>
        /// CanvasGroupのinterctable設定を行う拡張メソッド
        /// </summary>
        public static void SetInteractable(this Page page, bool value) {
            var canvasGroup = page.GetOrAddComponent<CanvasGroup>();
            canvasGroup.interactable = value;
        }

    }
}
