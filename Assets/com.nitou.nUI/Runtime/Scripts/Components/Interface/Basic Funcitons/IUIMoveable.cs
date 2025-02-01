using System;
using UnityEngine.EventSystems;

namespace Nitou.UI.Components{

    /// <summary>
    /// Interface of the UI that handles the “Move” event
    /// </summary>
    public interface IUIMoveable : IUIComponent{

        /// <summary>
        /// Observable that nortifies when a "Move" input is detected.
        /// </summary>
        public IObservable<MoveDirection> OnMoved { get; }
    }
}
