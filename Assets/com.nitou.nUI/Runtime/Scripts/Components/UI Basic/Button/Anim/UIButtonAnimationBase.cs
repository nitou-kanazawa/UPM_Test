using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nitou.UI.Components {

    [RequireComponent(typeof(UIButton))]
    [DisallowMultipleComponent]
    public abstract class UIButtonAnimationBase : UIBehaviour {

        protected UIButton _button;


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method

        protected override void Awake() {
            base.Awake();
            _button = GetComponent<UIButton>();
            if (_button == null) return;

            // バインド
            _button.OnDeselected.Subscribe(_ => OnDeselectAnimation()).AddTo(this);
            _button.OnSelected.Subscribe(_ => OnSelectAnimation()).AddTo(this);
            _button.OnSubmited.Subscribe(_ => OnClickAnimation()).AddTo(this);

            // 初期化処理
            InitializeInternal();
        }

        protected override void OnDestroy() {
            // 終了処理
            DisposeInternal();

            base.OnDestroy();
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
        protected abstract void OnDeselectAnimation();

        /// <summary>
        /// 選択時の値を適用する
        /// </summary>
        protected abstract void OnSelectAnimation();

        /// <summary>
        /// クリック時のアニメーション
        /// </summary>
        protected abstract void OnClickAnimation();

        /// <summary>
        /// 終了処理
        /// </summary>
        protected virtual void DisposeInternal() { }
    }
}
