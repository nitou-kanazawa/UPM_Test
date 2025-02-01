using UnityEngine;

namespace Nitou.UI.Components {

    [RequireComponent(typeof(CanvasGroup))]
    public abstract class ContentView : MonoBehaviour {

        protected CanvasGroup _canvasGroup;
    }
}
