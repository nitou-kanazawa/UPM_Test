using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;
using UniRx;
using System.Runtime.Serialization;

// [参考]
//  qiita: UniTask Ver2, UniTaskAsyncEnumerableまとめ https://qiita.com/toRisouP/items/8f66fd952eaffeaf3107
//  _: UniTaskがパワーアップ！『UniTask v2』を使おう！ https://hackmd.io/@-xLrSnFfROOeIeRnENCWcQ/HkVAMY5Sd
//  note: UniTaskに善玉例外を投げよう！CancellationToken.ThrowIfCancellationRequested() https://note.com/sandbox9/n/nff7ab030c9dd

namespace Nitou.DesignPattern {

    /// <summary>
    /// UniTaskに対応したシンプルなステートマシン．
    /// </summary>
    public partial class AwaitableStateMachine<TContext, TEvent> : IDisposable {

        /// <summary>
        /// ステートマシンの処理フェーズ
        /// </summary>
        public enum ProcessPhase {
            Idle,   // 停止中
            Enter,  // 
            Update,
            Exit,
        }

        /// <summary>
        /// ステートマシンが保持しているコンテキスト
        /// </summary>
        public TContext Context { get; private set; }


        /// ----------------------------------------------------------------------------
        // ステート情報

        /// <summary>
        /// 現在のステート
        /// </summary>
        public State CurrentState { get; private set; }

        /// <summary>
        /// 前回のステート
        /// </summary>
        public State PreviousState { get; private set; }


        /// ----------------------------------------------------------------------------
        // ステートマシン稼働情報

        /// <summary>
        /// ステートマシンの稼働状況
        /// </summary>
        public ProcessPhase CurrentPhase { get; private set; }

        /// <summary>
        /// ステートマシンが稼働中かどうか
        /// </summary>
        public bool IsRunningPhase => CurrentPhase != ProcessPhase.Idle;

        /// <summary>
        /// ステートマシンが更新処理中かどうか
        /// </summary>
        public bool IsUpdating => CurrentPhase == ProcessPhase.Update;

        /// <summary>
        /// ステートマシンが開始処理中かどうか
        /// </summary>
        public bool IsEntering => CurrentPhase == ProcessPhase.Enter;


        /// ----------------------------------------------------------------------------

        // 登録ステートのリスト
        private readonly Dictionary<Type, State> _states = new();   // ※Typeをキーとする

        // 遷移リクエスト
        private readonly Queue<State> _transitionsQueue = new();

        // キャンセル用
        private CancellationTokenSource _cancellationTokenSource;


        /// ----------------------------------------------------------------------------
        // Public Method 

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AwaitableStateMachine(TContext context) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            // 
            Context = context;
            CurrentPhase = ProcessPhase.Idle;
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose() {
            Stop();
        }


        /// ----------------------------------------------------------------------------
        #region ステートマシン操作系

        /// <summary>
        /// ステートマシンを実行する
        /// </summary>
        public void Run() {
            if (IsRunningPhase) {
                throw new InvalidOperationException("ステートマシンは、起動中です");
            }

            _cancellationTokenSource = new();
                UpdateLoop(_cancellationTokenSource.Token);
 
        }

        /// <summary>
        /// ステートマシンを停止する
        /// </summary>
        public void Stop() {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region ステート遷移テーブル構築系

        /// <summary>
        /// ステートを登録する
        /// </summary>
        public void RegisterState(State state) {
            if (IsRunningPhase) {
                throw new InvalidOperationException("ステートマシンは、起動中です");
            }

            if (state == null) {
                throw new ArgumentNullException(nameof(state));
            }

            Type type = state.GetType();
            if (_states.ContainsKey(type)) {
                // 上書き登録を許さないので例外を吐く
                throw new ArgumentException($"ステート'{state.GetType().Name}'は、既にリストに登録済みです");
            }

            // リストへ追加
            state.Init(this);
            _states.Add(type, state);
        }

        /// <summary>
        /// ステートを登録する
        /// </summary>
        public void RegisterState<TState>() where TState : State, new() {
            if (IsRunningPhase) {
                throw new InvalidOperationException("ステートマシンは、起動中です");
            }

            Type type = typeof(TState);
            if (_states.ContainsKey(type)) {
                // 上書き登録を許さないので例外を吐く
                throw new ArgumentException($"ステート'{type}'は、既にリストに登録済みです");
            }

            // リストへ追加
            var state = new TState();
            state.Init(this);
            _states.Add(type, state);
        }
        #endregion


        /// ----------------------------------------------------------------------------
        // Public Method (ステート遷移)

        /// <summary>
        /// 遷移リクエストを積む
        /// </summary>
        public void EnqueueTransitionRequest<TState>() where TState : State {
            // 登録リストに含まれる場合，
            if (_states.TryGetValue(typeof(TState), out State state)) {
                _transitionsQueue.Enqueue(state);
            } else {
                throw new Exception($"State: {typeof(TState)} is not registered to state machine.");
            }
        }

        /// <summary>
        /// イベントが発行されたときの対応処理
        /// </summary>
        public void OnReciveEvent(TEvent eventId) {
            if (!IsRunningPhase) return;

            // 現在ステートの対応メソッドを実行
            CurrentState?.HandleEvent(eventId);
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// 指定ステートへ遷移する
        /// </summary>
        private async UniTask ChangeState(State nextState) {

            Debug_.Log($"Change State : [{CurrentState?.GetType().Name}]→[{nextState?.GetType().Name}]", Colors.SkyBlue);

            // 現在ステートの終了処理
            CurrentPhase = ProcessPhase.Exit;
            if (CurrentState != null) {
                PreviousState = CurrentState;
                CurrentState = null;

                // 終了処理
                await PreviousState.OnExit(toState: nextState);
            }

            // 新ステートの開始処理
            CurrentPhase = ProcessPhase.Enter;
            if (nextState != null) {
                CurrentState = nextState;

                // 開始処理
                await nextState.OnEnter(fromState: PreviousState);
            } else {
                throw new Exception($"State: {nextState.GetType().Name} is not registered to state machine.");
            }
        }

        /// <summary>
        /// 遷移フラグを確認して遷移先ステートを返す
        /// </summary>
        private State CheckTransitionRequests() {
            // 遷移リクエストの取得
            CurrentState?.CheckExitTransition();     // ※現在ステート側

            // 遷移リクエストの検証
            while (_transitionsQueue.Count != 0) {
                State nextState = _transitionsQueue.Dequeue();
                if (nextState == null) continue;

                // 遷移可能かの検証
                bool success = nextState.CheckEnterTransition(CurrentState);    // ※新ステート側
                if (success) {
                    return nextState;
                }
            }
            return null;
        }

        /// <summary>
        /// 更新ループ
        /// </summary>
        private async void UpdateLoop(CancellationToken token) {

            // 毎フレーム次のUpdateのタイミングまでawaitする
            await foreach (var _ in UniTaskAsyncEnumerable
                               .EveryUpdate()
                               .WithCancellation(token)) {

                // 遷移リクエストの確認
                var nextState = CheckTransitionRequests();      // 遷移先の取得
                _transitionsQueue.Clear();                      // ※遷移リクエストは確認後にクリア
                
                // ステート遷移
                if (nextState != null) {
                    await ChangeState(nextState);
                }

                // ステート更新処理
                CurrentPhase = ProcessPhase.Update;
                CurrentState?.OnUpdate();
            }


            Debug_.Log("Stoped");
        }


    }

}