using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// [REF]
//  _: How can I get a Box Collider's "corners" (vertices) positions? https://gamedev.stackexchange.com/questions/128833/how-can-i-get-a-box-colliders-corners-vertices-positions

namespace Nitou {

    /// <summary>
    /// <see cref="BoxCollider"/>型の基本的な拡張メソッド集．
    /// </summary>
    public static partial class BoxColliderExtensions {

        /// ----------------------------------------------------------------------------
        // 

        /// <summary>
        /// グローバル座標に変換したコライダー中心座標を取得する拡張メソッド．
        /// </summary>
        public static Vector3 GetWorldCenter(this BoxCollider box) {
            return box.transform.TransformPoint(box.center);
        }

        /// <summary>
        /// 親階層を考慮したスケールを取得する拡張メソッド．
        /// </summary>
        public static Vector3 GetScaledSize(this BoxCollider box) {
            return Vector3.Scale(box.transform.lossyScale, box.size);
        }


        /// ----------------------------------------------------------------------------


        /// <summary>
        /// 点が<see cref="BoxCollider"/>の内部に含まれるか判定する拡張メソッド．
        /// </summary>
        public static bool Contains(this BoxCollider box, Vector3 point) {

            var localPoint = box.transform.InverseTransformPoint(point);
            var scaledHalfSize = box.GetScaledSize().Half();

            // 全ての軸で境界内にあれば、点はBoxCollider内部にある
            return (Mathf.Abs(localPoint.x) <= scaledHalfSize.x)
                && (Mathf.Abs(localPoint.y) <= scaledHalfSize.y)
                && (Mathf.Abs(localPoint.z) <= scaledHalfSize.z);
        }

        /// <summary>
        /// 球が<see cref="BoxCollider"/>の内部に完全に含まれるか判定する拡張メソッド．
        /// </summary>
        public static bool Contains(BoxCollider box, SphereCollider sphere) {
            float radius = sphere.GetScaledRadius();
            Vector3 center = sphere.transform.TransformPoint(sphere.center);

            // BOX各面との距離で判定
            var planes = box.GetPlanes();
            foreach (var plane in planes) {
                if (plane.GetDistanceToPoint(center) < -radius) {
                    return false;
                }
            }
            return true;
        }


        /// ----------------------------------------------------------------------------
        #region MyRegion

        /// <summary>
        /// 最も長い方向を調べる拡張メソッド．
        /// </summary>
        public static Axis GetLongestAxis(this BoxCollider box) {
            var scale = box.GetScaledSize();

            Axis axis = (scale.x, scale.y, scale.z) switch {
                var (x, y, z) when (x >= y && x >= z) => Axis.X,
                var (x, y, z) when (y >= x && y >= z) => Axis.Y,
                var (x, y, z) when (z >= x && z >= y) => Axis.Z,
                _ => Axis.X // Fallback, though it shouldn't be reached
            };

            return axis;
        }

        /// <summary>
        /// コライダー中心から各軸方向(x,y,z)の境界座標．
        /// </summary>
        public static (Vector3 vx, Vector3 vy, Vector3 vz) GetAxisBoundPoints(this BoxCollider box) {

            var trans = box.transform;
            var scale = box.GetScaledSize().Half();

            return (
                vx: trans.right * scale.x,
                vy: trans.up * scale.y,
                vz: trans.forward * scale.z);
        }

        /// <summary>
        /// コライダー中心から各軸方向(x,y,z)の境界座標．
        /// </summary>
        public static (Vector3 positive, Vector3 negative) GetAxisBoundPoints(this BoxCollider box, Axis axis) {

            var trans = box.transform;
            var scale = box.GetScaledSize().Half();

            return axis switch {
                Axis.X => (trans.right * scale.x, -trans.right * scale.x),
                Axis.Y => (trans.up * scale.y, -trans.up * scale.y),
                Axis.Z => (trans.forward * scale.z, -trans.forward * scale.z),
                _ => throw new System.NotImplementedException()
            };
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region 頂点/線/面の取得

        // [REF]
        //  qiita: UnityのBoxColliderの頂点を取得するスクリプト https://qiita.com/RYUMAGE/items/dae74abaf2b7888dcbfa
        //  qiita: BoxColliderの各面をPlaneとして取得するスクリプト https://qiita.com/RYUMAGE/items/8fc826825ef27e178225

        /// <summary>
        /// <see cref="BoxCollider"/> の８頂点のワールド座標を取得する拡張メソッド．
        /// </summary>
        public static Vector3[] GetVertices(this BoxCollider box) {

            var center = box.transform.TransformPoint(box.center);

            (var vx, var vy, var vz) = box.GetAxisBoundPoints();
            var p1 = -vx + vy + vz;
            var p2 = vx + vy + vz;
            var p3 = vx + -vy + vz;
            var p4 = -vx + -vy + vz;

            var vertices = new Vector3[8];
            vertices[0] = center + p1;
            vertices[1] = center + p2;
            vertices[2] = center + p3;
            vertices[3] = center + p4;

            vertices[4] = center - p1;
            vertices[5] = center - p2;
            vertices[6] = center - p3;
            vertices[7] = center - p4;

            return vertices;
        }

        /// <summary>
        /// <see cref="BoxCollider"/> の6面を取得する拡張メソッド．
        /// </summary>
        public static Plane[] GetPlanes(this BoxCollider box) {

            var trans = box.transform;
            var center = trans.TransformPoint(box.center);

            (var vx, var vy, var vz) = box.GetAxisBoundPoints();

            var planes = new Plane[6];
            planes[0] = new Plane(trans.right, center + vx);
            planes[1] = new Plane(-trans.right, center - vx);
            planes[2] = new Plane(trans.up, center + vy);
            planes[3] = new Plane(-trans.up, center - vy);
            planes[4] = new Plane(trans.forward, center + vz);
            planes[5] = new Plane(-trans.forward, center - vz);

            return planes;
        }

        /// <summary>
        /// <see cref="BoxCollider"/> の線分を取得する拡張メソッド．
        /// </summary>
        public static LineSegment3[] GetLines(this BoxCollider box, Axis axis) {

            var vertices = box.GetVertices();
            var lines = new List<LineSegment3>();

            // 組み合わせの配列 (※GetVertices()の要素順に基づく)
            (int i, int j)[] indexPairs = axis switch {
                Axis.X => new (int, int)[] { (0, 1), (3, 2), (5, 4), (6, 7) },
                Axis.Y => new (int, int)[] { (2, 1), (3, 0), (4, 7), (5, 6) },
                Axis.Z => new (int, int)[] { (6, 0), (7, 1), (4, 2), (5, 3) },
                _ => throw new System.NotImplementedException()
            };

            // LineSeqment3に変換
            return indexPairs
                .Select(index => new LineSegment3(vertices[index.i], vertices[index.j]))
                .ToArray();
        }
        #endregion


        /// ----------------------------------------------------------------------------
        // 頂点 / 線 / 面の取得

        /// <summary>
        /// Gets the closest plane to a given point.
        /// </summary>
        public static Plane GetClosestPlane(this BoxCollider box, Vector3 point) {
            var planes = box.GetPlanes();

            float minDistance = float.MaxValue;
            Plane closestPlane = planes[0];

            foreach (var plane in planes) {
                float distance = Mathf.Abs(plane.GetDistanceToPoint(point));
                if (distance < minDistance) {
                    minDistance = distance;
                    closestPlane = plane;
                }
            }
            return closestPlane;
        }
    }
}
