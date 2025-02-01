using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

// [REF]
//  qiita: パスワードのようなランダムな文字列を生成して返す関数 https://baba-s.hatenablog.com/entry/2015/07/07/000000

namespace Nitou {

    /// <summary>
    /// <see cref="string"/>型の汎用メソッド集．
    /// </summary>
    public static partial class StringUtils {

        /// ----------------------------------------------------------------------------
        #region 文字列への変換

        public static string ToFloatText(this float self) {
            return self.ToString("0.00");
        }

        public static string ToFloatText(this float self, int decimalPlaces = 2) {
            // 小数点以下の桁数に基づいてフォーマット
            string format = "0." + new string('0', decimalPlaces);
            return self.ToString(format);
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region 文字列の生成

        private const string PASSWORD_CHARS = "0123456789abcdefghijklmnopqrstuvwxyz";
        
        /// <summary>
        /// 簡易的なパスワードを生成する．
        /// </summary>
        public static string GeneratePassword(int length) {
            var sb = new System.Text.StringBuilder(length);
            var r = new System.Random();

            for (int i = 0; i < length; i++) {
                int pos = r.Next(PASSWORD_CHARS.Length);
                char c = PASSWORD_CHARS[pos];
                sb.Append(c);
            }

            return sb.ToString();
        }

        #endregion


        /// ----------------------------------------------------------------------------
        #region 文字列の生成
        
        // UniquName

        /// <summary>
        /// コレクション内で重複しない名前を取得する．
        /// </summary>
        public static string GetUniqueName(string baseName, IEnumerable<string> existingNames, Func<string, int, string> selector) {
            string newName = baseName;
            int suffix = 1;

            // 重複がある場合は接尾辞を付けて一意の名前を生成
            while (existingNames.Contains(newName)) {
                newName = selector(baseName, suffix);
                suffix++;
            }

            return newName;
        }

        /// <summary>
        /// コレクション内で重複しない名前を取得する．
        /// </summary>
        public static string GetUniqueName(string baseName, IEnumerable<string> existingNames) {
            return GetUniqueName(baseName, existingNames, (baseName, index) => $"{baseName}({index})");
        }

        /// <summary>
        /// コレクション内で重複しないコピーオブジェクト名を取得する．
        /// </summary>
        public static string GetUniqueCopyDataName(string baseName, IEnumerable<string> existingNames) {
            string newName = GetCopyDataName(baseName);

            // 重複がある場合に一意のコピー名を生成
            while (existingNames.Contains(newName)) {
                newName = GetCopyDataName(newName);
            }

            return newName;
        }


        /// ----------------------------------------------------------------------------
        // 

        /// <summary>
        /// Generates name for copyed instance.
        /// </summary>
        public static string GetCopyDataName(string baseName) {
            // [NOTE]
            //  Generates a unique "copy" name for a given base name by appending "_cpy" or "_cpy(n)" where n is an incremented number.
            //  If the base name already contains "_cpy" or "_cpy(n)", the number n is incremented to ensure uniqueness.

            // pattern : "_cpy" or "_cpy(no)"
            string pattern = @"_cpy(?:\((\d+)\))?$";
            Match match = Regex.Match(baseName, pattern);

            if (match.Success) {
                //  "_cpy(no)" の場合
                if (match.Groups[1].Success && int.TryParse(match.Groups[1].Value, out int number)) {
                    // set incremented number
                    number++;
                    baseName = Regex.Replace(baseName, pattern, $"_cpy({number})");
                }
                // "_cpy" の場合
                else {
                    baseName = baseName + "(1)";
                }
            } else {
                // "_cpy" でない場合、"_cpy" を追加
                baseName += "_cpy";
            }

            return baseName;
        }
        #endregion

    }


    public static class ParseUtils {

        /// ----------------------------------------------------------------------------
        #region 文字列の生成

        public static float FloatOrZero(string text) {
            return float.TryParse(text, out var result) ? result : 0f;
        }

        public static float FloatOrDefault(string text, float value) {
            return float.TryParse(text, out var result) ? result : value;
        }
        #endregion

    }

}