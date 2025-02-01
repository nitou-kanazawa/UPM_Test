using System.Runtime.CompilerServices;
using UnityEngine;

// [REF]
//  ホトトギス通信: UnityEngine.MathfとSystem.Mathどっちを使うのが良い？という話 https://shibuya24.info/entry/unity-csharp-mathf

namespace Nitou {

    /// <summary>
    /// <see cref="float"/>型の基本的な拡張メソッド集．
    /// </summary>
    public static partial class FloatExtensions {

        /// ----------------------------------------------------------------------------
        #region 値の判定

        /// <summary>
        /// 正の値かどうかを判定する拡張メソッド．
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPositive(this float self) =>
            self > 0;

        /// <summary>
        /// 負の値かどうかを判定する拡張メソッド．
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNegative(this float self) =>
            self < 0;

        public static bool IsOver(this float self, float value) =>
            self > value;

        /// <summary>
        /// 値が０か判定する拡張メソッド．
        /// </summary>
        public static bool IsZero(this float self) =>
            Mathf.Approximately(self, 0f);

        #endregion


        /// ----------------------------------------------------------------------------
        #region 値の補正

        /// <summary>
        /// 正の値にする拡張メソッド．
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Positive(this float self) =>
            Mathf.Abs(self);

        /// <summary>
        /// 負の値にする拡張メソッド．
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Negative(this float self) =>
            Mathf.Abs(self) * (-1);

        /// <summary>
        /// 指定範囲内の値に制限する拡張メソッド．
        /// </summary>
        public static float Clamp(this float self, float min, float max) =>
            Mathf.Clamp(self, min, max);

        /// <summary>
        /// 指定範囲内の値に制限する拡張メソッド．
        /// </summary>
        public static float Clamp01(this float self) =>
            Mathf.Clamp01(self);

        /// <summary>
        /// 値を切り捨ててInt型で返す拡張メソッド．
        /// </summary>
        public static int FloorToInt(this float self) =>
            Mathf.FloorToInt(self);

        /// <summary>
        /// 値を丸めてInt型で返す拡張メソッド．
        /// </summary>
        public static int RoundToInt(this float self) =>
            Mathf.RoundToInt(self);

        // [NOTE]
        //  _: 値を特定の範囲に収める拡張メソッド https://12px.com/blog/2023/01/remap/

        /// <summary>
        /// 半分の値を返す拡張メソッド．
        /// </summary>
        public static float Remap(this float value,
            float fromMin, float fromMax,
            float toMin, float toMax,
            bool clamp = true) {
            var val = (value - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;
            return clamp ? Mathf.Clamp(val, toMin, toMax) : val;
        }

        #endregion


        /// ----------------------------------------------------------------------------
        #region 簡易計算

        /// <summary>
        /// 半分の値を返す拡張メソッド．
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Half(this float self) => self * 0.5f;

        /// <summary>
        /// ２倍の値を返す拡張メソッド．
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Twice(this float self) => self * 2f;
        #endregion
    }
}