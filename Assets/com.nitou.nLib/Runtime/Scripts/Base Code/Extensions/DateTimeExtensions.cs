using System;

// [REF]
//  コガネブログ: DateTime を代入する時の記述を簡略化する Deconstruction https://baba-s.hatenablog.com/entry/2019/09/03/230400

namespace Nitou
{
    /// <summary>
    /// <see cref="DateTime"/>型に対する汎用的な拡張メソッド集．
    /// </summary>
    public static partial class DateTimeExtensions {

        /// <summary>
        /// デコンストラクタ．
        /// </summary>
        public static void Deconstruct(this DateTime self, out int year, out int month, out int day) {
            year = self.Year;
            month = self.Month;
            day = self.Day;
        }
    }
}
