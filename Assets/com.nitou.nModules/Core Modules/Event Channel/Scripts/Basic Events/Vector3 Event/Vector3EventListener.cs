using UnityEngine;

namespace Nitou.EventChannel {
    using Nitou.EventChannel.Shared;

    /// <summary>
    /// Event listener for type of <see cref="Vector3"/>.
    /// </summary>
    [AddComponentMenu(
        ComponentMenu.Prefix.EventChannel + "Vector3 Event Listener"
    )]
    public class Vector3EventListener : EventListener<Vector3, Vector3EventChannel> { }
}
