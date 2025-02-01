using UnityEngine;

namespace Nitou.EventChannel{
    using Nitou.EventChannel.Shared;

    /// <summary>
    /// AudioClip型のイベントチャンネル
    /// </summary>
    [CreateAssetMenu(
        fileName = "Event_AudioClip",
        menuName = AssetMenu.Prefix.EventChannel + "AudioClip Event"
    )]
    public class AudioClipEventChannel : EventChannel<AudioClip>{}
}
