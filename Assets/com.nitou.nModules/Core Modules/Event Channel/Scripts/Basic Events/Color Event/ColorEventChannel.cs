using UnityEngine;

namespace Nitou.EventChannel {
    using Nitou.EventChannel.Shared;

    /// <summary>
    /// <see cref="Color"/>型のイベントチャンネル
    /// </summary>
    [CreateAssetMenu(
        fileName = "Event_Color",
        menuName = AssetMenu.Prefix.EventChannel + "Color Event"
    )]
    public class ColorEventChannel : EventChannel<Color> { }

}