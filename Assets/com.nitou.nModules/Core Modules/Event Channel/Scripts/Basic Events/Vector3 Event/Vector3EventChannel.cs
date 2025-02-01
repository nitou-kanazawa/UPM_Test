using UnityEngine;

namespace Nitou.EventChannel {
    using Nitou.EventChannel.Shared;

    /// <summary>
    /// Event channel for type of <see cref="Vector3"/>.
    /// </summary>
    [CreateAssetMenu(
        fileName = "Event_Vector3",
        menuName = AssetMenu.Prefix.EventChannel + "Vector3 Event"
    )]
    public class Vector3EventChannel : EventChannel<Vector3> { }
}
