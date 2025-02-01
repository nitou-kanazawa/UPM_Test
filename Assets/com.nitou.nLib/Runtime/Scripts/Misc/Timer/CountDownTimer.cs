using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

// [参考]
//  qiita: UniRxでカウントダウンタイマーを作る https://qiita.com/toRisouP/items/581ffc0ddce7090b275b
//  zenn: オレオレUniRxタイマー https://zenn.dev/keisuke114/scraps/5581b16d793806

namespace Nitou {

    /// <summary>
    /// カウントダウン方式のタイマー
    /// </summary>
    public class CountDownTimer : ITimer, IDisposable {

        private readonly ReactiveProperty<int> _currentRP;
        private readonly Subject<Unit> _overSubject = new();
        private IDisposable _subscription = null;

        private float _elapsedTime;           // ※実計算用の変数

        /// <summary>
        /// 
        /// </summary>
        public int Max { get; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsTimeOverd => _currentRP.Value <= 0f;

        /// <summary>
        /// 現在の時間
        /// </summary>
        public IReadOnlyReactiveProperty<int> Current => _currentRP;

        /// <summary>
        /// 終了通知
        /// </summary>
        public IObservable<Unit> OverObservable => _overSubject;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CountDownTimer(int max) {
            if (max <= 0) throw new System.InvalidOperationException();
            
            Max = max;

            _elapsedTime = Max;
            _currentRP = new ReactiveProperty<int>(Max);
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose() {
            Stop();

            _currentRP?.Dispose();
            _overSubject?.Dispose();
        }


        /// ----------------------------------------------------------------------------
        // Public Method (タイマー操作)

        public void Start() {
            if (_subscription != null) {
                Debug_.LogWarning("Timer is already running.");
                return;
            }
            if(IsTimeOverd) {
                Debug_.LogWarning("Time is up. Requires reset to use.");
                return;
            }

            // 更新処理
            _subscription = Observable.EveryUpdate()
                .Subscribe(_ => {
                    // Time.deltaTimeに基づいて更新
                    _elapsedTime -= Time.deltaTime;
                    _currentRP.Value = Mathf.CeilToInt(_elapsedTime);    // ※正の無限大方向に切り上げ

                    // 残り時間が0になったらTimeOverイベントを発行
                    if(IsTimeOverd) {
                        _overSubject.OnNext(Unit.Default);
                        Stop();
                    }
                });
        }

        public void Stop() {
            _subscription?.Dispose();
            _subscription = null;
        }

        public void Reset() {
            _elapsedTime = Max;
            _currentRP.Value = Max;
        }

    }
}
