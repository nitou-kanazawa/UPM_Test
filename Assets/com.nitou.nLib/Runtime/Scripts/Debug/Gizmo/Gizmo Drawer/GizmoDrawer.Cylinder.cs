using UnityEngine;
using Nitou.DesignPattern.Pooling;

// [参考]
//  Kanのメモ帳: ギズモで矢印、円柱、カプセル、円、弧を描画出来るようにするGizmoExtensions https://kan-kikuchi.hatenablog.com/entry/GizmoExtensions

namespace Nitou.DebugInternal {
    internal static partial class GizmoDrawer {

        /// <summary>
        /// 円柱
        /// </summary>
        internal static class Cylinder {

            /// <summary>
            /// 円形を定義するパラメータ
            /// </summary>
            private struct DiscParam {
                public float radius;
                public int segments;
                public float offset;

                public DiscParam(float radius, int segments, float offset = default) {
                    this.radius = radius;
                    this.segments = segments;
                    this.offset = offset;
                }
            }


            /// ----------------------------------------------------------------------------
            #region 描画メソッド

            /// <summary>
            /// 円柱を描画する
            /// </summary>
            public static void DrawWireCylinder(PlaneType type, Vector3 center, Quaternion rotation, float radius, float height,int discSegments = 20) {

                if (rotation.Equals(default)) {
                    rotation = Quaternion.identity;
                }

                var matrix = Matrix4x4.TRS(center, rotation, Vector3.one);
                using (new GizmoUtil.MatrixScope(matrix)) {

                    var half = height / 2;

                    // Outer lines
                    int outerSegments = 5;
                    DrawWireCylinderOuterLines(
                        type,
                        new DiscParam(radius, outerSegments, half),
                        new DiscParam(radius, outerSegments, -half)
                    );

                    // Disks
                    DrawWireDisc(type, new DiscParam(radius, discSegments, half));
                    DrawWireDisc(type, new DiscParam(radius, discSegments, -half));
                }

            }

            /// <summary>
            /// 円錐を描画する
            /// </summary>
            public static void DrawWireCone(PlaneType type, Vector3 center, Quaternion rotation, float radius, float height,int discSegments = 20) {

                if (rotation.Equals(default)) {
                    rotation = Quaternion.identity;
                }

                var matrix = Matrix4x4.TRS(center, rotation, Vector3.one);
                using (new GizmoUtil.MatrixScope(matrix)) {

                    // Outer lines
                    int outerSegments = 5;
                    DrawWireConeOuterLines(type, new DiscParam(radius, outerSegments), height);

                    // Disks
                    DrawWireDisc(type, new DiscParam(radius, discSegments));
                }
            }
            #endregion


            /// ----------------------------------------------------------------------------
            #region Private Method

            /// <summary>
            /// 円柱の側面部を描画する
            /// </summary>
            private static void DrawWireCylinderOuterLines(PlaneType planeType, DiscParam upperDisc, DiscParam lowerDisc) {
                if (upperDisc.segments != lowerDisc.segments) return;

                // リスト取得
                var upperPoints = ListPool<Vector3>.New();
                var lowerPoints = ListPool<Vector3>.New();

                // 座標計算
                MathUtils.CirclePoints(
                    radius: upperDisc.radius,
                    resultPoints: upperPoints,
                    segments: upperDisc.segments,
                    offset: upperDisc.offset * planeType.GetNormal(),
                    isLoop: false,
                    type: planeType
                );
                MathUtils.CirclePoints(
                    radius: lowerDisc.radius,
                    resultPoints: lowerPoints,
                    segments: lowerDisc.segments,
                    offset: lowerDisc.offset * planeType.GetNormal(),
                    isLoop: false,
                    type: planeType
                );

                // 描画（※上下円の各点を結ぶ線分）
                Basic.DrawLineSet(upperPoints, lowerPoints);

                // リスト解放
                upperPoints.Free();
                lowerPoints.Free();
            }

            /// <summary>
            /// 円錐の側面部を描画する
            /// </summary>
            private static void DrawWireConeOuterLines(PlaneType type, DiscParam lowerDisc, float height) {

                // リスト取得
                var lowerPoints = ListPool<Vector3>.New();

                var top = (lowerDisc.offset + height) * type.GetNormal();
                MathUtils.CirclePoints(
                    radius: lowerDisc.radius,
                    resultPoints: lowerPoints,
                    segments: lowerDisc.segments,
                    offset: lowerDisc.offset * type.GetNormal(),
                    isLoop: false,
                    type: type
                );

                // 描画（※底面(円)の各点から頂点への線分）
                lowerPoints.ForEach(p => Gizmos.DrawLine(p, top));

                // リスト解放
                lowerPoints.Free();
            }

            /// <summary>
            /// 円盤を描画する
            /// </summary>
            private static void DrawWireDisc(PlaneType planType, DiscParam disc) {

                // リスト取得
                var points = ListPool<Vector3>.New();

                // 座標計算
                MathUtils.CirclePoints(
                    radius: disc.radius,
                    resultPoints: points,
                    segments: disc.segments,
                    offset: disc.offset * planType.GetNormal(),
                    isLoop: true,
                    type: planType
                );

                // 描画
                Basic.DrawLines(points);

                // リスト解放
                points.Free();
            }
            #endregion
        }

    }
}
