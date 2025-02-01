using System;
using UniRx;
using UnityEngine.UI;

namespace Nitou {

    /// <summary>
    /// <see cref="Toggle"/>型の基本的な拡張メソッド集．
    /// </summary>
    public static partial class ToggleExtensions {

        /// ----------------------------------------------------------------------------
        // イベントの登録

        /// <summary>
        /// イベント登録を簡略化する拡張メソッド
        /// </summary>
        public static IDisposable SetOnValueChangedDestination(this Toggle self, Action<bool> onValueChanged) {
            return self.onValueChanged
                .AsObservable()
                .Subscribe(onValueChanged.Invoke)
                .AddTo(self);
        }
    }
}
