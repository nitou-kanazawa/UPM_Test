using UnityEngine;

namespace Nitou {

    /// <summary>
    /// <see cref="CapsuleCollider"/>型の基本的な拡張メソッド集．
    /// </summary>
    public static class CapsuleColliderExtensions {

        /// ----------------------------------------------------------------------------
        // 

        /// <summary>
        /// グローバル座標に変換したコライダー中心座標を取得する拡張メソッド．
        /// </summary>
        public static Vector3 GetWorldCenter(this CapsuleCollider self) {
            return self.transform.TransformPoint(self.center);
        }

        /// <summary>
        /// 親階層を考慮した半径を取得する拡張メソッド．
        /// </summary>
        public static float GetScaledRadius(this CapsuleCollider capsule) {
            return capsule.radius * Mathf.Max(capsule.transform.lossyScale.x, capsule.transform.lossyScale.z);
        }

        /// <summary>
        /// 親階層を考慮した高さを取得する拡張メソッド．
        /// </summary>
        public static float GetScaledHeight(this CapsuleCollider capsule) {
            return capsule.height * capsule.transform.lossyScale.y;
        }


        /// ----------------------------------------------------------------------------

        /// <summary>
        /// 指定座標が<see cref="CapsuleCollider"/>の内部に含まれるか判定する拡張メソッド．
        /// </summary>
        public static bool Contains(this CapsuleCollider capsule, Vector3 point) {

            // 点をローカル座標に変換
            var localPoint = capsule.transform.InverseTransformPoint(point);

            // スケールを考慮したカプセルの半径と高さ
            float radius = capsule.GetScaledRadius();
            float height = capsule.GetScaledHeight();

            // カプセルの中心と軸方向
            Vector3 center = capsule.center;
            Vector3 axis = capsule.GetAxisVector();

            // カプセルの両端の球体の中心を計算
            Vector3 point1 = center - axis * (height * 0.5f - radius);
            Vector3 point2 = center + axis * (height * 0.5f - radius);

            // 点が球体の内部にあるかをチェック
            if (Vector3.Distance(localPoint, point1) <= radius || Vector3.Distance(localPoint, point2) <= radius) {
                return true;
            }

            // 点がシリンダー部分の内部にあるかをチェック
            Vector3 projection = Vector3.Project(localPoint - point1, axis);
            if (projection.magnitude <= (height - radius * 2) && Vector3.Distance(localPoint, point1 + projection) <= radius) {
                return true;
            }

            return false;
        }


        /// ----------------------------------------------------------------------------
        // Public Method (Axis)

        /// <summary>
        /// <see cref="CapsuleCollider"/> の軸を取得する．
        /// </summary>
        public static Axis GetAxis(this CapsuleCollider capsule) {
            return capsule.direction switch {
                0 => Axis.X,
                1 => Axis.Y,
                2 => Axis.Z,
                _ => throw new System.NotImplementedException()
            };
        }

        /// <summary>
        /// <see cref="CapsuleCollider"/> の軸に対応する <see cref="Vector3"/> を取得する．
        /// </summary>
        public static Vector3 GetAxisVector(this CapsuleCollider capsule) {
            return capsule.direction switch {
                0 => capsule.transform.right,
                1 => capsule.transform.up,
                2 => capsule.transform.forward,
                _ => throw new System.NotImplementedException()
            };
        }
    }
}
