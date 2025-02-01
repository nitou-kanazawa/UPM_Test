using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using UniRx;

namespace Nitou {

    /// <summary>
    /// 一定時間経過した要素を削除するコレクション
    /// </summary>
    public class TimedCollection<T> : IEnumerable<T>, IDisposable {

        /// <summary>
        /// 格納用のデータクラス
        /// </summary>
        private class TimedItem {
            public T Item { get; }
            public DateTime ExpirationTime { get; }

            public TimedItem(T item, DateTime expirationTime) {
                Item = item;
                ExpirationTime = expirationTime;
            }
        }

        private readonly List<TimedItem> _items = new();
        private readonly TimeSpan _timeout;

        // 内部処理用
        private bool _isProcessing = false;
        private readonly Subject<T> _onItemRemoved = new();
        private CancellationTokenSource _cts;

        /// <summary>
        /// 現在の要素数
        /// </summary>
        public int Count => _items.Count;

        /// <summary>
        /// データが取り除かれるときのイベント通知
        /// </summary>
        public IObservable<T> OnItemRemoved => _onItemRemoved;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// Constructor.
        /// </summary>
        public TimedCollection(TimeSpan timeout) {
            _timeout = timeout;
            _cts = new CancellationTokenSource();
        }
        
        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose() {
            _cts.Cancel();  // タスクをキャンセル
            _cts.Dispose();
            _onItemRemoved.OnCompleted(); // ストリームの完了を通知
            _onItemRemoved.Dispose(); // リソースの解放
        }


        /// ----------------------------------------------------------------------------
        // Private Method (コレクション操作)

        /// <summary>
        /// Add item.
        /// </summary>
        public void Add(T item) {
            var expirationTime = DateTime.UtcNow + _timeout;
            _items.Add(new TimedItem(item, expirationTime));

            if (!_isProcessing) {
                _isProcessing = true;
                RemoveExpiredItemsAsync(_cts.Token).Forget();
            }
        }

        /// <summary>
        /// Remove item.
        /// </summary>
        public bool Remove(T item) {
            var timedItem = _items.Find(t => EqualityComparer<T>.Default.Equals(t.Item, item));
            if (timedItem != null) {
                _items.Remove(timedItem);
                _onItemRemoved.OnNext(timedItem.Item); // アイテム除外の通知
                return true;
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator() {
            foreach (var timedItem in _items) {
                yield return timedItem.Item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        private async UniTaskVoid RemoveExpiredItemsAsync(CancellationToken cancellationToken) {
            try {
                while (_items.Count > 0) {
                    await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancellationToken);

                    var now = DateTime.UtcNow;
                    var expiredItems = _items.FindAll(item => item.ExpirationTime <= now);
                    foreach (var expiredItem in expiredItems) {
                        _items.Remove(expiredItem);
                        _onItemRemoved.OnNext(expiredItem.Item); // アイテム除外の通知
                    }
                }
            } catch (OperationCanceledException) {
                // タスクがキャンセルされた場合の処理
            } finally {
                _isProcessing = false;
            }
        }
    }
}