using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Nitou.DebugInternal{

    /// <summary>
    /// <see cref="ScrollRect"/>の各プロパティを可視化するためのデバッグ用コンポーネント
    /// </summary>
    [RequireComponent(typeof(ScrollRect))]
    internal class ScrollRectDebugger : DebugComponent<ScrollRect>{

        private void Start() {
        }
    }
}
