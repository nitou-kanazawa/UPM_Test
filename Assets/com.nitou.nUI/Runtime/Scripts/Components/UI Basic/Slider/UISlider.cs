using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;
using Sirenix.OdinInspector;

namespace Nitou.UI.Components {

    /// <summary>
    /// 基本機能のみの独自スライダーUI
    /// </summary>
    [AddComponentMenu(menuName: ComponentMenu.Prefix.UIComponents + "UI Slider")]
    public class UISlider : Slider, IUISlider {

        // Event stream
        private readonly Subject<Unit> _onSelectSubject = new();
        private readonly Subject<Unit> _onDeselectSubject = new();


        /// ----------------------------------------------------------------------------
        // Properity

        /// <summary>
        /// 値が更新された時に通知されるObservable．
        /// </summary>
        public IObservable<float> OnValuChanged => base.onValueChanged.AsObservable();

        /// <summary>
        /// 選択された時に通知されるObservable．
        /// </summary>
        public IObservable<Unit> OnSelected => _onSelectSubject;

        /// <summary>
        /// 非選択された時に通知されるObservable．
        /// </summary>
        public IObservable<Unit> OnDeselected => _onDeselectSubject;


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method

        protected override void OnDestroy() {
            _onSelectSubject?.Dispose();
            _onSelectSubject?.Dispose();

            base.OnDestroy();
        }


        /// ----------------------------------------------------------------------------
        // Interface Method


        /// <summary>
        /// 選択されたときの処理
        /// </summary>
        public override void OnSelect(BaseEventData eventData) {
            base.OnSelect(eventData);
            _onSelectSubject.OnNext(Unit.Default);
        }

        /// <summary>
        /// 選択されたときの処理
        /// </summary>
        public override void OnDeselect(BaseEventData eventData) {
            base.OnDeselect(eventData);
            _onDeselectSubject.OnNext(Unit.Default);
        }
    }
}