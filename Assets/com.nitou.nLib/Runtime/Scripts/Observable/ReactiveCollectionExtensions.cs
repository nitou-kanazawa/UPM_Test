using System;
using System.Collections.Generic;
using System.Linq;
using Nitou;

namespace UniRx {

    /// <summary>
    /// <see cref="ReactiveCollection{T}"/>の基本的な拡張メソッド集．
    /// </summary>
    public static partial class ReactiveCollectionExtensions {

        /// <summary>
        /// 変更があったことを通知するObservableを返す.
        /// </summary>
        public static IObservable<Unit> ObserveAnyChanged<T>(this ReactiveCollection<T> self) {
            return Observable.Merge(
                self.ObserveAdd().AsUnitObservable(),
                self.ObserveCountChanged().AsUnitObservable(),
                self.ObserveMove().AsUnitObservable(),
                self.ObserveRemove().AsUnitObservable(),
                self.ObserveReplace().AsUnitObservable(),
                self.ObserveReset().AsUnitObservable()
                )
                .ThrottleFrame(1, FrameCountType.EndOfFrame);
        }

        /// <summary>
        /// 中身だけを全て入れ替える拡張メソッド.
        /// </summary>
        public static ReactiveCollection<T> Set<T>(this ReactiveCollection<T> self, IList<T> source) {
            int before = self.Count;
            int after = source.Count;
            int minCount = before < after ? before : after;

            // Replace.
            for (int i = 0; i < minCount; i++) {
                self[i] = source[i];
            }

            // Add.
            for (int i = before; i < after; i++) {
                self.Add(source[i]);
            }

            // Remove.
            for (int i = before - 1; after <= i; i--) {
                self.RemoveAt(i);
            }
            return self;
        }

        /// <summary>
        /// 中身を同期させる拡張メソッド．
        /// 正常に追加/削除のイベント発火が発火されるが，要素の順序は考慮しない．
        /// </summary>
        public static void SynchronizeWith<T>(this ReactiveCollection<T> self, IEnumerable<T> source) {
            // 要素の削除
            self.RemoveAll(x => !source.Contains(x));
            // 要素の追加
            self.AddRangeIfNotContains(source);
        }

        /// <summary>
        /// 条件マッチするインデックスを取得する拡張メソッド.
        /// </summary>
        public static int FindIndex<T>(this ReactiveCollection<T> self, Predicate<T> match) {
            for (int i = 0; i < self.Count; i++) {
                if (match(self[i]))
                    return i;
            }
            return -1;
        }

    }
}
