using System;
using UniRx;

namespace Nitou.UI.Components {

    /// <summary>
    /// Interface of the UI that handles the “Submit” event.
    /// </summary>
    public interface IUISubmitable : IUIComponent {

        /// <summary>
        /// 決定入力された時のイベント通知
        /// </summary>
        public IObservable<Unit> OnSubmited { get; }
    }

}