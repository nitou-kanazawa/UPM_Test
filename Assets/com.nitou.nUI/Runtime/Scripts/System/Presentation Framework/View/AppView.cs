using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Nitou.UI.PresentationFramework {

    /// <summary>
    /// UI要素のインスタンス．
    /// </summary>
    public abstract class AppView<TState> : MonoBehaviour 
        where TState : AppViewState {
        
        /// <summary>
        /// 初期化が完了しているかどうか．
        /// </summary>
        private bool _isInitialized;

        /// <summary>
        /// 初期化処理．
        /// </summary>
        public async UniTask InitializeAsync(TState state) {
            if (_isInitialized) return;

            _isInitialized = true;

            await Initialize(state);
        }

        /// <summary>
        /// 初期化処理 (※派生クラスでの定義用)．
        /// </summary>
        protected abstract UniTask Initialize(TState state);
    }

}
