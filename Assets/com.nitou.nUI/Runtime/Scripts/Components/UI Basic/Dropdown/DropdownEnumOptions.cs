using System;
using System.Linq;
using UniRx;
using UnityEngine;
using TMPro;

namespace Nitou.UI {

    /// <summary>
    /// 
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TMP_Dropdown))]
    public abstract class DropdownEnumOptions<TEnum> : MonoBehaviour, IDataHolder<TEnum>
        where TEnum : Enum {

        private TMP_Dropdown _dropdown;
        private ReactiveProperty<TEnum> _currentRP = new();

        /// <summary>
        /// 値の変化を通知するObservable
        /// </summary>
        public IObservable<TEnum> OnValueChanged => _currentRP;

        // 全要素
        private static readonly TEnum[] enumValues = (TEnum[])Enum.GetValues(typeof(TEnum));

        private static int GetEnumIndex(TEnum type) {
            return Array.IndexOf(enumValues, type);
        }


        /// ----------------------------------------------------------------------------
        // LifeCycle Events

        protected void Start() {
            Setup();

            // Viewの更新
            _dropdown.onValueChanged.AsObservable()
                .Subscribe(index => {
                    if (index.IsInRange(enumValues))
                        _currentRP.Value = enumValues[index];
                    else
                        Setup();
                })
                .AddTo(this);

            // RPの更新
            _currentRP
                .Subscribe(type => {
                    _dropdown.value = GetEnumIndex(type);
                    _dropdown.RefreshShownValue();
                }).AddTo(this);
        }

        private void OnDestroy() {
            _currentRP?.Dispose();
        }

        private void OnValidate() {
            if (_dropdown is null)
                _dropdown = GetComponent<TMP_Dropdown>();
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// ゲッタ．
        /// </summary>
        public TEnum GetValue() {
            return _currentRP.Value;
        }

        /// <summary>
        /// セッタ．
        /// </summary>
        public void SetValue(TEnum type) {
            _currentRP.Value = type;
        }

        /// <summary>
        /// 
        /// </summary>
        private void Setup() {
            OnValidate();

            // Enumの名前リストを取得してDropdownのオプションに設定
            _dropdown.options.Clear();
            _dropdown.options.AddRange(enumValues.Select(name => new TMP_Dropdown.OptionData(name.ToString())));

            // 初期選択を最初の項目に設定
            _dropdown.value = GetEnumIndex(_currentRP.Value);
            _dropdown.RefreshShownValue();
        }
    }
}
