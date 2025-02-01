using UnityEngine;

namespace Nitou {

    /// <summary>
    /// 画面の周囲（スクリーンのパディング領域）のマウスやタッチ操作を防止するための構造体
    /// </summary>
    [System.Serializable]
    public struct ScreenDeadZone {

        // 上部のパディング幅（画面高さに対する割合）
        [Range(0f, 1f)] public float top;

        // 下部のパディング幅（画面高さに対する割合）
        [Range(0f, 1f)] public float bottom;

        // 左部のパディング幅（画面幅に対する割合）
        [Range(0f, 1f)] public float left;

        // 右部のパディング幅（画面幅に対する割合）
        [Range(0f, 1f)] public float right;

        public float XMin => left * Screen.width;
        public float YMin => bottom * Screen.height;


        public Rect CalculateSafeZone() {

            var pos = new Vector2(XMin, YMin);
            var size = new Vector2(
                Screen.width - (1 - (left + right)),
                Screen.height - (1 - (top + bottom)));


            return new Rect(pos, size);
        }

        /// <summary>
        /// マウス座標がデッドゾーン内かどうか
        /// </summary>
        public bool IsMouseInDeadZone() {
            Vector2 mousePosition = Input.mousePosition;
            Rect safeZone = CalculateSafeZone();

            // Invert the Y coordinate for mouse position to align with Unity's GUI Rect coordinate system
            mousePosition.y = Screen.height - mousePosition.y;

            return !safeZone.Contains(mousePosition);
        }

    }
}



#if UNITY_EDITOR
namespace Nitou.EditorScripts {
    using UnityEditor;

    /*

    [CustomPropertyDrawer(typeof(ScreenDeadZone))]
    public class ScreenDeadZoneDrawer : PropertyDrawer {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            // 親プロパティ（ScreenDeadZone）のラベルを表示
            EditorGUI.PrefixLabel(position, label);

            // インデントの設定
            EditorGUI.indentLevel++;

            // Rectプロパティを取得
            SerializedProperty percentageProp = property.FindPropertyRelative("percentage");

            // 各フィールドを表示
            Rect rectField = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);

            // xMin, yMin, width, height をそれぞれ0.0～1.0の範囲内でスライダー表示
            EditorGUI.Slider(new Rect(rectField.x, rectField.y, rectField.width, rectField.height), percentageProp.FindPropertyRelative("x"), 0.0f, 1.0f, "X Min");
            rectField.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.Slider(new Rect(rectField.x, rectField.y, rectField.width, rectField.height), percentageProp.FindPropertyRelative("y"), 0.0f, 1.0f, "Y Min");
            rectField.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.Slider(new Rect(rectField.x, rectField.y, rectField.width, rectField.height), percentageProp.FindPropertyRelative("width"), 0.0f, 1.0f, "Width");
            rectField.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.Slider(new Rect(rectField.x, rectField.y, rectField.width, rectField.height), percentageProp.FindPropertyRelative("height"), 0.0f, 1.0f, "Height");

            // インデントを戻す
            EditorGUI.indentLevel--;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            // 4つのスライダーと標準の間隔分の高さを計算
            return EditorGUIUtility.singleLineHeight * 5 + EditorGUIUtility.standardVerticalSpacing * 4;
        }
    }

    */
}
#endif