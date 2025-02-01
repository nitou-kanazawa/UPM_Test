using System.Runtime.CompilerServices;
using UnityEngine;

// [参考]
//  qiita: C#で数値をカンマ付き文字列に変換する方法 https://qiita.com/benjamin1gou/items/fd95dc47bc31ec734b83

namespace Nitou {

    /// <summary>
    /// <see cref="int"/>型の基本的な拡張メソッド集．
    /// </summary>
    public static partial class IntExtensions {

        /// ----------------------------------------------------------------------------
        #region 値の判定

        /// <summary>
        /// 偶数かどうかを判定する拡張メソッド．
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEven(this int self) => 
            (self % 2) == 0;

        /// <summary>
        /// 奇数かどうかを判定する拡張メソッド．
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOdd(this int self) => 
            (self % 2) != 0;

        /// <summary>
        /// 正の値かどうかを判定する拡張メソッド．
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPositive(this int self) =>
            self > 0;

        /// <summary>
        /// 負の値かどうかを判定する拡張メソッド．
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNegative(this int self) =>
            self < 0;


        /// <summary>
        /// 指定された範囲内に収まっているかどうかを判定する拡張メソッド．
        /// </summary>
        public static bool IsInRange(this int self, int min, int max) =>
            min <= self && self <= max;

        #endregion


        /// ----------------------------------------------------------------------------
        #region 値の補正

        /// <summary>
        /// 正の値にする拡張メソッド．
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Positive(this int self) => 
            Mathf.Abs(self);

        /// <summary>
        /// 負の値にする拡張メソッド．
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Negative(this int self) => 
            Mathf.Abs(self) * (-1);

        /// <summary>
        /// 入力を指定範囲内の値に制限する拡張メソッド．
        /// </summary>
        public static int Clamp(this int self, int min, int max) => 
            Mathf.Clamp(self, min, max);

        /// <summary>
        /// 数値を加算して、範囲を超えた分は min からの値として処理して返す拡張メソッド．
        /// </summary>
        public static int Repeat(this int self, int min, int max) {
            if (min >= max)
                throw new System.ArgumentException("min must be less than max");

            return (self - min + (max - min)) % (max - min) + min;
        }

        #endregion


        /// ----------------------------------------------------------------------------
        #region 文字列への変換

        /// <summary>
        /// 指定された桁数で0埋めした文字列を返す拡張メソッド．
        /// </summary>
        public static string ToString_ZeroFill(this int self, int numberOfDigits) =>
            self.ToString("D" + numberOfDigits);

        /// <summary>
        /// 指定された桁数の固定小数点数を付加した文字列を返す拡張メソッド．
        /// </summary>
        /// <remarks>
        /// 123.FixedPoint(2) → "123.00"
        /// 123.FixedPoint(4) → "123.0000"
        /// </remarks>
        public static string ToString_FixedPoint(this int self, int numberOfDigits) =>
            self.ToString("F" + numberOfDigits);

        /// <summary>
        /// 指定された　　拡張メソッド．
        /// </summary>
        public static string ToString_Separate(this int self) =>
            string.Format("{0:#,0}", self);

        /// <summary>
        /// カンマ付き文字列を返す拡張メソッド．
        /// </summary>
        /// <remarks>
        /// 1000000 → "1,000,000"
        /// </remarks>
        public static string ToString_WithComma(this int self) =>
            self.ToString("N0");
        #endregion
    }
}