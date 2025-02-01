using UnityEngine;

namespace Nitou.EventChannel {
    using Nitou.EventChannel.Shared;

    /// <summary>
    /// <see cref="Color"/>型のイベントリスナー
    /// </summary>
    [AddComponentMenu(
        ComponentMenu.Prefix.EventChannel + "Color Event Listener"
    )]
    public class ColorEventListener : EventListener<Color, ColorEventChannel> { }

}