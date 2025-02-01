using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

// [REF]
//  Hatena Blog: Action, Func, Predicateデリゲートを使ってみた https://oooomincrypto.hatenadiary.jp/entry/2022/04/24/201149
//  JojoBase: 拡張メソッドは作って貯めておくと便利です https://johobase.com/custom-extension-methods-list/#i-5
//  JojoBase: コレクションの拡張メソッド Collection Extensions https://johobase.com/collection-extensions-methods-list/
//  qiita: あるとちょっと便利な拡張メソッド紹介 https://qiita.com/s_mino_ri/items/0fd2e2b3cebb7a62ad46

namespace Nitou {

    /// <summary>
    /// Collectionの基本的な拡張メソッド集．
    /// </summary>
    public static partial class CollectionExtensions {

        /// ----------------------------------------------------------------------------
        #region 要素の判定

        /// <summary>
        /// コレクションが空かどうかを判定する拡張メソッド．
        /// </summary>
        public static bool IsEmpty(this ICollection self) {
            return self.Count == 0;
        }

        /// <summary>
        /// コレクションがNullまたは空かどうかを判定する拡張メソッド．
        /// </summary>
        public static bool IsNullOrEmpty(this ICollection self) {
            return self == null || self.Count == 0;
        }

        /// <summary>
        /// 指定した要素が全てコレクション内にあるかどうかを判定する拡張メソッド．
        /// </summary>
        public static bool ContainsAll<T>(this ICollection<T> self, params T[] items) {
            foreach (T item in items) {
                if (!self.Contains(item)) {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 複数の要素のいずれかがコレクションに格納されているかどうかを判定する拡張メソッド．
        /// </summary>
        public static bool ContainsAny<T>(this ICollection<T> self, params T[] items) {
            foreach (T item in items) {
                if (self.Contains(item)) {
                    return true;
                }
            }
            return false;
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region 要素数の判定

        /// <summary>
        /// 指定インデックスが範囲内にあるかどうかを判定する拡張メソッド．
        /// </summary>
        public static bool IsInRange<T>(this int index, IReadOnlyCollection<T> collection) {
            return 0 <= index && index < collection.Count;
        }

        /// <summary>
        /// 指定インデックスが範囲外にあるかどうかを判定する拡張メソッド．
        /// </summary>
        public static bool IsOutRange<T>(this int index, IReadOnlyCollection<T> collection) {
            return !index.IsInRange(collection);
        }

        #endregion


        /// ----------------------------------------------------------------------------
        #region 要素の追加

        /// <summary>
        /// 指定した処理条件を満たす場合に要素を追加する拡張メソッド．
        /// </summary>
        public static bool AddIf<T>(this ICollection<T> self, Predicate<T> predicate, T item) {
            if (predicate(item)) {
                self.Add(item);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 要素がNullの場合にコレクションに追加する拡張メソッド．
        /// </summary>
        public static bool AddIfNotNull<T>(this ICollection<T> self, T item) where T : class {
            if (item != null) {
                self.Add(item);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 要素がコレクション内に含まれなければ追加する拡張メソッド．
        /// </summary>
        public static bool AddIfNotContains<T>(this ICollection<T> self, T item) {
            if (!self.Contains(item)) {
                self.Add(item);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 要素群を追加する拡張メソッド．
        /// </summary>
        public static void AddRange<T>(this ICollection<T> self, params T[] items) {
            foreach (var item in items) {
                self.Add(item);
            }
        }

        /// <summary>
        /// 指定コレクションから存在しない要素を追加する拡張メソッド.
        /// </summary>
        public static void AddRangeIf<T>(this ICollection<T> self, Predicate<T> predicate, params T[] items) {
            foreach (var item in items) {
                self.AddIf(predicate, item);
            }
        }

        /// <summary>
        /// 要素がコレクション内に含まれなければ追加する拡張メソッド．
        /// </summary>
        public static void AddRangeIf<T>(this ICollection<T> self, Predicate<T> predicate, IEnumerable<T> items) {
            foreach (var item in items) {
                self.AddIf(predicate, item);
            }
        }

        /// <summary>
        /// 要素がコレクション内に含まれなければ追加する拡張メソッド．
        /// </summary>
        public static void AddRangeIfNotContains<T>(this ICollection<T> self, params T[] items) {
            foreach (var item in items) {
                self.AddIfNotContains(item);
            }
        }

        /// <summary>
        /// 要素がコレクション内に含まれなければ追加する拡張メソッド．
        /// </summary>
        public static void AddRangeIfNotContains<T>(this ICollection<T> self, IEnumerable<T> items) {
            foreach (var item in items) {
                self.AddIfNotContains(item);
            }
        }

        #endregion


        /// ----------------------------------------------------------------------------
        #region 要素の削除

        /// <summary>
        /// 指定した処理条件を満たす場合に要素を削除する拡張メソッド．
        /// </summary>
        public static void RemoveIf<T>(this ICollection<T> self, Predicate<T> predicate, T item) {
            if (predicate(item)) {
                self.Remove(item);
            }
        }

        /// <summary>
        /// 複数の要素を削除する拡張メソッド．
        /// </summary>
        public static void RemoveRange<T>(this ICollection<T> self, params T[] items) {
            foreach (T item in items) {
                self.Remove(item);
            }
        }

        /// <summary>
        /// 複数の要素のそれぞれに対して指定した条件を満たす場合に削除する拡張メソッド．
        /// </summary>
        public static void RemoveRangeIf<T>(this ICollection<T> self, Predicate<T> predicate, params T[] items) {
            foreach (T item in items) {
                if (predicate(item)) {
                    self.Remove(item);
                }
            }
        }

        #endregion
    }
}