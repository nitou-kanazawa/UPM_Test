using System;
using System.Collections.Generic;

namespace Nitou {

    /// <summary>
    /// 
    /// </summary>
    public interface IDisposableCollectionHolder {
        ICollection<IDisposable> GetDisposableCollection();
    }

    /// <summary>
    /// 
    /// </summary>
    public static partial class DisposableExtensions {

        public static void AddTo(this IDisposable disposable, IDisposableCollectionHolder holder) {
            if (disposable == null) throw new ArgumentNullException(nameof(disposable));
            if (holder == null) throw new ArgumentNullException(nameof(holder));

            holder.GetDisposableCollection().Add(disposable);
        }
    }
}
