using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityScreenNavigator.Runtime.Core.Sheet;

namespace Nitou.UI.PresentationFramework {

    public abstract class SheetPresenter<TSheet, TRootView, TRootViewState> : SheetPresenter<TSheet>, IDisposableCollectionHolder
        where TSheet : Sheet<TRootView, TRootViewState>
        where TRootView : AppView<TRootViewState>
        where TRootViewState : AppViewState, new() {

        private readonly List<IDisposable> _disposables = new List<IDisposable>();
        private TRootViewState _state;


        /// ----------------------------------------------------------------------------
        // 

        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected SheetPresenter(TSheet view) : base(view) { }

        ICollection<IDisposable> IDisposableCollectionHolder.GetDisposableCollection() {
            return _disposables;
        }

        protected sealed override void Initialize(TSheet view) {
            base.Initialize(view);
        }

        protected sealed override void Dispose(TSheet view) {
            base.Dispose(view);
            _disposables.ForEach(d => d.Dispose());
        }


        /// ----------------------------------------------------------------------------
        // LifecycleEvent

        protected sealed override async Task ViewDidLoad(TSheet view) {
            await base.ViewDidLoad(view);
            var state = new TRootViewState();
            _state = state;
            _disposables.Add(state);
            view.Setup(state);
            await ViewDidLoad(view, _state);
        }

        protected sealed override async Task ViewWillEnter(TSheet view) {
            await base.ViewWillEnter(view);
            await ViewWillEnter(view, _state);
        }

        protected sealed override void ViewDidEnter(TSheet view) {
            base.ViewDidEnter(view);
            ViewDidEnter(view, _state);
        }

        protected sealed override async Task ViewWillExit(TSheet view) {
            await base.ViewWillExit(view);
            await ViewWillExit(view, _state);
        }

        protected sealed override void ViewDidExit(TSheet view) {
            base.ViewDidExit(view);
            ViewDidExit(view, _state);
        }

        protected override async Task ViewWillDestroy(TSheet view) {
            await base.ViewWillDestroy(view);
            await ViewWillDestroy(view, _state);
        }


        /// ----------------------------------------------------------------------------
        // LifecycleEvent (※)

        /// <summary>
        /// このシートがロードされた直後に呼ばれる
        /// </summary>
        protected virtual Task ViewDidLoad(TSheet view, TRootViewState viewState) {
            return Task.CompletedTask;
        }

        /// <summary>
        /// このシートが表示される直前に呼ばれる
        /// </summary>
        protected virtual Task ViewWillEnter(TSheet view, TRootViewState viewState) {
            return Task.CompletedTask;
        }

        /// <summary>
        /// このシートが表示された直後に呼ばれる
        /// </summary>
        protected virtual void ViewDidEnter(TSheet view, TRootViewState viewState) {
        }

        /// <summary>
        /// このシートが非表示される直前に呼ばれる
        /// </summary>
        protected virtual Task ViewWillExit(TSheet view, TRootViewState viewState) {
            return Task.CompletedTask;
        }

        /// <summary>
        /// このシートが非表示された直後に呼ばれる
        /// </summary>
        protected virtual void ViewDidExit(TSheet view, TRootViewState viewState) {
        }

        /// <summary>
        /// このシートがリリースされる直前に呼ばれる
        /// （※親ページがpop遷移する場合などには呼ばれないので注意！）
        /// </summary>
        protected virtual Task ViewWillDestroy(TSheet view, TRootViewState viewState) {
            return Task.CompletedTask;
        }


    }
}
