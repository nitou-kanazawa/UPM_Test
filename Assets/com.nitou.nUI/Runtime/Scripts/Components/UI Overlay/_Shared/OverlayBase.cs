using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Nitou.UI.Overlay {

    /// <summary>
    /// オーバレイの基底クラス
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class OverlayBase : MonoBehaviour, IUIOverlay {

        public enum State {
            Open,
            Transition,
            Close,
        }

        protected CanvasGroup _canvasGroup;

        /// <summary>
        /// 遷移状態．
        /// </summary>
        public State OverlayState { get; private set; } = State.Transition;

        /// <summary>
        /// 蓋絵が閉じた状態かどうか
        /// </summary>
        public bool IsClose => OverlayState is State.Close;

        /// <summary>
        /// 蓋絵が閉じた状態かどうか
        /// </summary>
        public bool IsOpen => OverlayState is State.Open;


        /// ----------------------------------------------------------------------------
        // Lifecycle Events

        protected async UniTaskVoid Start() {
            _canvasGroup = gameObject.GetComponent<CanvasGroup>();

            Initialize();

            // 初期状態を非同期で設定
            await OpenInternal(0f);
            OverlayState = State.Open;
        }


        protected virtual void Initialize() {}


        /// ----------------------------------------------------------------------------
        // Public Methord

        /// <summary>
        /// Progress: 1→0の画面遷移アニメーション
        /// </summary>
        public async UniTask OpenAsync(float duration = 1f) {
            // ※完全に閉じた状況でなければ終了
            if (!IsClose) return;

            OverlayState = State.Transition;
            await OpenInternal(duration);
            OverlayState = State.Open;
        }

        /// <summary>
        /// Progress: 0→1の画面遷移アニメーション
        /// </summary>
        public async UniTask CloseAsync(float duration = 1f) {
            // ※完全に開いた状況でなければ終了
            if (!IsOpen) return;

            OverlayState = State.Transition;
            await CloseInternal(duration);
            OverlayState = State.Close;
        }


        /// ----------------------------------------------------------------------------
        // Public Methord (遷移アニメーション)

        protected abstract UniTask OpenInternal(float duration);

        protected abstract UniTask CloseInternal(float duration);
    }

}