using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nitou.UI.Components {

    [RequireComponent(typeof(UICursor))]
    [DisallowMultipleComponent]
    public abstract class UICursorAnimationBase : UIBehaviour {

        protected UICursor _cursor;


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method

        protected override void Awake() {
            base.Awake();
            _cursor = GetComponent<UICursor>();
            if (_cursor == null) return;

            // バインド
            _cursor.OnEnabled.Subscribe(_ => OnEnableAnimation()).AddTo(this);
            _cursor.OnDisabled.Subscribe(_ => OnDisableAnimation()).AddTo(this);

            // 初期化処理
            InitializeInternal();
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            // 終了処理
            DisposeInternal();
        }


        /// ----------------------------------------------------------------------------
        // Protected Method

        /// <summary>
        /// 初期化処理
        /// </summary>
        protected virtual void InitializeInternal() { }

        /// <summary>
        /// 非選択時の値を適用する
        /// </summary>
        protected abstract void OnEnableAnimation();

        /// <summary>
        /// 選択時の値を適用する
        /// </summary>
        protected abstract void OnDisableAnimation();

        /// <summary>
        /// 終了処理
        /// </summary>
        protected virtual void DisposeInternal() { }
    }
}
