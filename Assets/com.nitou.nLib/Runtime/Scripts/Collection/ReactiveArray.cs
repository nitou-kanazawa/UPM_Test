using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;

// [REF]
//  github: ReactiveCollection.cs https://github.com/neuecc/UniRx/blob/master/Assets/Plugins/UniRx/Scripts/UnityEngineBridge/ReactiveCollection.cs#L40

namespace Nitou {

    /// <summary>
    /// 配列の要素の変更や移動をリアクティブに監視できる ReactiveArray クラス
    /// </summary>
    public sealed class ReactiveArray<T> : IDisposable, IEnumerable<T>, IEnumerable {

        private readonly T[] _items;

        [NonSerialized] bool _isDisposed = false;

        // Streem
        [NonSerialized] Subject<CollectionReplaceEvent<T>> _collectionReplace = null;
        [NonSerialized] Subject<CollectionMoveEvent<T>> _collectionMove = null;

        /// <summary>
        /// インデクサ
        /// </summary>
        public T this[int index] {
            get => GetItem(index);
            set => SetItem(index, value);
        }

        /// <summary>
        /// 要素数．
        /// </summary>
        public int Length => _items.Length;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// Constructor.
        /// </summary>
        public ReactiveArray(int length) {
            _items = new T[length];
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ReactiveArray(IList<T> list) {
            if (list is null) throw new ArgumentNullException(nameof(list));

            _items = new T[list.Count];
            for (int i = 0; i < list.Count; i++) {
                _items[i] = list[i];
            }
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose() {
            if (!_isDisposed) {
                SubjectUtils.DisposeSubject(ref _collectionReplace);
                SubjectUtils.DisposeSubject(ref _collectionMove);

                _isDisposed = true;
            }
        }

        /// <summary>
        /// 指定インデックスの要素を取得する．
        /// </summary>
        public T GetItem(int index) {
            if (_isDisposed) throw new ObjectDisposedException(nameof(ReactiveArray<T>));
            if (index.IsOutRange(_items)) throw new ArgumentOutOfRangeException(nameof(index));
            return _items[index];
        }

        /// <summary>
        /// 指定インデックスに要素を設定する．
        /// </summary>
        public void SetItem(int index, T value) {
            if (_isDisposed) throw new ObjectDisposedException(nameof(ReactiveArray<T>));
            if (index.IsOutRange(_items)) throw new ArgumentOutOfRangeException(nameof(index));

            var oldValue = _items[index];
            _items[index] = value;

            // イベント通知
            _collectionReplace?.OnNext(new CollectionReplaceEvent<T>(index, oldValue, value));
        }

        /// <summary>
        /// 要素を移動する．
        /// </summary>
        public void Move(int oldIndex, int newIndex) {
            if (_isDisposed) throw new ObjectDisposedException(nameof(ReactiveArray<T>));
            if (oldIndex.IsOutRange(_items) || newIndex.IsOutRange(_items)) {
                throw new ArgumentOutOfRangeException();
            }

            var item = _items[oldIndex];
            // 配列内の要素をシフト
            if (oldIndex < newIndex) {
                for (int i = oldIndex; i < newIndex; i++) {
                    _items[i] = _items[i + 1];
                }
            } else {
                for (int i = oldIndex; i > newIndex; i--) {
                    _items[i] = _items[i - 1];
                }
            }

            _items[newIndex] = item;

            // Move イベントを発行
            _collectionMove?.OnNext(new CollectionMoveEvent<T>(oldIndex, newIndex, item));
        }


        /// ----------------------------------------------------------------------------
        // Public Method (Event Streem)

        /// <summary>
        /// 
        /// </summary>
        public IObservable<CollectionReplaceEvent<T>> ObserveReplace() {
            if (_isDisposed) return Observable.Empty<CollectionReplaceEvent<T>>();
            return _collectionReplace ?? (_collectionReplace = new Subject<CollectionReplaceEvent<T>>());
        }

        /// <summary>
        /// 
        /// </summary>
        public IObservable<CollectionMoveEvent<T>> ObserveMove() {
            if (_isDisposed) return Observable.Empty<CollectionMoveEvent<T>>();
            return _collectionMove ?? (_collectionMove = new Subject<CollectionMoveEvent<T>>());
        }


        /// ----------------------------------------------------------------------------
        // Public Method (Enumerable)

        /// <summary>
        /// 配列の列挙をサポートするための IEnumerable<T> の実装
        /// </summary>
        public IEnumerator<T> GetEnumerator() {
            if (_isDisposed) throw new ObjectDisposedException(nameof(ReactiveArray<T>));
            return ((IEnumerable<T>)_items).GetEnumerator();
        }

        /// <summary>
        /// 非ジェネリック IEnumerable の実装
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
