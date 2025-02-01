#if UNITY_EDITOR
using System.ComponentModel;
#endif
using UnityEngine;
using UnityEngine.Timeline;

namespace Nitou.TweenPlayables
{
    [TrackBindingType(typeof(LineRenderer))]
    [TrackClipType(typeof(TweenLineRendererClip))]
#if UNITY_EDITOR
    [DisplayName("Tween Playables/General/Tween LineRenderer Track")]
#endif
    public class TweenLineRendererTrack : TweenAnimationTrack<LineRenderer, TweenLineRendererMixerBehaviour, TweenLineRendererBehaviour> { }
}