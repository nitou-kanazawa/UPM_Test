using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Nitou.DesignPattern {

    public partial class AwaitableStateMachine<TContext, TEvent> {

        /// <summary>
        /// ステート基底クラス
        /// </summary>
        public abstract class State {

            /// <summary>
            /// このステートが所属するステートマシン
            /// </summary>
            public AwaitableStateMachine<TContext,TEvent> StateMachine { get; protected set; }

            /// <summary>
            /// このステートが所属するステートマシンが持っているコンテキスト
            /// </summary>
            public TContext Context { get; protected set; }


            /// ----------------------------------------------------------------------------

            /// <summary>
            /// ステート初期化処理
            /// </summary>
            internal void Init(AwaitableStateMachine<TContext, TEvent> stateMachine) {

                // コンポーネント
                StateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
                Context = stateMachine.Context;

                OnInit();
            }

            /// <summary>
            /// 派生クラス用の初期化処理
            /// </summary>
            protected virtual void OnInit() { }


            /// ----------------------------------------------------------------------------
            // Public Method (ステート遷移条件)

            /// <summary>
            /// Checks if the required conditions to exit this state are true. If so it returns the desired state (null otherwise). After this the state machine will
            /// proceed to evaluate the "enter transition" condition on the target state.
            /// </summary>
            public virtual void CheckExitTransition() {

                // [処理の一例]
                //if (conditionA) {
                //    EnqueueTransition<TargetStateA>();
                //} else if (conditionB) {
                //    EnqueueTransition<TargetStateB>();
                //    EnqueueTransition<TargetStateC>();
                //}
            }

            /// <summary>
            /// Checks if the required conditions to enter this state are true. If so the state machine will automatically change the current state to the desired one.
            /// </summary>  
            public virtual bool CheckEnterTransition(State fromState) => true;


            /// ----------------------------------------------------------------------------
            // Public Method (イベント処理)

            /// <summary>
            /// イベントを受け取ったときの対応処理
            /// </summary>
            public virtual void HandleEvent(TEvent eventId) {}


            /// ----------------------------------------------------------------------------
            // Public Method (ステート遷移処理)

            /// <summary>
            /// ステート開始処理
            /// </summary>
            public virtual async UniTask OnEnter(State fromState) {
                await UniTask.Yield();
            }

            /// <summary>
            /// ステート終了処理
            /// </summary>
            public virtual async UniTask OnExit(State toState) {
                await UniTask.Yield();
            }


            /// ----------------------------------------------------------------------------
            // Public Method (ステート更新処理)

            /// <summary>
            /// ステート更新処理
            /// </summary>
            public virtual void OnUpdate() {
            }


        }
    }
}

