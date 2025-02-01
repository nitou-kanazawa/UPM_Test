using System;
using UnityEngine;
using UniRx;

namespace Nitou.UI.Components {
    
    /// <summary>
    /// 基本機能のみの独自カーソルUI
    /// </summary>
    [AddComponentMenu(
        menuName: ComponentMenu.Prefix.UIComponents +"UI Cursor"
    )]
    [DisallowMultipleComponent]
    public class UICursor : MonoBehaviour, IUIComponent {

        // 実装
        private Subject<Unit> _onEnableSubject = new();
        private Subject<Unit> _onDisableSubject = new();

        /// <summary>
        /// アクティブになった時のイベント通知
        /// </summary>
        public IObservable<Unit> OnEnabled => _onEnableSubject;

        /// <summary>
        /// 非アクティブになった時のイベント通知
        /// </summary>
        public IObservable<Unit> OnDisabled => _onDisableSubject;

        
        /// ----------------------------------------------------------------------------
        // Public MonoBehaviour

        private void OnEnable() {
            _onEnableSubject.OnNext(Unit.Default);
        }

        private void OnDisable() {
            _onDisableSubject.OnNext(Unit.Default);
        }

        private void OnDestroy() {
            _onEnableSubject.Dispose();
            _onDisableSubject.Dispose();
        }
    }

}