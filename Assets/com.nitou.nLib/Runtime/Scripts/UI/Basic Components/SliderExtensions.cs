using System;
using UniRx;
using UnityEngine.UI;

namespace Nitou {

    /// <summary>
    /// <see cref="Slider"/>型の基本的な拡張メソッド集．
    /// </summary>
    public static partial class SliderExtensions {

        /// ----------------------------------------------------------------------------
        // イベントの登録

        /// <summary>
        /// イベント登録を簡略化する拡張メソッド
        /// </summary>
        public static IDisposable SetOnValueChangedDestination(this Slider self, Action<float> onValueChanged) {
            return self.onValueChanged
                .AsObservable()
                .Subscribe(onValueChanged.Invoke)
                .AddTo(self);
        }

    }
}
