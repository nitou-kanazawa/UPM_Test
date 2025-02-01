using UnityEngine;

namespace Nitou.EventChannel {
    using Nitou.EventChannel.Shared;

    /// <summary>
    /// AudioClip型のイベントリスナー 
    /// </summary>
    [AddComponentMenu(
        ComponentMenu.Prefix.EventChannel + "AudioClip Event Listener"
    )]
    public class AudioClipEventListener : EventListener<AudioClip, AudioClipEventChannel> { }
}
