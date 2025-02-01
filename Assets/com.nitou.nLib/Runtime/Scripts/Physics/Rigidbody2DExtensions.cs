using UnityEngine;

// [参考]
//  PG日誌: RigidBody2DにAddExplosionForceを追加する https://takap-tech.com/entry/2023/11/29/004251

namespace Nitou {

    /// <summary>
    /// Rigidbody2Dの拡張メソッドクラス
    /// </summary>
    public static partial class Rigidbody2DExtensions {

        /// <summary>
        /// 指定したオブジェクトに爆発する力を加える拡張メソッド
        /// </summary>
        public static void AddExplosionForce(this Rigidbody2D self, float explosionForce,
            in Vector2 explosionPosition, float explosionRadius,
            float upwardsModifier = 0, ForceMode2D mode = ForceMode2D.Force){
            
            Vector2 explosionDirection = self.position - explosionPosition;
            float explosionDistance = explosionDirection.magnitude;

            if (upwardsModifier == 0f) {
                explosionDirection /= explosionDistance;
            } else {
                explosionDirection.y += upwardsModifier;
                explosionDirection.Normalize();
            }

            Vector2 force = Mathf.Lerp(0, explosionForce,
                1.0f - explosionDistance / explosionRadius) * explosionDirection;

            self.AddForce(force, mode);
        }

        /// <summary>
        /// 指定したオブジェクトに爆発する力を加える拡張メソッド
        /// </summary>
        public static void AddExplosionForce(this Rigidbody2D self, float explosionForce,
            in Vector3 explosionPosition, float explosionRadius, float upwardsModifier) {

            AddExplosionForce(self, explosionForce, explosionPosition, explosionRadius, upwardsModifier, ForceMode2D.Force);
        }

        /// <summary>
        /// 指定したオブジェクトに爆発する力を加える拡張メソッド
        /// </summary>
        public static void AddExplosionForce(this Rigidbody2D self, float explosionForce,
            in Vector3 explosionPosition, float explosionRadius) {
            
            AddExplosionForce(self, explosionForce, explosionPosition, explosionRadius, 0);
        }
    }

}
