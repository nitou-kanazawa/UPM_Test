using UnityEngine;

namespace Nitou {
    public static partial class RectTransformExtensions {

        /// <summary>
        /// <see cref="RectTransform"/>のアンカー、ピボット座標を保持しておくスコープ
        /// </summary>
        public class AnchorPivotCacheScope : System.IDisposable {

            // target
            private readonly RectTransform _rect;

            // cache
            private readonly Vector2 _anchorMin;
            private readonly Vector2 _anchorMax;
            private readonly Vector2 _pivot;

            public AnchorPivotCacheScope(RectTransform rect) {
                if (rect == null) throw new System.ArgumentNullException(nameof(rect));

                _rect = rect;
                _anchorMin = rect.anchorMin;
                _anchorMax = rect.anchorMax;
                _pivot = rect.pivot;
            }

            public void Dispose() {
                _rect.anchorMin = _anchorMin;
                _rect.anchorMax = _anchorMax;
                _rect.anchorMin = _pivot;
            }
        }

    }
}