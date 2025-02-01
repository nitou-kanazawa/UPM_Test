using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace Nitou.UI.Components {

    [DisallowMultipleComponent]
    public class UIButtonAnimation_PopStyle : UIButtonAnimationBase {

        [Title("Main images")]
        [SerializeField, Indent] Image _backdropImage;
        [SerializeField, Indent] Image _highlightImage;

        [Title("Tween params")]
        [SerializeField, Indent] Color _normalColor = Colors.WhiteSmoke;
        [SerializeField, Indent] Color _rollOverColor = Colors.Gray;
        [Range(0.1f, 1f)]
        [SerializeField, Indent] float _duration = 0.2f;
        [SerializeField, Indent] Ease _easing = Ease.OutCubic;

        private Tween _selectTween;
        private Tween _clickTween;


        /// ----------------------------------------------------------------------------
        // Protected Method

        protected override void OnClickAnimation() {
            _clickTween?.Kill();
            _clickTween = DOTween.Sequence();
        }

        protected override void OnDeselectAnimation() {
            _selectTween?.Kill();
            _selectTween = DOTween.Sequence()
                // Fade Out
                .Join(_highlightImage.DOFadeOut(_duration))
                .Join(transform.DOScale(1, _duration))
                // UI共通
                .IgnoreTimeScale().SetLink(gameObject);
        }

        protected override void OnSelectAnimation() {
            _selectTween?.Kill();
            _selectTween = DOTween.Sequence()
                // Fade In
                .Join(_highlightImage.DOFadeIn(_duration))
                .Join(transform.DOScale(1.1f, _duration))
                // UI共通
                .IgnoreTimeScale().SetLink(gameObject);
        }

        protected override void DisposeInternal() {
            _selectTween?.Kill();
            _clickTween?.Kill();
        }


        /// ----------------------------------------------------------------------------
        // Private Method
    }
}
