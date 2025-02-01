using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Nitou.DesignPattern.Pooling;

namespace Nitou.DebugInternal {
    internal static partial class GizmoDrawer{

        /// <summary>
        /// 基本図形
        /// </summary>
        internal static class Basic {

            /// ----------------------------------------------------------------------------
            #region 線

            /// <summary>
            /// 点列上に折れ線を描画する
            /// </summary>
            public static void DrawLines(IReadOnlyList<Vector3> points) {
                if (points == null || points.Count <= 1) return;

                Vector3 from = points.First();
                points.Skip(1).ForEach(p => {
                    Gizmos.DrawLine(from, p);
                    from = p;
                });
            }

            /// <summary>
            /// ２グループの各対応点を結ぶ線を描画する
            /// </summary>
            public static void DrawLineSet(IReadOnlyList<Vector3> points1, IReadOnlyList<Vector3> points2) {
                if (points1 == null || points2 == null || points1.Count != points2.Count) return;

                for (int i = 0; i < points1.Count; i++) {
                    Gizmos.DrawLine(points1[i], points2[i]);
                }
            }
            #endregion


            /// ----------------------------------------------------------------------------
            #region 円弧

            /// <summary>
            /// 円弧を描画する
            /// </summary>
            public static void DrawWireArc(float radius, float angle, int segments = 20) {

                Vector3 from = Vector3.forward * radius;
                var step = Mathf.RoundToInt(angle / segments);
                for (int i = 0; i <= angle; i += step) {
                    var to = new Vector3(
                        x: radius * Mathf.Sin(i * Mathf.Deg2Rad),
                        y: 0,
                        z: radius * Mathf.Cos(i * Mathf.Deg2Rad)
                    );
                    Gizmos.DrawLine(from, to);
                    from = to;
                }
            }

            /// <summary>
            /// 円弧を描画する
            /// </summary>
            public static void DrawWireArc(Vector3 center, Quaternion rotation, float radius, float angle, int segments = 20) {

                if (rotation.Equals(default)) {
                    rotation = Quaternion.identity;
                }

                var matrix = Matrix4x4.TRS(center, rotation, Vector3.one);
                using (new GizmoUtil.MatrixScope(matrix)) {
                    DrawWireArc(radius, angle, segments);
                }
            }

            /// <summary>
            /// 円弧を描画する
            /// </summary>
            public static void DrawWireArc(Matrix4x4 matrix, float radius, float angle, int segments) {

                using (new GizmoUtil.MatrixScope(matrix)) {
                    DrawWireArc(radius, angle, segments);
                }
            }


            // ※↓親の回転を考慮

            /// <summary>
            /// 円弧を描画する
            /// </summary>
            public static void DrawWireArc(Vector3 center, float radius, float angle, int segments, Quaternion rotation, Vector3 centerOfRotation) {

                if (rotation.Equals(default)) {
                    rotation = Quaternion.identity;
                }

                var matrix = Matrix4x4.TRS(centerOfRotation, rotation, Vector3.one);
                using (new GizmoUtil.MatrixScope(matrix)) {

                    var deltaTranslation = centerOfRotation - center;
                    Vector3 from = deltaTranslation + Vector3.forward * radius;
                    var step = Mathf.RoundToInt(angle / segments);
                    for (int i = 0; i <= angle; i += step) {
                        var to = new Vector3(
                            radius * Mathf.Sin(i * Mathf.Deg2Rad),
                            0,
                            radius * Mathf.Cos(i * Mathf.Deg2Rad)
                        ) + deltaTranslation;

                        Gizmos.DrawLine(from, to);
                        from = to;
                    }
                }
            }
            #endregion


            /// ----------------------------------------------------------------------------
            #region 円

            /// <summary>
            /// 円を描画する
            /// </summary>
            public static void DrawCircle(PlaneType type, float radius, int segments = 20) {
                var points = ListPool<Vector3>.New();
                
                MathUtils.CirclePoints(radius,points, segments, type: type);
                DrawLines(points);

                points.Free();
            }

            /// <summary>
            /// 円を描画する
            /// </summary>
            public static void DrawCircle(PlaneType type, Vector3 center, Quaternion rotation, float radius, int segments = 20) {
                if (rotation.Equals(default)) {
                    rotation = Quaternion.identity;
                }

                var matrix = Matrix4x4.TRS(center, rotation, Vector3.one);
                using (new GizmoUtil.MatrixScope(matrix)) {
                    DrawCircle(type, radius, segments);
                }
            }
            #endregion

        }
    }
}
