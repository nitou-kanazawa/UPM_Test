using System;
using System.Collections.Generic;
using System.Linq;

// [REF]
//  コガネブログ: 配列やリストの分解代入を可能にする Deconstruct https://baba-s.hatenablog.com/entry/2019/09/12/085000#google_vignette
//  StackOverflow: Does C# 7 have array/enumerable destructuring? https://stackoverflow.com/questions/47815660/does-c-sharp-7-have-array-enumerable-destructuring

namespace Nitou {

    /// <summary>
    /// <see cref="List{T}"/>型の基本的な拡張メソッド集．
    /// </summary>
    public static partial class ListExtensions {

        /// ----------------------------------------------------------------------------
        #region 要素の取得

        /// <summary>
        /// 最後の要素を取り出す拡張メソッド．要素が0なら<see cref="InvalidOperationException">例外</see>を投げる．
        /// </summary>
        public static T PopLast<T>(this IList<T> list) {
            if (list.Count == 0) {
                throw new InvalidOperationException();
            }

            var t = list[list.Count - 1];

            list.RemoveAt(list.Count - 1);

            return t;
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region 要素の削除

        /// <summary>
        /// 該当する要素を全て削除する拡張メソッド．
        /// </summary>
        public static void RemoveAll<T>(this IList<T> self, Func<T, bool> predicate) {
            for (int i = self.Count - 1; i >= 0; i--) {
                if (predicate(self[i])) {
                    self.RemoveAt(i);
                }
            }
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region その他

        /// <summary>
        /// デコンストラクタ．
        /// </summary>
        public static void Deconstruct<T>(this IList<T> self,
            out T first, out IList<T> rest) {
            first = self.Count > 0 ? self[0] : default;
            rest = self.Skip(1).ToArray();
        }

        /// <summary>
        /// デコンストラクタ．
        /// </summary>
        public static void Deconstruct<T>(this IList<T> self,
            out T first, out T second, out IList<T> rest) {
            first = self.Count > 0 ? self[0] : default;
            second = self.Count > 1 ? self[1] : default;
            rest = self.Skip(2).ToArray();
        }

        /// <summary>
        /// デコンストラクタ．
        /// </summary>
        public static void Deconstruct<T>(this IList<T> self,
            out T first, out T second, out T third, out IList<T> rest) {
            first = self.Count > 0 ? self[0] : default;
            second = self.Count > 1 ? self[1] : default;
            third = self.Count > 2 ? self[2] : default;
            rest = self.Skip(3).ToArray();
        }

        /// <summary>
        /// デコンストラクタ．
        /// </summary>
        public static void Deconstruct<T>(this IList<T> self,
            out T first, out T second, out T third, out T four, out IList<T> rest) {
            first = self.Count > 0 ? self[0] : default;
            second = self.Count > 1 ? self[1] : default;
            third = self.Count > 2 ? self[2] : default;
            four = self.Count > 3 ? self[3] : default;
            rest = self.Skip(4).ToArray();
        }

        #endregion

    }
}
