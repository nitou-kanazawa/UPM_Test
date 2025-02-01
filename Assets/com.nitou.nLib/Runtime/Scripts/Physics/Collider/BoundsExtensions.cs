using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Nitou {

    /// <summary>
    /// <see cref="Bounds"/>型の基本的な拡張メソッド集．
    /// </summary>
    public static class BoundsExtensions {

        /// <summary>
        /// Bounds の全頂点を取得します。
        /// </summary>
        /// <param name="bounds">対象の Bounds</param>
        /// <returns>全8頂点の配列</returns>
        public static Vector3[] GetCorners(this Bounds bounds) {
            Vector3[] corners = new Vector3[8];
            corners[0] = bounds.min; // 左下奥
            corners[1] = new Vector3(bounds.min.x, bounds.min.y, bounds.max.z); // 左下手前
            corners[2] = new Vector3(bounds.min.x, bounds.max.y, bounds.min.z); // 左上奥
            corners[3] = new Vector3(bounds.min.x, bounds.max.y, bounds.max.z); // 左上手前
            corners[4] = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z); // 右下奥
            corners[5] = new Vector3(bounds.max.x, bounds.min.y, bounds.max.z); // 右下手前
            corners[6] = new Vector3(bounds.max.x, bounds.max.y, bounds.min.z); // 右上奥
            corners[7] = bounds.max; // 右上手前
            return corners;
        }

        /// <summary>
        /// 他の<see cref="Bounds"/>が範囲内に完全に含まれているか確認する拡張メソッド．
        /// </summary>
        public static bool Contains(this Bounds self, Bounds other) {

            // AABBの内外判定
            return self.Contains(other.min) && self.Contains(other.max);
        }

        /// <summary>
        /// 両者を含むBoundsを返す拡張メソッド．
        /// </summary>
        public static Bounds Union(this Bounds a, Bounds b) {
            // Calculate the minimum and maximum corners of the union bounds
            var min = Vector3.Min(a.min, b.min);
            var max = Vector3.Max(a.max, b.max);

            // Create and return a new Bounds that encompasses both
            return new Bounds((min + max) * 0.5f, max - min);
        }


        /// ----------------------------------------------------------------------------
        #region Gizmos

        /// <summary>
        /// Gizmoを表示する拡張メソッド．
        /// </summary>
        public static void DrawGizmo(this Bounds self, Color color) {
            using (new GizmoUtil.ColorScope(color)) {
                Gizmos.DrawWireCube(self.center, self.size);
            }
        }

        /// <summary>
        /// Gizmoを表示する拡張メソッド．
        /// </summary>
        public static void DrawGizmo(this Bounds self) => DrawGizmo(self, Colors.Green);
        #endregion
    }


    /// <summary>
    /// <see cref="Bounds"/>型を対象とした汎用メソッド集．
    /// </summary>
    public static class BoundUtils {

        public static Bounds Union(IReadOnlyCollection<Bounds> bounds) {
            if (bounds == null || bounds.Count < 1) throw new System.InvalidOperationException();
            if (bounds.Count == 1) return bounds.First();

            var min = bounds.Select(b => b.min).Aggregate((minPt, nextPt) => Vector3.Min(minPt, nextPt));
            var max = bounds.Select(b => b.max).Aggregate((maxPt, nextPt) => Vector3.Max(maxPt, nextPt));

            return new Bounds((min + max) * 0.5f, max - min);
        }
    }
}
