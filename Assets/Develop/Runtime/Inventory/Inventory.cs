using System;
using System.Collections.Generic;
using UniRx;

namespace Nitou.InventorySystem {

    /// <summary>
    /// シンプルなインベントリの基底クラス．
    /// </summary>
    public abstract class Inventory<TItem> : IDisposable
        where TItem : IInventoryItem {

        protected readonly ReactiveCollection<TItem> _items = new ();

        /// <summary>
        /// 外部からの購読用．
        /// </summary>
        public IReadOnlyReactiveCollection<TItem> Items => _items;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public Inventory() {

        }

        /// <summary>
        /// 終了処理．
        /// </summary>
        public void Dispose() {
            _items.Dispose();
        }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// 
        /// </summary>
        public virtual void AddItem(TItem item) {
            _items.Add(item);
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void RemoveItem(TItem item) {
            _items.Remove(item);
        }

        public IReadOnlyList<TItem> GetItems() {
            return _items;
        }

    }

}
