using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Nitou {

    /// <summary>
    /// 0～1の範囲の正規化された最小値と最大値を扱うための構造体。
    /// </summary>
    [System.Serializable]
    public struct NormalizedRange {

        [Range(0f, 1f)]
        [SerializeField] float _minValue;

        [Range(0f, 1f)]
        [SerializeField] float _maxValue;

        /// <summary>
        /// 最小値（0～1の範囲）。
        /// </summary>
        public float Min {
            get => _minValue;
            set => _minValue = Mathf.Clamp01(value);
        }

        /// <summary>
        /// 最大値（0～1の範囲）。
        /// </summary>
        public float Max {
            get => _maxValue;
            set => _maxValue = Mathf.Clamp01(value);
        }

        /// <summary>
        /// コンストラクタ。最小値と最大値を0～1の範囲に正規化して設定します。
        /// </summary>
        public NormalizedRange(float minValue, float maxValue) {
            _minValue = Mathf.Clamp01(minValue);
            _maxValue = Mathf.Clamp01(maxValue);
        }

        /// <summary>
        /// このインスタンスの値を文字列として返します。
        /// </summary>
        public override string ToString() {
            return $"Min: {_minValue:0.00}, Max: {_maxValue:0.00}";
        }
    }
}


#if UNITY_EDITOR
namespace Nitou.Inspector {

    [CustomPropertyDrawer(typeof(NormalizedRange))]
    internal sealed class NormalizedRangePropertyDrawer : PropertyDrawer {

        /// <summary>
        /// プロパティのGUIを描画します。
        /// </summary>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            // プロパティのスコープを設定
            using (new EditorGUI.PropertyScope(position, label, property)) {
                // "_minValue"と"_maxValue"プロパティを取得
                SerializedProperty minValueProperty = property.FindPropertyRelative("_minValue");
                SerializedProperty maxValueProperty = property.FindPropertyRelative("_maxValue");

                if (minValueProperty != null && maxValueProperty != null) {
                    float minValue = minValueProperty.floatValue;
                    float maxValue = maxValueProperty.floatValue;

                    // MinMaxSliderで値を入力
                    EditorGUI.MinMaxSlider(position, label, ref minValue, ref maxValue, 0f, 1f);

                    // 入力された値をClampして反映
                    minValueProperty.floatValue = Mathf.Clamp01(minValue);
                    maxValueProperty.floatValue = Mathf.Clamp01(maxValue);
                } else {
                    EditorGUI.LabelField(position, label.text, "Error: '_minValue' or '_maxValue' not found.");
                }
            }
        }

        /// <summary>
        /// プロパティの高さを取得します。
        /// </summary>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
#endif
