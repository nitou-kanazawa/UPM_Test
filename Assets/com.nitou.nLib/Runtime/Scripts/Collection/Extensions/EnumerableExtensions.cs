using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;

// [REF]
//  qiita: .NET 9でLINQに追加されたメソッド https://qiita.com/RyotaMurohoshi/items/595b87e1db93768d0d44
// _: IEnumerable.IsNullOrEmpty https://csharpvbcomparer.blogspot.com/2014/04/tips-ienumerable-isnullorempty.html

namespace Nitou {

    /// <summary>
    /// <see cref="IEnumerable"/>型の基本的な拡張メソッド集
    /// </summary>
    public static partial class EnumerableExtensions {

        /// ----------------------------------------------------------------------------
        #region 要素の判定

        /// <summary>
        /// Null,または空かどうか調べる拡張メソッド．
        /// </summary>
        public static bool IsNullOrEmptyEnumerable<T>(this IEnumerable<T> source) {
            return source == null || !source.Any();
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region 要素の取得

        /// <summary>
        /// 指定要素のインデックスを取得する拡張メソッド．シーケンスに含まれない場合は-1を返す．
        /// </summary>
        public static int IndexOf<T>(this IEnumerable<T> source, T value) {
            if (source == null) throw new ArgumentNullException(nameof(source));

            int index = 0;
            var comparer = EqualityComparer<T>.Default;

            foreach (var item in source) {
                if (comparer.Equals(item, value))
                    return index;

                index++;
            }

            return -1; // 見つからない場合
        }

        /// <summary>
        /// 指定要素のインデックスを取得する拡張メソッド．シーケンスに含まれない場合は-1を返す．
        /// </summary>
        public static int IndexOf<T>(this IEnumerable<T> source, Func<T, bool> predicate) {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            int index = 0;
            foreach (var item in source) {
                if (predicate(item))
                    return index;

                index++;
            }

            return -1; // 条件に合致する要素がない場合
        }


        /// <summary>
        /// 条件に基づいて最小の要素を取得する拡張メソッド．
        /// </summary>
        public static T MinBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
            where TKey : IComparable<TKey> {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return source
                .Aggregate((min, current) => selector(current)
                .CompareTo(selector(min)) < 0 ? current : min);
        }

        /// <summary>
        /// 条件に基づいて最大の要素を取得する拡張メソッド．
        /// </summary>
        public static T MaxBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
            where TKey : IComparable<TKey> {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return source
                .Aggregate((max, current) => selector(current)
                .CompareTo(selector(max)) > 0 ? current : max);
        }

        /// <summary>
        /// 最大値と最小値を取得する拡張メソッド．
        /// </summary>
        public static (T min, T max) MinMax<T>(this IEnumerable<T> source)
            where T : IComparable<T> {
            if (source is null) throw new ArgumentNullException(nameof(source));

            using (var enumerator = source.GetEnumerator()) {
                if (!enumerator.MoveNext()) throw new InvalidOperationException("Sequence contains no elements");

                // 初期値
                T min = enumerator.Current;
                T max = enumerator.Current;

                while (enumerator.MoveNext()) {
                    if (enumerator.Current.CompareTo(max) > 0) {
                        max = enumerator.Current;
                    }
                    if (enumerator.Current.CompareTo(min) < 0) {
                        min = enumerator.Current;
                    }
                }

                return (min, max);
            }
        }

        /// <summary>
        /// 最大値と最小値を取得する拡張メソッド．
        /// </summary>
        public static (TResult min, TResult max) MinMax<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
            where TResult : IComparable<TResult> {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (selector is null) throw new ArgumentNullException(nameof(selector));

            using (var enumerator = source.GetEnumerator()) {
                if (!enumerator.MoveNext()) throw new InvalidOperationException("Sequence contains no elements");

                // 初期値
                TResult minValue = selector(enumerator.Current);
                TResult maxValue = selector(enumerator.Current);

                while (enumerator.MoveNext()) {
                    TResult currentValue = selector(enumerator.Current);

                    if (currentValue.CompareTo(maxValue) > 0) {
                        maxValue = currentValue;
                    }
                    if (currentValue.CompareTo(minValue) < 0) {
                        minValue = currentValue;
                    }
                }

                return (minValue, maxValue);
            }
        }

        /// <summary>
        /// 最大値と最小値を取得する拡張メソッド．
        /// </summary>
        public static (T min, T max) MinMax<T>(this IEnumerable<T> source, Func<T, T, bool> isGreaterThan) {
            if (source is null) throw new System.ArgumentNullException(nameof(source));
            if (isGreaterThan is null) throw new System.ArgumentNullException(nameof(isGreaterThan));

            using (var enumerator = source.GetEnumerator()) {
                if (!enumerator.MoveNext()) throw new InvalidOperationException("Sequence contains no elements");

                // 初期値
                T min = enumerator.Current;
                T max = enumerator.Current;

                while (enumerator.MoveNext()) {
                    var current = enumerator.Current;

                    if (isGreaterThan(current, max)) {
                        max = current;
                    }
                    if (!isGreaterThan(current, min)) {
                        min = current;
                    }
                }

                return (min, max);
            }
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region 要素の変換

        /// <summary>
        /// nullを除いたシーケンスに変換する拡張メソッド．
        /// </summary>
        public static IEnumerable<T> WithoutNull<T>(this IEnumerable<T> source) where T : class {
            return source.Where(item => item != null);
        }

        /// <summary>
        /// 各要素にインデックスを付与したシーケンスを取得する拡張メソッド．
        /// </summary>
        public static IEnumerable<(T item, int index)> Index<T>(this IEnumerable<T> source) {
            return source.Select((item, index) => (item, index));
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region 要素の走査

        /// <summary>
        /// IEnumerableの各要素に対して、指定された処理を実行する拡張メソッド．
        /// </summary>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T, int> action) {
            foreach ((var item, var index) in source.Index()) {
                action(item, index);
            }
            return source;
        }

        /// <summary>
        /// IEnumerableの各要素に対して、指定された処理を実行する拡張メソッド．
        /// </summary>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action) {
            foreach (var item in source) {
                action(item);
            }
            return source;
        }
        #endregion



        /// ----------------------------------------------------------------------------
        #region  文字列への変換

        /// <summary>
        /// Csv形式の文字列に変換します。
        /// </summary>
        public static string ToCsvText(this IEnumerable<string> source, char separator = ',') {
            if (source == null) return null;

            var csv = new StringBuilder();
            source.ForEach(v => {
                string val = v;
                if (v.Contains("\"") || v.Contains("\n")) {
                    if (v.Contains("\"")) {
                        // ダブルクォートがある場合はダブルクォートを２つに重ねる。(" => "")
                        val = val.Replace("\"", "\"\"");
                    }
                    // ダブルクォートまたは改行がある場合はダブルクォートで囲む。
                    val = $"\"{val}\"";
                }
                csv.AppendFormat("{0}{1}", val, separator);
            });
            return csv.ToString(0, csv.Length - 1);
        }

        /// <summary>
        /// 
        /// </summary>
        public static string ToBracketedString<T>(this IEnumerable<T> source) {
            return $"[{string.Join(", ", source)}]";
        }
        #endregion
    }
}
