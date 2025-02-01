using UnityEngine;

namespace Nitou.EventChannel {
    using Nitou.EventChannel.Shared;

    /// <summary>
    /// <see cref="void"/>型のイベントチャンネル
    /// </summary>
    [CreateAssetMenu(
        fileName = "Event_Void",
        menuName = AssetMenu.Prefix.EventChannel + "Void Event"
    )]
    public class VoidEventChannel : EventChannel { }

}