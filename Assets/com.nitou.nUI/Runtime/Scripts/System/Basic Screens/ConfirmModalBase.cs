using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityScreenNavigator.Runtime.Core.Modal;
using Sirenix.OdinInspector;

namespace Nitou.UI.BasicScreens {

    /// <summary>
    /// 確認モーダルの基底クラス．
    /// </summary>
    public abstract class ConfirmModalBase : Modal {

        [TitleGroup("Text")]
        [SerializeField, Indent] protected TextMeshProUGUI _titleText;

        [TitleGroup("Text")]
        [SerializeField, Indent] protected TextMeshProUGUI _messageText;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// 表示テキストを設定する
        /// </summary>
        public void SetText(string title, string message) {
            if (_titleText != null) {
                _titleText.text = title;
            }

            if (_messageText != null) {
                _messageText.text = message;
            }
        }


        /// ----------------------------------------------------------------------------
        // Lifecycle Events

        /// <summary>
        /// 終了処理
        /// </summary>
        public override Task Cleanup() {
            return base.Cleanup();
        }
    }
        

}