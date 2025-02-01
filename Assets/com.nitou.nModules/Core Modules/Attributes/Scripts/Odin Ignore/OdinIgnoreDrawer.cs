#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Nitou {

    [CustomPropertyDrawer(typeof(OdinIgnoreAttribute))]
    internal class OdinIgnoreDrawer : PropertyDrawer {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent content) {

            // ※問題のあるColor型のみ用意しておく
            if(property.propertyType == SerializedPropertyType.Color) {
                property.colorValue = EditorGUI.ColorField(position, property.displayName, property.colorValue);
            }

        }
    }
}
#endif