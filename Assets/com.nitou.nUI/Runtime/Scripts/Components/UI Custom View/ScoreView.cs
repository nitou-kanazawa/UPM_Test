using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace Nitou.UI.Components {

    /// <summary>
    /// スコア表示用のUI
    /// </summary>
    [AddComponentMenu( ComponentMenu.Prefix.UIView + "Score View")]
    public class ScoreView : HideableView {

        [TitleGroup(IKey.COMPONENT)]
        [SerializeField, Indent] TextMeshProUGUI _labelText;

        [TitleGroup(IKey.COMPONENT)]
        [SerializeField, Indent] TextMeshProUGUI _scoreText;

        [TitleGroup(IKey.COMPONENT)]
        [SerializeField, Indent] TextMeshProUGUI _unitText;

        [TitleGroup("Tween Settings")]
        [Range(0,1)]
        [SerializeField, Indent] float _duration = 0.2f;

        private Tweener _scoreTween;
        private int _currentValue;


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method

        protected override void Awake() {
            base.Awake();
            ResetState();
        }

        protected override void OnDestroy() {
            base.OnDestroy();
            _scoreTween?.Kill();
        }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// ラベルを設定する
        /// </summary>
        public void SetLabelText(string str) {
            if (_labelText == null) return;
            _labelText.text = str;
        }

        /// <summary>
        /// 単位を設定する
        /// </summary>
        public void SetUnitText(string str) {
            if (_unitText == null) return;
            _unitText.text = str;
        }

        /// <summary>
        /// スコアを設定する
        /// </summary>
        public void SetScore(int value, bool animated = true) {
            if (_scoreText == null) return;

            // ※実際の値はアニメーション前に更新
            var start = _currentValue;
            _currentValue = value;       

            // 表示処理
            if (animated) {
                _scoreTween?.Kill();
                _scoreTween = _scoreText
                    .DOTextInt(start, value, _duration, x => x.ToString_WithComma())
                    .IgnoreTimeScale().SetLink(gameObject);
            } else {
                _scoreText.text = value.ToString_WithComma();
            }
        }


        /// ----------------------------------------------------------------------------
        // Public Method

        public void ResetState() {
            SetScore(0,false);
        }
    }

}