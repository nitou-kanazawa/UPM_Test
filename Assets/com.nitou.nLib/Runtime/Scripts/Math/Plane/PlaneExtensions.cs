using UnityEngine;

// [REF]
//  Document: Plane https://docs.unity3d.com/ja/2023.2/ScriptReference/Plane.html
//  ねこじゃらシティ: 平面の計算を楽できるPlane構造体の使い方 https://nekojara.city/unity-plane-struct

namespace Nitou {

    /// <summary>
    /// 平面．
    /// </summary>
    public enum PlaneType {
        XY,
        YZ,
        ZX,
    }

    /// <summary>
    /// <see cref="Plane"/>型の基本的な拡張メソッド集．
    /// </summary>
    public static class PlaneExtensions {

        /// <summary>
        /// 
        /// </summary>
        public static Vector3 GetPosition(this Plane self) {
            return self.normal * self.distance;
        }

        /// <summary>
        /// オーバーラップしているか判定する拡張メソッド．
        /// </summary>
        public static bool IsOverlapping(this Plane self, Vector3 position, float radius) {
            if (radius <= 0f) throw new System.InvalidOperationException("radius must be greater than 0.");

            // ※Plane.GetDistanceToPointは符号付き距離を返すので，２乗で比較
            return Mathf.Pow(self.GetDistanceToPoint(position), 2) <= Mathf.Pow(radius, 2);
        }

        /// <summary>
        /// ギズモを表示する．
        /// </summary>
        public static void DrawGizmo(this Plane self, Color color) {
            Gizmos_.DrawSphere(self.GetPosition(), 0.1f, color);
            Gizmos_.DrawLineArrow(self.GetPosition(), self.normal, color);
        }

    }


    public static class PlaneUtil {

        /// <summary>
        /// 平面に対応した法線ベクトルを取得する．
        /// </summary>
        public static Vector3 GetNormal(this PlaneType type) {
            return type switch {
                PlaneType.XY => Vector3.forward,
                PlaneType.YZ => Vector3.right,
                PlaneType.ZX => Vector3.up,
                _ => throw new System.NotImplementedException()
            };
        }

    }

}
