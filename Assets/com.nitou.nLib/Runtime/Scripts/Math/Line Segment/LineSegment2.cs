using System.Linq;
using UnityEngine;

// [REF]
//  ねこじゃらシティ: 線分同士の交差判定 https://nekojara.city/unity-line-segment-cross
//  qiita: ２D線分計算Class https://qiita.com/RYUMAGE/items/a00cdc92e65116f23183

namespace Nitou {

    /// <summary>
    /// 線分を表す構造体．
    /// </summary>
    [System.Serializable]
    public struct LineSegment2 {

        public Vector2 start;
        public Vector2 end;


        /// ----------------------------------------------------------------------------
        // Property

        /// <summary>
        /// 中点．
        /// </summary>
        public Vector2 Center => (start + end) * 0.5f;

        /// <summary>
        /// 方向ベクトル．
        /// </summary>
        public Vector2 Vector => end - start;

        /// <summary>
        /// 法線ベクトル．
        /// </summary>
        public Vector2 Normal => Vector2.Perpendicular(Vector);


        /// ----------------------------------------------------------------------------
        // Public Method (基本メソッド)

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public LineSegment2(Vector2 start, Vector2 end) {
            this.start = start;
            this.end = end;
        }

        /// <summary>
        /// 複製．
        /// </summary>
        public LineSegment2 Clone() {
            return new LineSegment2(start, end);
        }

        /// <summary>
        /// 方向ベクトル．
        /// </summary>
        public float Distance() {
            return Vector2.Distance(start, end);
        }

        /// <summary>
        /// パラメータ（0~1）を指定して線分上の点を取得する．
        /// </summary>
        public Vector2 GetPoint(float t) {
            return Vector2.Lerp(start, end, t);
        }

        /// <summary>
        /// 分割数を指定して、線分上の点列を取得する．
        /// </summary>
        public Vector2[] GetPoints(int num) {
            if (num <= 0) throw new System.InvalidOperationException("The number of divisions must be a positive integer greater than zero.");
            
            return EnumerableUtils.Linspace(start,end,num).ToArray();
        }

        /// <summary>
        /// ２つの線分が並行か判定する．
        /// </summary>
        public bool IsParallel(LineSegment2 other) {
            float cross = Cross(this.Vector, other.Vector);
            return Mathf.Abs(cross) < Vector2.kEpsilon;
        }

        /// <summary>
        /// ２つの線分が交差しているかを判定する．
        /// </summary>
        public bool IsCrossing(LineSegment2 other) {
            Vector2 v1 = this.start - other.start;
            Vector2 v2 = this.end - other.start;
            Vector2 v3 = other.start - this.start;
            Vector2 v4 = other.end - this.start;
            return Cross(other.Vector, v1) * Cross(other.Vector, v2) < 0 &&
                Cross(this.Vector, v3) * Cross(this.Vector, v4) < 0;
        }

        /// <summary>
        /// ２つの線分の交差点を計算する．
        /// </summary>
        public Vector2 CrossPoint(LineSegment2 other) {
            Vector2 v1 = other.start - this.start;
            float nume = Cross(v1, other.Vector);
            float deno = Cross(Vector, other.Vector);

            // 交差していない場合，
            if (Mathf.Abs(deno) < Mathf.Epsilon) {
                Debug_.LogWarning("Cross point is not exist.");
                return this.start;  // ※自分の開始点を返す
            }

            float t = nume / deno;
            return this.start + Vector * t;
        }

        /// <summary>
        /// 最近傍点を計算する．
        /// </summary>
        public Vector2 GetNearestPoint(Vector2 point) {
            Vector2 v1 = point - start;
            float dot = Vector2.Dot(Vector, v1);

            Vector2 p = start + Vector * dot / Vector.sqrMagnitude;
            if (Vector2.Dot(p - start, Vector) < 0) {
                p = start;
            } else if (Vector2.Distance(start, p) > Distance()) {
                p = end;
            }

            return p;
        }

        /// <summary>
        /// 点との最短距離．
        /// </summary>
        public float DistanceFromPoint(Vector2 point) {
            return Vector2.Distance(GetNearestPoint(point), point);
        }


        /// ----------------------------------------------------------------------------
        // Static Method

        /// <summary>
        /// 外積．
        /// </summary>
        public static float Cross(Vector2 vec1, Vector2 vec2) {
            return vec1.x * vec2.y - vec2.x * vec1.y;
        }        
    }
}
