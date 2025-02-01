using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// [参考]
//  PG日誌: 基本型に範囲チェック機能を追加する https://takap-tech.com/entry/2020/06/20/232208#IsInRangemin-max%E5%A4%89%E6%95%B0%E5%80%A4%E3%81%8C-min--max-%E3%81%AE%E7%AF%84%E5%9B%B2%E5%86%85%E3%81%8B%E7%A2%BA%E8%AA%8D%E3%81%99%E3%82%8B

namespace Nitou {

    /// <summary>
    /// Genericの基本的な拡張メソッド集
    /// </summary>
    public static class GenericExtensions {

        /// ----------------------------------------------------------------------------
        #region 値の判定

        /// <summary>
        /// 値が範囲内にあるかどうかを判定する拡張メソッド
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInRange<T>(this T value, T min, T max) where T : IComparable {
            bool isBetween = (0 <= value.CompareTo(min) && value.CompareTo(max) <= 0);
            return isBetween;
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region 値の取得

        /// <summary>
        /// 小さい方の値を取得する拡張メソッド
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetSmaller<T>(this T value, T min) where T : IComparable<T> {
            return (value.CompareTo(min) < 0) ? min : value;
        }

        /// <summary>
        /// 大きい方の値を取得する拡張メソッド
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetLarger<T>(this T value, T max) where T : IComparable<T> {
            return (value.CompareTo(max) > 0) ? max : value;
        }
        #endregion
    }

}
