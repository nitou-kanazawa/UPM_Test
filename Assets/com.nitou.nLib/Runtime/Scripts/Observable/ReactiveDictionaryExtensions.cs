using System;
using System.Collections.Generic;
using System.Linq;
using Nitou;

namespace UniRx {

    /// <summary>
    /// ReactiveDictionary拡張メソッド.
    /// </summary>
    public static class ReactiveDictionaryExtensions {

        /// <summary>
        /// 変更があったことを通知するObservable.
        /// </summary>
        public static IObservable<Unit> ObserveAnyChanged<TKey, TValue>(this ReactiveDictionary<TKey, TValue> self) {
            return Observable.Merge(
                self.ObserveAdd().AsUnitObservable(),
                self.ObserveCountChanged().AsUnitObservable(),
                self.ObserveRemove().AsUnitObservable(),
                self.ObserveReplace().AsUnitObservable(),
                self.ObserveReset().AsUnitObservable()
                )
                .ThrottleFrame(1);
        }

        /// <summary>
        /// 中身だけを全て入れ替える.
        /// </summary>
        public static void Set<TKey, TValue>(this ReactiveDictionary<TKey, TValue> self, IEnumerable<TValue> source, Func<TValue, TKey> selector) {
            HashSet<TKey> activeKeys = new HashSet<TKey>();

            foreach (TValue value in source) {
                TKey key = selector(value);
                activeKeys.Add(key);
                self[key] = value;
            }

            //アクティブリストに入っていないアイテムを除外.
            TKey[] removeKeys = self.Keys
                    .Where(key => !activeKeys.Contains(key))
                    .ToArray();
            foreach (var key in removeKeys) {
                self.Remove(key);
            }
        }

        /// <summary>
        /// 中身だけを全て入れ替える.
        /// </summary>
        public static void Set<TKey, TValue, TRValue>(this ReactiveDictionary<TKey, TRValue> self, IEnumerable<TValue> source, Func<TValue, TKey> selector)
                where TRValue : IReactiveProperty<TValue>, new() {
            HashSet<TKey> activeKeys = new HashSet<TKey>();

            foreach (TValue value in source) {
                TKey key = selector(value);
                activeKeys.Add(key);

                //valueが存在しない場合、新しく生成.
                if (!self.ContainsKey(key))
                    self[key] = new TRValue();
                self[key].Value = value;
                self[key] = self[key];
            }

            //アクティブリストに入っていないアイテムを除外.
            TKey[] removeKeys = self.Keys
                    .Where(key => !activeKeys.Contains(key))
                    .ToArray();

            foreach (var key in removeKeys) {
                self.Remove(key);
            }
        }
    }
}
