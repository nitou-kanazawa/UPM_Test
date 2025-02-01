using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Nitou.UI.Overlay {

    /// <summary>
    /// オーバーレイを管理する
    /// </summary>
    public sealed class OverlayContainer : MonoBehaviour {

        [SerializeField, ReadOnly] private OverlayBase _current;

        private readonly Dictionary<string, OverlayBase> _instanceCacheByName = new ();

        internal OverlayBase Current => _current;
        public bool HasOverlay => _current != null;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// オーバーレイを読み込む
        /// </summary>
        public void Load<TOverlay>(string resourceKey) 
            where TOverlay : OverlayBase {
            if (HasOverlay) {
                _current.DestroyGameObject();
            }

            // 
            var prefab = Resources.Load<TOverlay>(resourceKey);
            if (prefab == null) return;

            _current = Instantiate<TOverlay>(prefab, this.transform);
        }






        /// ----------------------------------------------------------------------------
        // Private Method



#if UNITY_EDITOR
        private void OnValidate() {

        }
#endif

    }

}
