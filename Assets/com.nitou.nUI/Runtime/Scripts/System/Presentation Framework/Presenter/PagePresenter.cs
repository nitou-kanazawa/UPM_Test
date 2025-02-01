using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityScreenNavigator.Runtime.Core.Page;

namespace Nitou.UI.PresentationFramework {

    public abstract class PagePresenter<TPage, TRootView, TRootViewState> : PagePresenter<TPage>, 
        IDisposableCollectionHolder
        where TPage : Page<TRootView, TRootViewState>
        where TRootView : AppView<TRootViewState>
        where TRootViewState : AppViewState, new() {

        private readonly List<IDisposable> _disposables = new();
        private TRootViewState _state;


        /// ----------------------------------------------------------------------------
        // 

        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected PagePresenter(TPage view) : base(view) {}


        ICollection<IDisposable> IDisposableCollectionHolder.GetDisposableCollection() {
            return _disposables;
        }
        
        /// <summary>
        /// 初期化処理
        /// </summary>
        protected sealed override void Initialize(TPage view) {
            base.Initialize(view);
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <param name="view"></param>
        protected sealed override void Dispose(TPage view) {
            base.Dispose(view);
            _disposables.ForEach(d => d.Dispose());
        }


        /// ----------------------------------------------------------------------------
        // LifecycleEvent

        protected sealed override async Task ViewDidLoad(TPage view) {
            await base.ViewDidLoad(view);

            // ViewStateの注入
            var state = new TRootViewState();
            _state = state;
            _disposables.Add(state);
            view.Setup(state);

            // 派生クラスの処理
            await ViewDidLoad(view, _state);
        }

        protected sealed override async Task ViewWillPushEnter(TPage view) {
            await base.ViewWillPushEnter(view);
            await ViewWillPushEnter(view, _state);
        }

        protected sealed override void ViewDidPushEnter(TPage view) {
            base.ViewDidPushEnter(view);
            ViewDidPushEnter(view, _state);
        }

        protected sealed override async Task ViewWillPushExit(TPage view) {
            await base.ViewWillPushExit(view);
            await ViewWillPushExit(view, _state);
        }

        protected sealed override void ViewDidPushExit(TPage view) {
            base.ViewDidPushExit(view);
            ViewDidPushExit(view, _state);
        }

        protected sealed override async Task ViewWillPopEnter(TPage view) {
            await base.ViewWillPopEnter(view);
            await ViewWillPopEnter(view, _state);
        }

        protected sealed override void ViewDidPopEnter(TPage view) {
            base.ViewDidPopEnter(view);
            ViewDidPopEnter(view, _state);
        }

        protected sealed override async Task ViewWillPopExit(TPage view) {
            await base.ViewWillPopExit(view);
            await ViewWillPopExit(view, _state);
        }

        protected sealed override void ViewDidPopExit(TPage view) {
            base.ViewDidPopExit(view);
            ViewDidPopExit(view, _state);
        }

        protected override async Task ViewWillDestroy(TPage view) {
            await base.ViewWillDestroy(view);
            await ViewWillDestroy(view, _state);
        }



        /// ----------------------------------------------------------------------------
        // LifecycleEvent (※派生クラスでの再定義用)

        /// <summary>
        /// このページがロードされた直後に呼ばれる
        /// </summary>
        protected virtual Task ViewDidLoad(TPage view, TRootViewState viewState) {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Push遷移によりこのページが表示される直前に呼ばれる
        /// </summary>
        protected virtual Task ViewWillPushEnter(TPage view, TRootViewState viewState) {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Push遷移によりこのページが表示された直後に呼ばれる
        /// </summary>
        protected virtual void ViewDidPushEnter(TPage view, TRootViewState viewState) {
        }

        /// <summary>
        /// Push遷移によりこのページが非表示になる直前に呼ばれる
        /// </summary>
        protected virtual Task ViewWillPushExit(TPage view, TRootViewState viewState) {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Push遷移によりこのページが非表示になった直後に呼ばれる
        /// </summary>
        protected virtual void ViewDidPushExit(TPage view, TRootViewState viewState) {
        }

        /// <summary>
        /// Pop遷移によりこのページが表示される直前に呼ばれる
        /// </summary>
        protected virtual Task ViewWillPopEnter(TPage view, TRootViewState viewState) {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Pop遷移によりこのページが表示された直後に呼ばれる 
        /// </summary>
        protected virtual void ViewDidPopEnter(TPage view, TRootViewState viewState) {
        }

        /// <summary>
        /// Pop遷移によりこのページが非表示になる直前に呼ばれる
        /// </summary>
        protected virtual Task ViewWillPopExit(TPage view, TRootViewState viewState) {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Pop遷移によりこのページが非表示になった直後に呼ばれる 
        /// </summary>
        protected virtual void ViewDidPopExit(TPage view, TRootViewState viewState) {
        }

        /// <summary>
        /// このページがリリースされる直前に呼ばれる 
        /// </summary>
        protected virtual Task ViewWillDestroy(TPage view, TRootViewState viewState) {
            return Task.CompletedTask;
        }
    }
}
