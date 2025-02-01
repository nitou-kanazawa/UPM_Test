using UnityEngine;

namespace Nitou.EventChannel {
    using Nitou.EventChannel.Shared;

    /// <summary>
    /// <see cref="void"/>型のイベントリスナー
    /// </summary>
    [AddComponentMenu(
        ComponentMenu.Prefix.EventChannel + "Void Event Listener"
    )]
    public class VoidEventListener : EventListener<VoidEventChannel> { }

}