using System.Collections.Generic;
using UnityEngine;

namespace Nitou.DebugInternal {
    internal static partial class GizmoDrawer {

        /// <summary>
        /// 直方体
        /// </summary>
        internal static class Cube {

            /// ----------------------------------------------------------------------------
            #region 描画メソッド

            /// <summary>
            /// ワイヤーキューブを描画する
            /// </summary>
            public static void DrawWireCube(Vector3 center, Quaternion rotation, Vector3 size) {

                if (rotation.Equals(default)) {
                    rotation = Quaternion.identity;
                }

                var matrix = Matrix4x4.TRS(center, rotation, size);
                using (new GizmoUtil.MatrixScope(matrix)) {
                    Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
                }
            }

            /// <summary>
            /// キューブを描画する
            /// </summary>
            public static void DrawCube(Vector3 center, Quaternion rotation, Vector3 size) {

                if (rotation.Equals(default)) {
                    rotation = Quaternion.identity;
                }

                var matrix = Matrix4x4.TRS(center, rotation, size);
                using (new GizmoUtil.MatrixScope(matrix)) {
                    Gizmos.DrawCube(Vector3.zero, Vector3.one);
                }
            }
            #endregion
        }
    }
}
