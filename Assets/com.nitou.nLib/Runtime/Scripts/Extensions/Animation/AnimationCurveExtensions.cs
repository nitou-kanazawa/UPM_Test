using UnityEngine;

namespace Nitou {

    /// <summary>
    /// <see cref="AnimationCurve"/>の基本的な拡張メソッド集
    /// </summary>
    public static class AnimationCurveExtensions {

        /// ----------------------------------------------------------------------------
        #region Clamp

        /// <summary>
        /// 値（縦軸）の範囲を制限する拡張メソッド
        /// </summary>
        public static AnimationCurve ClampValue(this AnimationCurve curve, RangeFloat valueRange) {
            var keys = curve.keys;
            if (keys.Length <= 0) return curve;

            // 値の修正
            for (int i = 0; i < keys.Length; i++) {
                keys[i].value = valueRange.Clamp(keys[i].value);
            }

            curve.keys = keys;
            return curve;
        }

        /// <summary>
        /// 値（縦軸）の範囲を制限する拡張メソッド
        /// </summary>
        public static AnimationCurve ClampValue01(this AnimationCurve curve) {
            var keys = curve.keys;
            if (keys.Length <= 0) return curve;

            // 値の修正
            for (int i = 0; i < keys.Length; i++) {
                keys[i].value = Mathf.Clamp01(keys[i].value);
            }

            curve.keys = keys;
            return curve;
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region Normalize

        // [参考]
        //  LIGHT11: 正規化されたAnimationCurveの入力フィールドを表示する https://light11.hatenadiary.com/entry/2019/10/08/012902

        /// <summary>
        /// 時間（横軸）を正規化する拡張メソッド
        /// </summary>
        public static AnimationCurve NormalizeTime(this AnimationCurve curve) {
            var keys = curve.keys;
            if (keys.Length <= 0) return curve;

            var (minTime, maxTime) = keys.MinMax(selector: x => x.time);

            var range = maxTime - minTime;
            var timeScale = (range < 0.0001f) ? 1 : 1 / range;

            // 値の修正
            for (var i = 0; i < keys.Length; ++i) {
                keys[i].time = (keys[i].time - minTime) * timeScale;
            }

            curve.keys = keys;
            return curve;
        }

        /// <summary>
        /// 値（縦軸）を正規化する拡張メソッド
        /// </summary>
        public static AnimationCurve NormalizeValue(this AnimationCurve curve) {
            var keys = curve.keys;
            if (keys.Length <= 0) return curve;

            var (minValue, maxValue) = keys.MinMax(selector: x => x.value);

            var range = maxValue - minValue;
            var valScale = range < 1 ? 1 : 1 / range;
            var valOffset = 0f;
            if (range < 1f) {
                if (minValue > 0f && minValue + range <= 1f) {
                    valOffset = minValue;
                } else {
                    valOffset = 1f - range;
                }
            }

            // 値の修正
            for (var i = 0; i < keys.Length; ++i) {
                keys[i].value = (keys[i].value - minValue) * valScale + valOffset;
            }

            curve.keys = keys;
            return curve;
        }
        #endregion
    }


    // [TODO] いくつかプリセット的なメソッドを整備する (2024.08.01)
    public static class AnimationCurveUtil { }
}
