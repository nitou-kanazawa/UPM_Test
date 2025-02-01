using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace Nitou.UI.Components {

    [DisallowMultipleComponent]
    public class UICursorAnimation_PopStyle : UICursorAnimationBase {

        [Title("Tween params")]
        [SerializeField, Indent] float _min = 0.9f;
        [SerializeField, Indent] float _max = 1.1f;

        [Range(0.1f, 1f)]
        [SerializeField, Indent] float _pitch = 0.8f;
        [SerializeField, Indent] Ease _easing = Ease.InCubic;

        private Vector3 _startScale;
        private Vector3 _endScale;

        private Tween _enableTween;


        /// ----------------------------------------------------------------------------
        // Protected Method

        protected override void InitializeInternal() {

            _startScale = transform.localScale * _min;
            _endScale = transform.localScale* _max;

            // カーソルがアクティブなら即再生
            if (_cursor.isActiveAndEnabled) {
                OnEnableAnimation();
            }
        }

        protected override void OnEnableAnimation() {
            _enableTween?.Kill();
            _enableTween = transform.DOScale(_endScale, _pitch)
                .SetLoops(-1, LoopType.Restart)
                //.SetEase(_easing)
                // 
                .IgnoreTimeScale().SetLink(gameObject);
        }

        protected override void OnDisableAnimation() {
            _enableTween?.Kill();
            transform.localScale = _startScale;
        }

        protected override void DisposeInternal() {
            _enableTween?.Kill();
        }
    }
}
