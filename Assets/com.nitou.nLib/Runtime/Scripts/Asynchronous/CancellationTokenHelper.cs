using System.Threading;

// [REF]
//  Zenn: UniRx/UniTask大全 https://zenn.dev/tmb/articles/e4fb3fe350852f

namespace Nitou {

    /// <summary>
    /// CancellationTokenSourceインスタンスを入れ替えて繰り返し使用するためのラッパー．
    /// </summary>
    public sealed class CancellationTokenHelper {
        
        /// <summary>
        /// ソース
        /// </summary>
        public CancellationTokenSource Cts { get; private set; }

        /// <summary> 
        /// トークン 
        /// </summary>
        public CancellationToken Token => Cts.Token;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public CancellationTokenHelper() {
            Reset();
        }

        /// <summary>
        /// デストラクタ．
        /// </summary>
        ~CancellationTokenHelper() {
            Dispose();
        }

        /// <summary>
        /// リセット．
        /// </summary>
        public void Reset() {
            Dispose();
            Cts = new CancellationTokenSource();
        }

        /// <summary>
        /// 破棄．
        /// </summary>
        public void Dispose() {
            if (Cts != null) {
                Cts.Cancel();
                Cts.Dispose();
                Cts = null;
            }
        }
    }
}
