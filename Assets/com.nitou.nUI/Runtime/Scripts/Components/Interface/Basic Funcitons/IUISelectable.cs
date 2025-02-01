using System;
using UniRx;

namespace Nitou.UI.Components {

    /// <summary>
    /// Interface of the UI that handles the “Select” event.
    /// </summary>
    public interface IUISelectable : IUIComponent {

        /// <summary>
        /// Observable that nortifies when object is selected.
        /// </summary>
        public IObservable<Unit> OnSelected { get; }

        /// <summary>
        /// Observable that nortifies when object is deselected.
        /// </summary>
        public IObservable<Unit> OnDeselected { get; }
    }

}