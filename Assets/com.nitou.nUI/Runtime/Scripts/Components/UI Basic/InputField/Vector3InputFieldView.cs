using System;
using UniRx;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
using System.Globalization;

namespace Nitou.UI {

    /// <summary>
    /// <see cref="Vector3"/>の入力を受け付けるためのView．
    /// </summary>
    public sealed class Vector3InputFieldView : InputFieldView<Vector3> {

        [Title("View")]
        [SerializeField, Indent] private TMP_InputField _xInputField;
        [SerializeField, Indent] private TMP_InputField _yInputField;
        [SerializeField, Indent] private TMP_InputField _zInputField;

        [Title("Settings")]
        [SerializeField, Indent] private int _decimalPlaces = 2;


        /// ----------------------------------------------------------------------------
        // LifeCycle Events

        private void Awake() {
            // ViewModelの監視
            _valueRP.Subscribe(v => ApplyValue(v)).AddTo(this);

            // Viewの監視
            Observable.Merge(
                _xInputField.onEndEdit.AsObservable().AsUnitObservable(),
                _yInputField.onEndEdit.AsObservable().AsUnitObservable(),
                _zInputField.onEndEdit.AsObservable().AsUnitObservable()
            )
            .Subscribe(_ => UpdateValue())
            .AddTo(this);
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        private void UpdateValue() {
            Vector3 currentValue = _valueRP.Value;
            Vector3? parsedValue = TryParseValue();

            // パースが成功した場合のみReactivePropertyに反映
            if (parsedValue.HasValue) {
                _valueRP.Value = parsedValue.Value;
            } else {
                // パースに失敗した場合は元の値を再適用
                ApplyValue(currentValue);
            }
        }

        /// <summary>
        /// Viewから値を読み取る．
        /// </summary>
        private Vector3? TryParseValue() {

            // parse values
            bool xParsed = float.TryParse(_xInputField.text, NumberStyles.Float, CultureInfo.InvariantCulture, out var x);
            bool yParsed = float.TryParse(_yInputField.text, NumberStyles.Float, CultureInfo.InvariantCulture, out var y);
            bool zParsed = float.TryParse(_zInputField.text, NumberStyles.Float, CultureInfo.InvariantCulture, out var z);

            // 全てのパースが成功した場合にのみ値を返す
            if (xParsed && yParsed && zParsed) {
                return new Vector3(x, y, z);
            }

            // いずれかのパースに失敗した場合はnullを返す
            return null;
        }

        /// <summary>
        /// Viewに値を適用する．
        /// </summary>
        private void ApplyValue(Vector3 value) {
            _xInputField.text = value.x.ToFloatText(_decimalPlaces);
            _yInputField.text = value.y.ToFloatText(_decimalPlaces);
            _zInputField.text = value.z.ToFloatText(_decimalPlaces);
        }

#if UNITY_EDITOR
        private void OnValidate() {
            _decimalPlaces = Mathf.Clamp(_decimalPlaces, 0, 5);
        }
#endif
    }
}