using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

// [参考]
//  コガネブログ: 指定された文字列が電話番号かどうかを返す関数 https://baba-s.hatenablog.com/entry/2014/11/10/110048

namespace Nitou {

    /// <summary>
    /// <see cref="string"/>型の基本的な拡張メソッド集
    /// </summary>
    public static partial class StringExtensions {

        /// ----------------------------------------------------------------------------
        #region 文字列の判定

        /// <summary>
        /// 文字列が Nullもしくは空 かどうかを判定する
        /// </summary>
        public static bool IsNullOrEmpty(this string self) =>
            string.IsNullOrEmpty(self);

        /// <summary>
        /// 文字列が Null/空文字/空白文字 かどうかを判定する
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string self) =>
            string.IsNullOrWhiteSpace(self);


        /// <summary>
        /// 指定されたいずれかの文字列を含むかどうかを判定する
        /// </summary>
        public static bool IncludeAny(this string self, params string[] list) =>
        list.Any(c => self.Contains(c));

        /// <summary>
        /// 文字列が指定されたいずれかの文字列と等しいかどうかを判定する
        /// </summary>
        public static bool IsAny(this string self, params string[] values) =>
            values.Any(c => c == self);


        /// <summary>
        /// 文字列がメールアドレスかどうかを返します
        /// </summary>
        public static bool IsMailAddress(this string self) {
            if (string.IsNullOrEmpty(self)) { return false; }

            return Regex.IsMatch(
                self,
                @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$",
                RegexOptions.IgnoreCase
            );
        }

        /// <summary>
        /// 文字列が電話番号かどうかを判定する
        /// </summary>
        public static bool IsPhoneNumber(this string self) {
            if (string.IsNullOrEmpty(self)) { return false; }

            return Regex.IsMatch(
                self,
                @"^0\d{1,4}-\d{1,4}-\d{4}$"
            );
        }

        /// <summary>
        /// 文字列が郵便番号かどうかを判定する
        /// </summary>
        public static bool IsZipCode(this string self) {
            if (string.IsNullOrEmpty(self)) { return false; }

            return Regex.IsMatch(
                self,
                @"^\d\d\d-\d\d\d\d$",
                RegexOptions.ECMAScript
            );
        }

        /// <summary>
        /// 文字列が URL かどうかを判定する
        /// </summary>
        public static bool IsUrl(this string self) {
            if (string.IsNullOrEmpty(self)) { return false; }

            return Regex.IsMatch(
               self,
               @"^s?https?://[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#]+$"
            );
        }

        // ----- 

        /// <summary>
        /// コレクション要素に重複があるか判定する拡張メソッド
        /// </summary>
        public static bool ContainsDuplicate(this IEnumerable<string> self) =>
            self.GroupBy(i => i).SelectMany(g => g.Skip(1)).Any();

        #endregion


        /// ----------------------------------------------------------------------------
        #region 文字列の修正

        /// <summary>
        /// 文字列から空白文字を削除する
        /// </summary>
        public static string WithoutSpace(this string self) =>
            String.Concat(self.Where(c => !Char.IsWhiteSpace(c)));

        #endregion

    }
}