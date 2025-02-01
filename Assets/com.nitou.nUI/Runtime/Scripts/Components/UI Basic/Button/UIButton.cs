using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;

namespace Nitou.UI.Components {

    /// <summary>
    /// 基本機能のみの独自ボタンUI（※クリックとサブミットは同一として扱う）
    /// </summary>
    [AddComponentMenu(ComponentMenu.Prefix.UIComponents + "UI Button")]
    public class UIButton : UISelectable, 
        IUISubmitable, IUICancelable,
        ISubmitHandler, ICancelHandler, IPointerClickHandler {

        // Event stream
        protected Subject<Unit> _onSubmitSubject = new();
        protected Subject<Unit> _onCancelSubject = new();

        private float _coolDown = 0f;


        /// ----------------------------------------------------------------------------
        // Properity

        /// <summary>
        /// 決定入力された時のイベント通知
        /// </summary>
        public IObservable<Unit> OnSubmited => _onSubmitSubject;

        /// <summary>
        /// キャンセル入力された時のイベント通知
        /// </summary>
        public IObservable<Unit> OnCanceled => _onCancelSubject;

        /// <summary>
        /// ロック状態かどうか
        /// </summary>
        public bool IsLocked { get; set; } = false;


        /// ----------------------------------------------------------------------------
        // Public MonoBehaviour

        protected override void OnDestroy() {
            base.OnDestroy();
            _onSubmitSubject.Dispose();
            _onCancelSubject.Dispose();
        }


        /// ----------------------------------------------------------------------------
        // Interface Method

        /// <summary>
        /// 決定入力されたときの処理
        /// </summary>
        public virtual void OnSubmit(BaseEventData eventData) {
            Press();

            DoStateTransition(SelectionState.Pressed, false);
            StartCoroutine(OnFinishSubmit());
        }

        /// <summary>
        /// 決定入力されたときの処理
        /// </summary>
        public virtual void OnCancel(BaseEventData eventData) {

            _onCancelSubject.OnNext(Unit.Default);
        }

        /// <summary>
        /// クリックされたときの処理
        /// </summary>
        public virtual void OnPointerClick(PointerEventData eventData) {
            // 左クリックのみ
            if (eventData.button != PointerEventData.InputButton.Left) return;

            Press();
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        protected void Press() {
            if (!IsActive() || !IsInteractable() || IsLocked) return;

            UISystemProfilerApi.AddMarker("Button.onClick", this);
            _onSubmitSubject.OnNext(Unit.Default);
        }

        private IEnumerator OnFinishSubmit() {
            var fadeTime = colors.fadeDuration;
            var elapsedTime = 0f;

            while (elapsedTime < fadeTime) {
                elapsedTime += Time.unscaledDeltaTime;
                yield return null;
            }

            base.DoStateTransition(currentSelectionState, false);
        }


        /// ----------------------------------------------------------------------------
        // Editor
#if UNITY_EDITOR
        protected override void Reset() {
            base.Reset();
        }
#endif
    }
}