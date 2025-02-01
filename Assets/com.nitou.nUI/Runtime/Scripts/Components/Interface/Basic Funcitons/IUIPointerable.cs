using System;
using UniRx;

namespace Nitou.UI.Components{

    /// <summary>
    /// Interface of the UI that handles the “Pointer” event.
    /// </summary>
    public interface IUIPointerable{

        /// <summary>
        /// Observable that nortifies when the pointe has enter.
        /// </summary>
        public IObservable<Unit> OnPointerEnter { get; }

        /// <summary>
        /// Observable that nortifies when the pointe has exit.
        /// </summary>
        public IObservable<Unit> OnPointerExit { get; }
    }
}
