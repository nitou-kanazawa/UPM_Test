using UnityEngine;

namespace Nitou {

    /// <summary>
    /// 角度計算の汎用メソッドを提供する静的クラス
    /// </summary>
    public static class AngleUtils {

        // 2つの2Dベクトルの間の角度を計算（度で返す）
        public static float AngleBetweenVectors(Vector2 from, Vector2 to) {
            return Vector2.SignedAngle(from, to);
        }

        /// <summary>
        /// 角度を 0度〜360度 の範囲で正規化 ．
        /// </summary>
        public static float NormalizeAngle360(float angle) {
            return Mathf.Repeat(angle, 360f);
        }

        /// <summary>
        /// 角度を 0度〜360度 の範囲で正規化 ．
        /// </summary>
        public static float NormalizeAngle180(float angle) {
            // 角度を -180度～180度の範囲に正規化
            angle = Mathf.Repeat(angle + 180f, 360f) - 180f;
            return angle;
        }

    }
}
