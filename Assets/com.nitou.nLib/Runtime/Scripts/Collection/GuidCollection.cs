using System;
using System.Collections.ObjectModel;

namespace Nitou {

    public class GuidCollection<T> : KeyedCollection<Guid, T>, IKeyedCollection<Guid, T>
        where T : IIdentifiable {

        protected override Guid GetKeyForItem(T item) {
            return item.guid;
        }

        protected override void InsertItem(int index, T item) {
            base.InsertItem(index, item);
        }

        protected override void SetItem(int index, T item) {
            base.SetItem(index, item);
        }

        public new bool TryGetValue(Guid key, out T value) {
            if (Dictionary == null) {
                value = default(T);
                return false;
            }

            return Dictionary.TryGetValue(key, out value);
        }
    }
}
