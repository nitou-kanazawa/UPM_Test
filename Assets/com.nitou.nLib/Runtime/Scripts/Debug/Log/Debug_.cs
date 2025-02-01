using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using UnityEngine;

// [REF]
//  qiita: UnityEditorの時のみDebug.Logを出す方法 https://qiita.com/toRisouP/items/d856d65dcc44916c487d
//  zonn: Debug.Logを便利にするために工夫していること https://zenn.dev/happy_elements/articles/38be21755773e0
//  _: Color型変数をもとにDebug.Logの文字色を変更する https://nmxi.hateblo.jp/entry/2019/02/24/235216
//  kanのメモ帳: ConditionalAttributeで複数のシンボルのANDやORを実装する方法 https://kan-kikuchi.hatenablog.com/entry/ConditionalAttribute_AND_OR
//  kanのメモ帳: 開発用ビルド時に有効になるDEVELOPMENT_BUILDとDEBUGの違い https://kan-kikuchi.hatenablog.com/entry/DEVELOPMENT_BUILD_DEBUG

namespace Nitou {
    using Nitou.RichText;
    using System.Text.RegularExpressions;
    using Debug = UnityEngine.Debug;

    /// <summary>
    /// Debugのラッパークラス
    /// </summary>
    public static partial class Debug_ {

        /// ----------------------------------------------------------------------------
        #region Public Method (基本ログ)

        /// <summary>
        /// UnityEditor上でのみ実行されるLogメソッド
        /// </summary>
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Log(object o) => Debug.Log(FormatObject(o));

        /// <summary>
        /// UnityEditor上でのみ実行されるLogメソッド
        /// </summary>
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Log(object o, Color color) => Debug.Log(FormatObject(o).WithColorTag(color));

        /// <summary>
        /// UnityEditor上でのみ実行されるLogメソッド
        /// </summary>
        public static void Log(params object[] messages) {
            var message = string.Join(',', messages.Select(FormatObject));
            Debug.Log(message);
        }

        /// <summary>
        /// UnityEditor上でのみ実行されるLogWarningメソッド
        /// </summary>
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void LogWarning(object o) => Debug.LogWarning(FormatObject(o));

        /// <summary>
        /// UnityEditor上でのみ実行されるLogWarningメソッド
        /// </summary>
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void LogWarning(object o, Color color) => Debug.LogWarning(FormatObject(o).WithColorTag(color));

        /// <summary>
        /// UnityEditor上でのみ実行されるLogErrorメソッド
        /// </summary>
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void LogError(object o) => Debug.LogError(o);

        /// <summary>
        /// UnityEditor上でのみ実行されるLogWarningメソッド
        /// </summary>
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void LogError(object o, Color color) => Debug.LogError(FormatObject(o).WithColorTag(color));

        #endregion


        /// ----------------------------------------------------------------------------
        #region Public Method (コレクション)

        private static readonly int MAX_ROW_NUM = 100;

        /// <summary>
        /// UnityEditor上でのみ実行されるLogメソッド
        /// </summary>
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void ListLog<T>(IReadOnlyList<T> list) => Log(list.Convert<T>());

        /// <summary>
        /// UnityEditor上でのみ実行されるLogメソッド
        /// </summary>
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void ListLog<T>(IReadOnlyList<T> list, Color color) => Log(list.Convert<T>(), color);

        /// <summary>
        /// UnityEditor上でのみ実行されるLogメソッド
        /// </summary>
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void DictLog<TKey, TValue>(IReadOnlyDictionary<TKey, TValue> dict) => Log(dict.Convert<TKey, TValue>());

        #endregion


        /// ----------------------------------------------------------------------------
        #region Private Method (変換メソッド)

        /// <summary>
        /// 文字列への変換（※null，空文字が判別できる形式）
        /// </summary>
        private static string FormatObject(object o) {
            if (o is null) {
                return "(null)";
            } 
            if (o as string == string.Empty) {
                return "(empty)";
            }
            return o.ToString();
        }

        /// <summary>
        /// リスト要素を文字列に変換する
        /// </summary>
        private static string Convert<T>(this IReadOnlyList<T> list) {
            if (list == null) return "(null)";

            var sb = new StringBuilder();
            sb.Append($"(The total number of elements is {list.Count})\n");

            // 文字列へ変換
            for (int index = 0; index < list.Count; index++) {
                // 最大行数を超えた場合，
                if (index >= MAX_ROW_NUM) {
                    sb.Append($"(+{list.Count - MAX_ROW_NUM} items has been omitted)");
                    break;
                }
                // 要素追加
                var rowText = $"[ {index} ] = {list[index]}";
#if UNITY_2022_1_OR_NEWER
                sb.Append($"{rowText.WithIndentTag()} \n");
#else
                sb.Append($"    {rowText} \n");
#endif
            }
            return sb.ToString();
        }

        /// <summary>
        /// ディクショナリ要素を文字列に変換する
        /// </summary>
        private static string Convert<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dict) {
            if (dict == null) return "(null)";

            var sb = new StringBuilder();
            sb.Append($"(The total number of elements is {dict.Count})\n");

            // 文字列へ変換
            int index = 0;
            foreach ((var key, var value) in dict) {
                // 最大行数を超えた場合，
                if (index >= MAX_ROW_NUM) {
                    sb.Append($"(+{dict.Count - MAX_ROW_NUM} items has been omitted)");
                    break;
                }

                // 要素追加
                var rowText = $"[ {key} ] = {value}";
#if UNITY_2022_1_OR_NEWER
                sb.Append($"{rowText.WithIndentTag()} \n");
#else
                sb.Append($"    {rowText} \n");
#endif
                index++;
            }
            return sb.ToString();
        }


        static string DecorateColorTag(object message) {
            var sb = new StringBuilder();
            var messageString = message.ToString();
            sb.Append(messageString);
            var reg = new Regex("\\[(?<tag>.*?)\\]", RegexOptions.IgnoreCase | RegexOptions.Singleline);

            var matches = reg.Matches(messageString);
            foreach (var match in matches.Reverse()) {
                var tag = match.Groups["tag"].Value;
                // HashCodeをもとに何らかの色を取得
                var color = Colors.SelectFromManyColors(tag.GetHashCode());
                sb.Insert(match.Index + match.Length, "</color></b>");
                sb.Insert(match.Index, $"<b><color={Colors.ToRgbCode(color)}>");
            }

            return sb.ToString();
        }

        #endregion

    }

}
