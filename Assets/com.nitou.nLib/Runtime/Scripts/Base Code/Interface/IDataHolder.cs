using System;
using System.Collections.Generic;
using UniRx;

namespace Nitou {

    /// <summary>
    /// An object that can hold data of type <see cref="T"/>.
    /// </summary>
    public interface IDataHolder<T> {

        /// <summary>
        /// Observable that notifies when the value changes.
        /// </summary>
        public IObservable<T> OnValueChanged { get; }

        /// <summary>
        /// Retrieves the value.
        /// </summary>
        public T GetValue();

        /// <summary>
        /// Sets the value.
        /// </summary>
        public void SetValue(T value);
    }


    public static class DataHolderExtensions {

        /// <summary>
        /// Two-way binding.
        /// </summary>
        public static void BindTo<T>(this IReactiveProperty<T> property, IDataHolder<T> target, ICollection<IDisposable> disposables) {

            // Property → Target
            property.SubscribeWithState(target, (x, t) => t.SetValue(x)).AddTo(disposables);

            // Targer → Property
            target.OnValueChanged.SubscribeWithState(property, (x, p) => p.Value = x).AddTo(disposables);
        }
    }
}
