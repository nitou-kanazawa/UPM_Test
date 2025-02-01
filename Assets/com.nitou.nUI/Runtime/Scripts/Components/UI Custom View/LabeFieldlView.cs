using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

namespace Nitou.UI.Component{

    /// <summary>
    /// シンプルなラベルと本文を持つView
    /// </summary>
    [DisallowMultipleComponent]
    public class LabeFieldlView : MonoBehaviour{

        [SerializeField, Indent] protected TextMeshProUGUI _labelText;
        [SerializeField, Indent] protected TextMeshProUGUI _valueText;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// ラベルを設定する
        /// </summary>
        public void SetLabel(string label) {
            if (label == null) return;
            _labelText.text = label;
        }

        /// <summary>
        /// 本文を設定する
        /// </summary>
        public void SetValue(string value) {
            if (value == null) return;
            _valueText.text = value;
        }

        public void SetDefault() {
            SetLabel("label");
            SetValue("value");
        }
    }
}
