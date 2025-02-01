using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;
using UnityScreenNavigator.Runtime.Core.Page;

// [NOTE] "USN_USE_ASYNC_METHODS"シンボルが必要．

namespace Nitou.UI.PresentationFramework {

    [RequireComponent(typeof(CanvasGroup))]
    public abstract class Page<TRootView, TViewState> : Page
        where TRootView : AppView<TViewState>
        where TViewState : AppViewState {

        public TRootView root;
        private TViewState _state;

        /// <summary>
        /// 初期化済みかどうか．
        /// </summary>
        public bool IsInitialized => _isInitialized;
        private bool _isInitialized;

        /// <summary>
        /// 初期化タイミング．
        /// （※InitializeだとViewStateが生成されていないので注意）
        /// </summary>
        protected virtual ViewInitializationTiming RootInitializationTiming => ViewInitializationTiming.BeforeFirstEnter;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// View Stateの設定
        /// </summary>
        public void Setup(TViewState state) {
            _state = state;
        }


        /// ----------------------------------------------------------------------------
        // Lifecycle Events

        public override async Task Initialize() {
            Assert.IsNotNull(root);

            await base.Initialize();

            // RootViewの初期化処理
            if (RootInitializationTiming == ViewInitializationTiming.Initialize && !_isInitialized) {
                await root.InitializeAsync(_state);
                _isInitialized = true;
            }
        }

        public override async Task WillPushEnter() {
            Assert.IsNotNull(root);

            await base.WillPushEnter();

            // RootViewの初期化処理
            if (RootInitializationTiming == ViewInitializationTiming.BeforeFirstEnter && !_isInitialized) {
                await root.InitializeAsync(_state);
                _isInitialized = true;
            }
        }

    }
}
