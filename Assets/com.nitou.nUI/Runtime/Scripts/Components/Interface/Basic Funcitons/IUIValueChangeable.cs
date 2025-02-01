using System;

namespace Nitou.UI.Components{

    /// <summary>
    /// "ValueChange"イベントを扱うUIであることを示すインターフェース．
    /// </summary>
    public interface IUIValueChangeable<TValue>{

        /// <summary>
        /// 値が変化した時のイベント通知．
        /// </summary>
        public IObservable<TValue> OnValueChanged { get; }

    }
}
