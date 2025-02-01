using System;
using UnityEngine;
using Nitou;

namespace UnityScreenNavigator.Runtime.Core.Modal {

    /// <summary>
    /// <see cref="Modal"/>型の基本的な拡張メソッド集．
    /// </summary>
    public static class ModalExtensions{

        /// <summary>
        /// CanvasGroupのinterctable設定を行う拡張メソッド
        /// </summary>
        public static void SetInteractable(this Modal modal, bool value) {
            var canvasGroup = modal.GetOrAddComponent<CanvasGroup>();
            canvasGroup.interactable = value;
        }
        
    }
}
