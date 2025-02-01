using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Nitou.UI.Components{

    /// <summary>
    /// Trigger component to select elements when the pointer overlap.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Selectable))]
    public sealed class SelectTrigger_OnPointerOverlap : MonoBehaviour, 
        IPointerEnterHandler, IPointerExitHandler{

        private Selectable _selectable;

        private void Awake() {
            _selectable = gameObject.GetComponent<Selectable>();
        }


        /// ----------------------------------------------------------------------------
        // Interface Method

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData) {
            if (_selectable is null) return;

            EventSystem.current.SetSelectedGameObject(_selectable.gameObject);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData) {

        }
    }
}
