using System;

namespace Nitou.UI.PresentationFramework {

    /// <summary>
    /// UI要素の状態値．
    /// </summary>
    public abstract class AppViewState : IDisposable {

        /// <summary>
        /// 終了処理が完了しているかどうか．
        /// </summary>
        private bool _isDisposed;

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose() {
            if (_isDisposed) throw new ObjectDisposedException(nameof(AppViewState));

            DisposeInternal();
            _isDisposed = true;
        }

        /// <summary>
        /// 終了処理 (※派生クラスでの定義用)
        /// </summary>
        protected abstract void DisposeInternal();
    }
}
