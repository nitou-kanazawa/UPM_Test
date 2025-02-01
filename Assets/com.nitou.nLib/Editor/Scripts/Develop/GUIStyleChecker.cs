#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Nitou.EditorScripts {

    public class GUIStyleChecker : EditorWindow {

        private List<GUIStyle> _editorGUIStyles;
        private Vector2 _position;

        /// <summary>
        /// 
        /// </summary>
        [MenuItem(ToolBarMenu.Prefix.Develop + "GUI Style/GUIStyleChecker")]
        private static void Open() {
            GetWindow<GUIStyleChecker>("GUIStyleChecker");
        }
        
        private void OnGUI() {
            Init();
            using (var scroll = new GUILayout.ScrollViewScope(_position)) {
                _position = scroll.scrollPosition;
                foreach (var style in _editorGUIStyles) {
                    using (new EditorGUILayout.HorizontalScope("box")) {
                        EditorGUILayout.SelectableLabel(style.name);
                        GUILayout.Space(10);
                        EditorGUILayout.LabelField(style.name, style, GUILayout.ExpandWidth(true));
                    }
                }
            }
        }
        
        private void Init() {
            if (_editorGUIStyles != null)
                return;

            _editorGUIStyles = new List<GUIStyle>();
            var e = GUI.skin.GetEnumerator();
            while (e.MoveNext()) {
                try {
                    _editorGUIStyles.Add(e.Current as GUIStyle);
                } catch {
                    // ignored
                }
            }
        }
    }
}
#endif