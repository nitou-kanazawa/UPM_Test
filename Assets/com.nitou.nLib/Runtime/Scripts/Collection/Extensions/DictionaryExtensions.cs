using System;
using System.Linq;
using System.Collections.Generic;

// [REF]
//  qiita: Dictionaryの拡張メソッド 36選 https://qiita.com/soi/items/6ce0e0ddefdd062c026a
//  コガネブログ: Dictionaryをforeachで使う時の記述を簡略化するDeconstruction https://baba-s.hatenablog.com/entry/2019/09/03/231000

namespace Nitou {

    /// <summary>
    /// <see cref="Dictionary{TKey, TValue}"/>型の基本的な拡張メソッド集
    /// </summary>
    public static partial class DictionaryExtensions {

        /// ----------------------------------------------------------------------------
        #region 要素の取得

        /// <summary>
        /// 指定したキー格納されいる場合はその値，なければデフォルト値を取得する．
        /// </summary>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key) {
            return self.TryGetValue(key, out TValue result) ? result : default;
        }

        /// <summary>
        /// 
        /// </summary>
        public static TValue GetValueOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value) {
            dict.TryAdd(key, value);
            return dict[key];
        }

        /// <summary>
        /// 
        /// </summary>
        public static TValue GetValueOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, Func<TKey, TValue> valueFactory) {
            dict.TryAdd(key, valueFactory);
            return dict[key];
        }

        /// <summary>
        /// 指定したキー格納されいる場合はその値，なければデフォルト値を追加して取得する拡張メソッド
        /// </summary>
        public static TValue GetValueOrAddNew<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key) where TValue : new() {
            dict.TryAddNew(key);
            return dict[key];
        }

        /// <summary>
        /// キーが含まれていない場合にデフォルト値を追加して取得する拡張メソッド
        /// </summary>
        public static TValue GetValueOrAddDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key) {
            dict.TryAddDefault(key);
            return dict[key];
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region 要素の追加

        /// <summary>
        /// <see cref="KeyValuePair{TKey, TValue}"/>として要素を追加する拡張メソッド．
        /// </summary>
        public static void Add<TKey, TValue>(this IDictionary<TKey, TValue> dict, KeyValuePair<TKey, TValue> pair)
            => dict.Add(pair.Key, pair.Value);

        /// <summary>
        /// <see cref="KeyValuePair{TKey, TValue}"/>として複数の要素を追加する拡張メソッド．
        /// </summary>
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dict, IEnumerable<KeyValuePair<TKey, TValue>> pairs) {
            foreach (var kv in pairs) {
                dict.Add(kv);
            }
        }

        /// <summary>
        /// キーが含まれていない場合のみ要素を追加する拡張メソッド．
        /// </summary>
        public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value) {
            if (!dict.ContainsKey(key)) {
                dict.Add(key, value);
                return true;
            }

            return false;
        }

        /// <summary>
        /// キーが含まれていない場合のみ要素を追加する拡張メソッド．
        /// </summary>
        public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, Func<TKey, TValue> valueFactory) {
            if (!dict.ContainsKey(key)) {
                dict.Add(key, valueFactory(key));
                return true;
            }

            return false;
        }

        /// <summary>
        /// キーが含まれていない場合に新規要素を追加する拡張メソッド．
        /// </summary>
        public static bool TryAddNew<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key) where TValue : new()
            => dict.TryAdd(key, _ => new TValue());

        /// <summary>
        /// キーが含まれていない場合にデフォルト値を追加する拡張メソッド．
        /// </summary>
        public static bool TryAddDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
            => dict.TryAdd(key, default(TValue));
        #endregion


        /// ----------------------------------------------------------------------------
        // 要素の削除

        /// <summary>
        /// valueを指定して要素を削除する拡張メソッド．
        /// </summary>
        public static void RemoveByValue<TKey, TValue>(this IDictionary<TKey, TValue> dict, TValue value) {
            var removeKeys = dict
                .Where(x => EqualityComparer<TValue>.Default.Equals(x.Value, value))
                .Select(x => x.Key)
                .ToArray();

            foreach (var key in removeKeys) {
                dict.Remove(key);
            }
        }



        /// ----------------------------------------------------------------------------
        // その他

        /// <summary>
        /// 指定されたキーが格納されている場合にactionを呼び出す拡張メソッド
        /// </summary>
        public static void SafeCall<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, Action<TValue> action) {
            if (!self.ContainsKey(key)) {
                return;
            }
            action(self[key]);
        }

        /// <summary>
        /// デコンストラクタ．
        /// </summary>
        public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> self, out TKey key, out TValue value) {
            key = self.Key;
            value = self.Value;
        }

    }
}
