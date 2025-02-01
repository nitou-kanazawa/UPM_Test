using System;
using System.Linq;
using System.Collections.Generic;

// [参考]
//  kanのメモ帳: enumの汎用的な便利メソッドをまとめた便利クラス https://kan-kikuchi.hatenablog.com/entry/EnumUtility
//  qiita: C# 7.3からGeneric制約にEnumが使えるようなって便利 https://qiita.com/m-otoguro/items/8b9fa888aed0733ca3a1
//  note: Enumを活用する便利関数まとめ https://note.com/projectmeme/n/nbbe8da48ba34

namespace Nitou {

    /// <summary>
    /// <see cref="Enum"/>型に対する汎用メソッド集
    /// </summary>
    public static class EnumUtil {

        /// --------------------------------------------------------------------
        #region 要素の取得

        /// <summary>
        /// 項目数を取得
        /// </summary>
        public static int Count<T>() where T : Enum {
            return Enum.GetValues(typeof(T)).Length;
        }

        /// <summary>
        /// 最初の要素を取得する
        /// </summary>
        public static T GetFirst<T>() where T : Enum {
            return (T)Enum.GetValues(typeof(T)).GetValue(0);
        }

        /// <summary>
        /// 最後の要素を取得する
        /// </summary>
        public static T GetLast<T>() where T : Enum {
            var array = Enum.GetValues(typeof(T));
            return (T)array.GetValue(array.Length - 1);
        }

        /// <summary>
        /// 次の要素を取得する
        /// </summary>
        public static bool TryGetNext<T>(T target, out T next) where T : Enum {
            var values = (T[])Enum.GetValues(typeof(T));
            int index = Array.IndexOf(values, target);

            if (index >= 0 && index < values.Length - 1) {
                next = values[index + 1];
                return true;
            }

            next = default(T);
            return false;
        }

        /// <summary>
        /// 前の要素を取得する
        /// </summary>
        public static bool TryGetPrevious<T>(T target, out T previous) where T : Enum {
            var values = (T[])Enum.GetValues(typeof(T));
            int index = Array.IndexOf(values, target);

            if (index > 0) {
                previous = values[index - 1];
                return true;
            }

            // 最初の要素の場合はリストの最後の要素を返す
            previous = values[values.Length - 1];
            return false;
        }

        /// <summary>
        /// 項目をランダムに一つ取得
        /// </summary>
        public static T GetRandom<T>() where T : Enum {
            int no = UnityEngine.Random.Range(0, Count<T>());
            return NoToType<T>(no);
        }

        /// <summary>
        /// 全ての項目が入ったListを取得
        /// </summary>
        public static T[] GetAllInList<T>() where T : Enum {
            return (T[])Enum.GetValues(typeof(T));
        }
        #endregion


        /// --------------------------------------------------------------------
        #region 要素の変換

        /// <summary>
        /// 入力された文字列と同じ項目を取得
        /// </summary>
        public static T KeyToType<T>(string targetKey) where T : Enum {
            return (T)Enum.Parse(typeof(T), targetKey);
        }

        /// <summary>
        /// 入力された番号の項目を取得
        /// </summary>
        public static T NoToType<T>(int targetNo) where T : Enum {
            if (!Enum.IsDefined(typeof(T), targetNo)) {
                throw new ArgumentOutOfRangeException(nameof(targetNo), $"Invalid enum value: {targetNo}");
            }
            return (T)Enum.ToObject(typeof(T), targetNo);
        }
        #endregion


        /// --------------------------------------------------------------------
        #region 要素の判定

        /// <summary>
        /// 入力された文字列の項目が含まれているか
        /// </summary>
        public static bool ContainsKey<T>(string tagetKey) where T : Enum {
            foreach (T t in Enum.GetValues(typeof(T))) {
                if (t.ToString() == tagetKey) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 最初の要素かどうか
        /// </summary>
        public static bool IsFirst<T>(T target) where T : Enum {
            return target.ToString() == (GetFirst<T>().ToString());
        }

        /// <summary>
        /// 最後の要素かどうか
        /// </summary>
        public static bool IsLast<T>(T target) where T : Enum {
            return target.ToString() == (GetLast<T>().ToString());
        }
        #endregion


        /// --------------------------------------------------------------------
        #region その他

        /// <summary>
        /// 全ての項目に対してデリゲートを実行
        /// </summary>
        public static void ForEach<T>(Action<T> action) where T : Enum {
            foreach (T t in Enum.GetValues(typeof(T))) {
                action(t);
            }
        }
        #endregion
    }
}
