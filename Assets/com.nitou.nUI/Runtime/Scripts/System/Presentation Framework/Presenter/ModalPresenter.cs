using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityScreenNavigator.Runtime.Core.Modal;

namespace Nitou.UI.PresentationFramework {

    public abstract class ModalPresenter<TModal, TRootView, TRootViewState> : ModalPresenter<TModal>, IDisposableCollectionHolder
        where TModal : Modal<TRootView, TRootViewState>
        where TRootView : AppView<TRootViewState>
        where TRootViewState : AppViewState, new() {

        private readonly List<IDisposable> _disposables = new ();
        private TRootViewState _state;


        /// ----------------------------------------------------------------------------
        // 

        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected ModalPresenter(TModal view) : base(view) { }

        ICollection<IDisposable> IDisposableCollectionHolder.GetDisposableCollection() {
            return _disposables;
        }

        protected sealed override void Initialize(TModal view) {
            base.Initialize(view);
        }

        protected sealed override void Dispose(TModal view) {
            base.Dispose(view);
            _disposables.ForEach(d => d.Dispose());
        }


        /// ----------------------------------------------------------------------------
        // IModalLifecycleEvent Method

        protected sealed override async Task ViewDidLoad(TModal view) {
            await base.ViewDidLoad(view);
            var state = new TRootViewState();
            _state = state;
            _disposables.Add(state);
            view.Setup(state);
            await ViewDidLoad(view, _state);
        }

        protected sealed override async Task ViewWillPushEnter(TModal view) {
            await base.ViewWillPushEnter(view);
            await ViewWillPushEnter(view, _state);
        }

        protected sealed override void ViewDidPushEnter(TModal view) {
            base.ViewDidPushEnter(view);
            ViewDidPushEnter(view, _state);
        }

        protected sealed override async Task ViewWillPushExit(TModal view) {
            await base.ViewWillPushExit(view);
            await ViewWillPushExit(view, _state);
        }

        protected sealed override void ViewDidPushExit(TModal view) {
            base.ViewDidPushExit(view);
            ViewDidPushExit(view, _state);
        }

        protected sealed override async Task ViewWillPopEnter(TModal view) {
            await base.ViewWillPopEnter(view);
            await ViewWillPopEnter(view, _state);
        }

        protected sealed override void ViewDidPopEnter(TModal view) {
            base.ViewDidPopEnter(view);
            ViewDidPopEnter(view, _state);
        }

        protected sealed override async Task ViewWillPopExit(TModal view) {
            await base.ViewWillPopExit(view);
            await ViewWillPopExit(view, _state);
        }

        protected sealed override void ViewDidPopExit(TModal view) {
            base.ViewDidPopExit(view);
            ViewDidPopExit(view, _state);
        }

        protected override async Task ViewWillDestroy(TModal view) {
            await base.ViewWillDestroy(view);
            await ViewWillDestroy(view, _state);
        }


        /// ----------------------------------------------------------------------------
        // IModalLifecycleEvent Method (※派生クラスでの再定義用)

        /// <summary>
        /// モーダルがロードされた直後に呼ばれる処理
        /// </summary>
        protected virtual Task ViewDidLoad(TModal view, TRootViewState viewState) {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Push遷移によりモーダルが表示される直前に呼ばれる処理
        /// </summary>
        protected virtual Task ViewWillPushEnter(TModal view, TRootViewState viewState) {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Push遷移によりモーダルが表示された直後に呼ばれる処理
        /// </summary>
        protected virtual void ViewDidPushEnter(TModal view, TRootViewState viewState) {
        }

        /// <summary>
        /// Push遷移によりモーダルが非表示になる直前に呼ばれる処理
        /// </summary>
        protected virtual Task ViewWillPushExit(TModal view, TRootViewState viewState) {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Push遷移によりモーダルが非表示になった直後に呼ばれる処理
        /// </summary>
        protected virtual void ViewDidPushExit(TModal view, TRootViewState viewState) {
        }

        /// <summary>
        /// Pop遷移によりモーダルが表示される直前に呼ばれる処理
        /// </summary>
        protected virtual Task ViewWillPopEnter(TModal view, TRootViewState viewState) {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Pop遷移によりモーダルが表示された直後に呼ばれる処理
        /// </summary>
        protected virtual void ViewDidPopEnter(TModal view, TRootViewState viewState) {
        }

        /// <summary>
        /// Pop遷移によりモーダルが非表示になる直前に呼ばれる処理
        /// </summary>
        protected virtual Task ViewWillPopExit(TModal view, TRootViewState viewState) {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Pop遷移によりモーダルが非表示になった直後に呼ばれる処理
        /// </summary>
        protected virtual void ViewDidPopExit(TModal view, TRootViewState viewState) {
        }

        /// <summary>
        /// モーダルがリリースされる直前に呼ばれる処理
        /// </summary>
        protected virtual Task ViewWillDestroy(TModal view, TRootViewState viewState) {
            return Task.CompletedTask;
        }
    }
}
