using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

// [REF]
//  ねこじゃらシティ: レイヤーをインスペクターから選択可能にする構造体 https://nekojara.city/unity-layer-inspector

namespace Nitou {

    /// <summary>
    /// レイヤー設定用の構造体．
    /// </summary>
    [System.Serializable]
    public struct Layer : IEquatable<Layer> {

        [SerializeField] private int _value;

        /// <summary>
        /// レイヤー値．
        /// </summary>
        public int Value {
            get => _value;
            set {
                if (!IsInRange(value)) {
                    throw new ArgumentOutOfRangeException(nameof(value), "レイヤーは0～31の範囲で指定してください。");
                }
                _value = value;
            }
        }

        /// <summary>
        /// レイヤー名．
        /// </summary>
        public string Name {
            get => LayerMask.LayerToName(_value);
            set {
                var layerValue = LayerMask.NameToLayer(value);

                // レイヤー名が存在しない場合はエラー
                if (layerValue == -1)
                    throw new System.ArgumentException($"レイヤー名「{value}」は存在しません。", nameof(value));

                _value = layerValue;
            }
        }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// 同値比較．
        /// </summary>
        public bool Equals(Layer other) => _value == other._value;

        /// <summary>
        /// 同値比較．
        /// </summary>
        public override bool Equals(object obj) => obj is Layer other && Equals(other);

        /// <summary>
        /// ハッシュコードの計算
        /// </summary>
        public override int GetHashCode() => _value.GetHashCode();

        /// <summary>
        /// string型への変換．
        /// </summary>
        public override string ToString() {
            return $"{Name}({_value})";
        }


        /// ----------------------------------------------------------------------------
        #region Static
        public static bool IsInRange(int value) {
            return 0 <= value && value <= 31;
        }

        public static bool operator ==(Layer left, Layer right) => left.Equals(right);
        public static bool operator !=(Layer left, Layer right) => !left.Equals(right);

        public static implicit operator int(Layer layer) => layer.Value;
        public static explicit operator Layer(int value) => new Layer { Value = value };
        #endregion
    }
}


#if UNITY_EDITOR
namespace Nitou.Inspector.EditorScripts {
    
    [CustomPropertyDrawer(typeof(Layer))]
    internal class LayerPropertyDrawer : PropertyDrawer {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);

            var valueProperty = property.FindPropertyRelative("_value");

            // 現在設定されているレイヤー値を取得
            var currentValue = valueProperty.intValue;

            // レイヤー一覧を表示
            var newValue = EditorGUI.LayerField(position, label, currentValue);

            // レイヤー値を更新
            valueProperty.intValue = newValue;

            EditorGUI.EndProperty();
        }
    }
}
#endif
