using UnityEngine;
using UnityEngine.UI;

namespace Nitou{

    [ExecuteAlways]
    public class ScrollVarTick : MonoBehaviour{

        [SerializeField] Scrollbar _scrollbar;
        private RectTransform _scrollbarRect;

        [Range(0,1)]
        [SerializeField] float _rate = 0;

        public float Rate {
            get => _rate;
            set {
                _rate = Mathf.Clamp01(value);
                UpdatePosition();
            }
        }


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method

        private void Awake() {
            if(_scrollbar != null) {
                _scrollbarRect = _scrollbar.GetComponent<RectTransform>();
            }

            // 
            UpdatePosition();
        }

        private void OnValidate() {
            if (_scrollbar != null && _scrollbarRect == null) {
                _scrollbarRect = _scrollbar.GetComponent<RectTransform>();
            } else {
                _scrollbarRect = null;
            }

            UpdatePosition();
        }

        /// ----------------------------------------------------------------------------
        // Private Method

        private void UpdatePosition() {
            if (_scrollbarRect == null) return;

            var rect = _scrollbarRect.GetWorldRect();
            var pos = new Vector3((rect.xMin + rect.xMax) / 2, rect.yMin + rect.height * _rate);
            transform.position = pos;
        }
    }
}
