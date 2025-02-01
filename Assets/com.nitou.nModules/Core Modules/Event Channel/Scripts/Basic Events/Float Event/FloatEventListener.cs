using UnityEngine;

namespace Nitou.EventChannel {
    using Nitou.EventChannel.Shared;

    /// <summary>
    /// <see cref="float"/>型のイベントリスナー
    /// </summary>
    [AddComponentMenu(
        ComponentMenu.Prefix.EventChannel + "Float Event Listener"
    )]
    public class FloatEventListener : EventListener<float, FloatEventChannel> { }

}