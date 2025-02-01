using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Nitou {

    /// <summary>
    /// 正規化された値（値域０～１）を扱うための構造体
    /// </summary>
    [System.Serializable]
    public struct NormalizedValue {

        [SerializeField] float _value;

        /// <summary>
        /// 値（0～1の範囲）。
        /// </summary>
        public float Value {
            get => _value;
            set => _value = Mathf.Clamp01(value);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NormalizedValue(float value) {
            _value = Mathf.Clamp01(value);
        }

        /// <summary>
        /// float型への暗黙の変換
        /// </summary>
        public static implicit operator float(NormalizedValue normalizedValue) {
            return normalizedValue.Value;
        }

        /// <summary>
        /// float型からの暗黙の変換
        /// </summary>
        public static implicit operator NormalizedValue(float value) {
            return new NormalizedValue(value);
        }

        public override string ToString() {
            return _value.ToString("0.00");
        }
    }
}


/// ----------------------------------------------------------------------------
#if UNITY_EDITOR
namespace Nitou.Inspector {

    [CustomPropertyDrawer(typeof(NormalizedValue))]
    internal sealed class NormalizedFloatPropertyDrawer : PropertyDrawer {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

            using (new EditorGUI.PropertyScope(position, label, property)) {
                SerializedProperty valueProperty = property.FindPropertyRelative("_value");

                // Draw a float slider
                float newValue = EditorGUI.Slider(position, label, valueProperty.floatValue, 0f, 1f);
                valueProperty.floatValue = Mathf.Clamp01(newValue);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
#endif
