using UnityEngine;

namespace Nitou.EventChannel {
    using Nitou.EventChannel.Shared;

    /// <summary>
    /// Event listener for type of <see cref="int"/>.
    /// </summary>
    [AddComponentMenu(
        ComponentMenu.Prefix.EventChannel + "Int Event Listener"
    )]
    public class IntEventListener : EventListener<int, IntEventChannel> {}

}