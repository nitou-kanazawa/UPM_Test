using System;
using UniRx;
using UnityEngine;

namespace Nitou.UI.Components{

    /// <summary>
    /// Interface of the UI that handles the “Shake” event.
    /// </summary>
    public interface IUIShakeable : IUIComponent {

        /// <summary>
        /// Observable that notifiers when forward moved.
        /// </summary>
        public IObservable<Unit> OnMoveNext { get; }

        /// <summary>
        /// Observable that notifiers when backward moved.
        /// </summary>
        public IObservable<Unit> OnMovePrevious { get; }
    
    }
}
