using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Nitou.UI.Components{

    /// <summary>
    /// Trigger component to select elements when start.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class SelectTrigger_OnStart : MonoBehaviour{

        private Selectable _selectable;

        public void Start() {
            _selectable = gameObject.GetComponent<Selectable>();

            if (_selectable!=null && EventSystem.current != null) {
                _selectable.Select();
            }
        }

    }
}
