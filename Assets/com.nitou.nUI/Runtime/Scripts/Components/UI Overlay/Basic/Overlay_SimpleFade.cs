using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace Nitou.UI.Overlay {

    /// <summary>
    /// シンプルなフェードイン・フェードアウトの蓋絵
    /// </summary>
    public class Overlay_SimpleFade : OverlayBase {

        [Title("Tween Parameters")]
        [SerializeField, Indent] Ease _openEasing = Ease.OutQuad;
        [SerializeField, Indent] Ease _closeEasing = Ease.OutQuad;

        private Tween _tween;


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Methord 

        private void OnDestroy() {
            _tween?.Kill();    
        }


        /// ----------------------------------------------------------------------------
        // Public Methord (遷移アニメーション)

        /// <summary>
        /// Progress: 1→0の画面遷移アニメーション
        /// </summary>
        protected override UniTask OpenInternal(float duration) {
            _tween?.Kill();
            _tween = _canvasGroup
                .DOFade(0f, duration).From(1f).SetEase(_openEasing)
                .IgnoreTimeScale();

            return _tween.ToUniTask(cancellationToken: this.GetCancellationTokenOnDestroy());
        }

        /// <summary>
        /// Progress: 0→1の画面遷移アニメーション
        /// </summary>
        protected override UniTask CloseInternal(float duration) {
            _tween?.Kill();
            _tween = _canvasGroup
                .DOFade(1f, duration).From(0f).SetEase(_closeEasing)
                .IgnoreTimeScale();

            return _tween.ToUniTask(cancellationToken: this.GetCancellationTokenOnDestroy());
        }

    }
}
