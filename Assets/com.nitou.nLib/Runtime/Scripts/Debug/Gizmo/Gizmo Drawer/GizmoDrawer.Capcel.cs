using UnityEngine;

// [参考]
//  Kanのメモ帳: ギズモで矢印、円柱、カプセル、円、弧を描画出来るようにするGizmoExtensions https://kan-kikuchi.hatenablog.com/entry/GizmoExtensions

namespace Nitou.DebugInternal {
    internal static partial class GizmoDrawer {

        /// <summary>
        /// カプセル
        /// </summary>
        internal static class Capcel {

            /// ----------------------------------------------------------------------------
            #region 描画メソッド

            /// <summary>
            /// カプセルを描画する
            /// </summary>
            public static void DrawWireCapsule(Vector3 center, float radius, float height, Quaternion rotation = default) {

                //var old = Gizmos.matrix;

                //if (rotation.Equals(default)) {
                //    rotation = Quaternion.identity;
                //}
                //Gizmos.matrix = Matrix4x4.TRS(center, rotation, Vector3.one);
                //var half = height / 2 - radius;

                ////draw cylinder base
                //DrawWireCylinder(center, rotation, radius, height - radius * 2);

                //// draw upper cap
                //// do some cool stuff with orthogonal matrices
                //var mat = Matrix4x4.Translate(center + rotation * Vector3.up * half) * Matrix4x4.Rotate(rotation * Quaternion.AngleAxis(90, Vector3.forward));
                //BasicShapeDrawer.DrawWireArc(mat, radius, 180, 20);
                //mat = Matrix4x4.Translate(center + rotation * Vector3.up * half) * Matrix4x4.Rotate(rotation * Quaternion.AngleAxis(90, Vector3.up) * Quaternion.AngleAxis(90, Vector3.forward));
                //BasicShapeDrawer.DrawWireArc(mat, radius, 180, 20);

                //// draw lower cap
                //mat = Matrix4x4.Translate(center + rotation * Vector3.down * half) * Matrix4x4.Rotate(rotation * Quaternion.AngleAxis(90, Vector3.up) * Quaternion.AngleAxis(-90, Vector3.forward));
                //BasicShapeDrawer.DrawWireArc(mat, radius, 180, 20);
                //mat = Matrix4x4.Translate(center + rotation * Vector3.down * half) * Matrix4x4.Rotate(rotation * Quaternion.AngleAxis(-90, Vector3.forward));
                //BasicShapeDrawer.DrawWireArc(mat, radius, 180, 20);

                //Gizmos.matrix = old;
            }
            #endregion



        }
    }
}