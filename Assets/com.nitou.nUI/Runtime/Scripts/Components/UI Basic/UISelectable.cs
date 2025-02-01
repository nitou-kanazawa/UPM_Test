using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;

namespace Nitou.UI.Components {

    /// <summary>
    /// <see cref="Selectable"/>に独自機能を追加した基本コンポーネント
    /// </summary>
    [DisallowMultipleComponent]
    public abstract class UISelectable : Selectable, 
        IUISelectable, IUIMoveable {

        //
        [SerializeField] protected UICursor _cursor;

        // Event stream
        protected readonly Subject<Unit> _onSelectSubject = new();
        protected readonly Subject<Unit> _onDeselectSubject = new();
        protected readonly Subject<MoveDirection> _onMoveSubject = new();

        // Misc
        protected readonly CompositeDisposable _disposables = new ();


        /// ----------------------------------------------------------------------------
        // Properity

        /// <summary>
        /// 選択された時に通知するObservable．
        /// </summary>
        public IObservable<Unit> OnSelected => _onSelectSubject;

        /// <summary>
        /// 非選択された時に通知するObservable．
        /// </summary>
        public IObservable<Unit> OnDeselected => _onDeselectSubject;

        /// <summary>
        /// 移動入力が入った時に通知するObservable．
        /// </summary>
        public IObservable<MoveDirection> OnMoved => _onMoveSubject;


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method

        protected override void Awake() {
            base.Awake();
            SetCursorActivation(false);

            // 
            _disposables.Add(_onSelectSubject);
            _disposables.Add(_onDeselectSubject);
            _disposables.Add(_onMoveSubject);
        }

        protected override void OnDestroy() {
            _disposables.Dispose();
            
            base.OnDestroy();
        }


        /// ----------------------------------------------------------------------------
        // Interface Method

        /// <summary>
        /// 選択されたときの処理
        /// </summary>
        public override void OnSelect(BaseEventData eventData) {
            base.OnSelect(eventData);

            _onSelectSubject.OnNext(Unit.Default);
            SetCursorActivation(true);
        }

        /// <summary>
        /// 選択されたときの処理
        /// </summary>
        public override void OnDeselect(BaseEventData eventData) {
            base.OnDeselect(eventData);

            _onDeselectSubject.OnNext(Unit.Default);
            SetCursorActivation(false);
        }

        /// <summary>
        /// 移動入力されたときの処理
        /// </summary>
        public override void OnMove(AxisEventData eventData) {

            base.OnMove(eventData);
            _onMoveSubject.OnNext(eventData.moveDir);
        }


        /// ----------------------------------------------------------------------------
        // Public Method

        public bool IsSelected() {
            if (EventSystem.current == null) return false;

            return EventSystem.current.currentSelectedGameObject == this.gameObject;
        }

        public override void Select() {
            if (EventSystem.current.currentSelectedGameObject == gameObject) return;

            base.Select();
        }


        /// ----------------------------------------------------------------------------
        // Protected Method

        /// <summary>
        /// 選択カーソルのアクティブ状態を設定する 
        /// </summary>
        protected void SetCursorActivation(bool value) {
            if (_cursor == null) return;

            _cursor.enabled = value;
            _cursor.gameObject.SetActive(value);
        }


        /// ----------------------------------------------------------------------------
        // Editor
#if UNITY_EDITOR
        protected override void Reset() {
            this.transition = Transition.None;
        }
#endif
    }
}
