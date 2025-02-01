using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Nitou.UI.Components{

    /// <summary>
    /// Trigger component to select elements when start.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class SelectTrigger_OnEnable : MonoBehaviour{

        private Selectable _selectable;
        public Selectable Selectable => 
            _selectable != null ? _selectable : (_selectable =gameObject.GetComponent<Selectable>());

        private void OnEnable() {

            if (Selectable != null && EventSystem.current != null) {
                Selectable.Select();
            }
        }
    }
}
