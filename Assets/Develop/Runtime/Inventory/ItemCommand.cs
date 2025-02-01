using UnityEngine;

namespace Nitou.InventorySystem {

    /// <summary>
    /// アイテムに対するコマンド．
    /// </summary>
    public abstract class ItemCommand<TItem>
        where TItem : IInventoryItem {

        /// <summary>
        /// コマンドを実行する．
        /// </summary>
        public abstract void Execute(TItem item);
    }


    /// <summary>
    /// アイテムに対するコマンド．
    /// </summary>
    public abstract class ItemCommand<TItem, TInventory>
        where TItem : IInventoryItem 
        where TInventory : Inventory<TItem>{

        /// <summary>
        /// コマンドを実行する．
        /// </summary>
        public abstract void Execute(TItem item, TInventory inventory);
    }

}
