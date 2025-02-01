using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace Nitou.UI.Components {

    public class UIButtonAnimation_NierStyle : UIButtonAnimationBase {

        [Title("Main images")]
        [SerializeField] Image _backdropImage;
        [SerializeField] Image _fillImage;
        [SerializeField] Image _outlineImage;

        [Title("Inner contents")]
        [SerializeField] Image _iconImage;
        [SerializeField] TextMeshProUGUI _text;

        [Title("Tween params")]
        //[OdinIgnore]
        [SerializeField] Color _normalColor = Colors.WhiteSmoke;
        //[OdinIgnore]
        [SerializeField] Color _rollOverColor = Colors.Gray;
        [Range(0.1f, 1f)]
        [SerializeField] float _duration = 0.2f;
        [SerializeField] Ease _easing = Ease.OutCubic;

        private float OutlineDration => _duration * 0.7f;

        private Tween _selectTween;
        private Tween _clickTween;

        private RectTransform _rect;


        /// ----------------------------------------------------------------------------
        // Protected Method

        protected override void InitializeInternal() {
            _rect = transform as RectTransform;
            ApplySettings();
        }

        protected override void OnClickAnimation() {
            _clickTween?.Kill();
            _clickTween = DOTween.Sequence()

                .Join(_rect.DOSizeDeltaWithCurrentAnchers(new Vector2(600, 60), _duration * 1.5f))
                .Join(_outlineImage.rectTransform.DOScaleX(0, _duration * 1.5f))
                // UI共通
                .IgnoreTimeScale()
                .SetLink(gameObject);

        }

        protected override void OnDeselectAnimation() {
            _selectTween?.Kill();
            _selectTween = DOTween.Sequence()
                // Fill 
                .Join(_fillImage.DOFillAmount(0f, _duration).SetEase(_easing))

                // Outline
                .Join(_outlineImage.DOFade(0f, OutlineDration).SetEase(_easing))
                .Join(_outlineImage.transform.DOScale(new Vector3(1, 0.8f, 1), OutlineDration).SetEase(_easing))

                // Roll over
                .Join(_iconImage.DOColor(_rollOverColor, _duration).SetEase(_easing))
                .Join(_text.DOColor(_rollOverColor, _duration).SetEase(_easing))

                // UI共通
                .IgnoreTimeScale()
                .SetLink(gameObject);


            _clickTween?.Kill();
            _clickTween = DOTween.Sequence()
                .Join(_rect.DOSizeDeltaWithCurrentAnchers(new Vector2(450, 60), _duration))
                // UI共通
                .IgnoreTimeScale()
                .SetLink(gameObject);

        }

        protected override void OnSelectAnimation() {
            _selectTween?.Kill();
            _selectTween = DOTween.Sequence()
                // Fill 
                .Join(_fillImage.DOFillAmount(1f, _duration).SetEase(_easing))

                // Outline
                .Join(_outlineImage.DOFade(1f, OutlineDration).SetEase(_easing))
                .Join(_outlineImage.transform.DOScaleY(1f, OutlineDration).SetEase(_easing))

                // Roll over
                .Join(_iconImage.DOColor(_normalColor, _duration).SetEase(_easing))
                .Join(_text.DOColor(_normalColor, _duration).SetEase(_easing))

                // UI共通
                .IgnoreTimeScale()
                .SetLink(gameObject);
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        private void ApplySettings() {

            // 
            _rect.SetPivotX(0f);    // ※クリック時に右に伸ばすので，pivotXを左に設定

            // 当たり判定
            _backdropImage.raycastTarget = true;

            // 
            _fillImage.raycastTarget = false;
            _fillImage.SetHorizontalFillMode(Image.OriginHorizontal.Left);
            _fillImage.fillAmount = 0f;

            // 
            _outlineImage.raycastTarget = false;
            _outlineImage.rectTransform.SetPivotX(1f);      // ※クリック時に右に縮めるので，pivotXを右に設定
            _outlineImage.SetAlpha(0f);

            // 
            _iconImage.raycastTarget = false;
            _iconImage.color = _rollOverColor;

            // 
            _text.raycastTarget = false;
            _text.color = _rollOverColor;
        }
    }
}
