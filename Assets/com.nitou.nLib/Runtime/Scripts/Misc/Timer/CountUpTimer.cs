using System;
using UniRx;
using UnityEngine;

// [参考]
//  _: Mathfの切り上げ、切り捨て、偶数丸め。使い分けが大事よね https://ekulabo.com/mathf-round

namespace Nitou {

    /// <summary>
    /// カウントアップ方式のタイマー
    /// </summary>
    public class CountUpTimer : ITimer, IDisposable {

        private readonly ReactiveProperty<int> _currentRP;
        private IDisposable _subscription = null;

        private float _elapsedTime;           // ※実計算用の変数
                
        /// <summary>
        /// 現在の時間
        /// </summary>
        public IReadOnlyReactiveProperty<int> Current => _currentRP;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CountUpTimer() {
            _elapsedTime = 0;
            _currentRP = new ReactiveProperty<int>(0);
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose() {
            Stop();

            _currentRP?.Dispose();
        }


        /// ----------------------------------------------------------------------------
        // Public Method (タイマー操作)
        
        public void Start() {
            if (_subscription != null) {
                Debug_.LogWarning("Timer is already running.");
                return;
            }

            // 更新処理
            _subscription = Observable.EveryUpdate()
                .Subscribe(_ => {
                    // Time.deltaTimeに基づいて更新
                    _elapsedTime += Time.deltaTime;
                    _currentRP.Value = Mathf.FloorToInt(_elapsedTime); // ※負の無限大方向に切り下げ
                });
        }

        public void Stop() {
            _subscription?.Dispose();
            _subscription = null;
        }

        public void Reset() {
            _elapsedTime = 0;
            _currentRP.Value = 0;
        }
    }
}
