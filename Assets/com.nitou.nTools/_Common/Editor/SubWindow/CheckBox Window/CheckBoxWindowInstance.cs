#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

// [参考]
//  github: Kogane Check Box Window https://github.com/baba-s/Kogane.CheckBoxWindow

namespace Nitou.Tools.Shared {
    using Nitou.EditorShared;

    /// <summary>
    /// チェックボックスのリストを表示するウインドウ
    /// </summary>
    public sealed class CheckBoxWindowInstance : EditorWindow {

        /// <summary>
        /// リストデータ
        /// </summary>
        private IReadOnlyList<ICheckBoxWindowData> _dataList = Array.Empty<CheckBoxWindowData>();
        
        /// <summary>
        /// 
        /// </summary>
        private Action<IReadOnlyList<ICheckBoxWindowData>> _onOk;

        private SearchField _searchField;
        private string _filteringText = string.Empty;
        private Vector2 _scrollPosition;

        // 定数
        private const int BUTTON_WIDTH = 80;
        private const int ROW_HEIGHT = 18;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// ウインドウを開く
        /// </summary>
        public void Open(
            string title,
            IReadOnlyList<ICheckBoxWindowData> dataList,
            Action<IReadOnlyList<ICheckBoxWindowData>> onOk
        ) {
            titleContent = new(title);
            wantsMouseMove = true;
            _dataList = dataList;
            _onOk = onOk;
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// 
        /// </summary>
        private void OnGUI() {
            // パディングを設定
            int padding = 5;
            using var areaScope = new GUILayout.AreaScope(new Rect(padding, padding, position.width - padding * 2, position.height - padding * 2));

            // 
            using (new EditorGUILayout.HorizontalScope()) {
                if (GUILayout.Button("Select all", GUILayout.Width(BUTTON_WIDTH))) {
                    foreach (var x in _dataList) {
                        x.IsChecked = true;
                    }
                }
                if (GUILayout.Button("Deselect all", GUILayout.Width(BUTTON_WIDTH))) {
                    foreach (var x in _dataList) {
                        x.IsChecked = false;
                    }
                }

                // 検索フィールド
                _searchField ??= new();
                _filteringText = _searchField.OnToolbarGUI(_filteringText);
            }

            // 
            DrawContent();

            // 
            using (new EditorGUILayout.HorizontalScope()) {
                if (GUILayout.Button("OK")) {
                    _onOk(_dataList);
                    Close();
                }
                if (GUILayout.Button("Cancel")) {
                    Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void DrawContent() {

            using (var scrollViewScope = new EditorGUILayout.ScrollViewScope(_scrollPosition)) {
                var mousePosition = Event.current.mousePosition;
                var y = 0;

                for (var i = 0; i < _dataList.Count; i++) {
                    var data = _dataList[i];
                    var name = data.Name;

                    // ※フィルタリング
                    if (!name.Contains(_filteringText, StringComparison.OrdinalIgnoreCase)) continue;

                    var isHover = ((int)mousePosition.y) / ROW_HEIGHT == y;
                    var style = isHover ? Styles.hover : Styles.GetRowStyle(i);

                    using var hs = new EditorGUILayout.HorizontalScope(style);
                    using var vs = new EditorGUILayout.VerticalScope();

                    GUILayout.FlexibleSpace();
                    data.IsChecked = EditorGUILayout.ToggleLeft(name, data.IsChecked);
                    GUILayout.FlexibleSpace();

                    y++;
                }

                _scrollPosition = scrollViewScope.scrollPosition;
            }
        }


        /// ----------------------------------------------------------------------------
        private static class Styles {

            public static GUIStyle evenRowItem;
            public static GUIStyle oddRowItem;
            public static GUIStyle hover;

            /// <summary>
            /// コンストラクタ
            /// </summary>
            static Styles() {

                // Row Styles
                evenRowItem = CreateGUIStyle(new Color32(63, 63, 63, 255));
                oddRowItem = CreateGUIStyle(new Color32(56, 56, 56, 255));

                //
                //hover = CreateGUIStyle(new Color32(44, 93, 135, 255));
                hover = CreateGUIStyle(EditorColors.ButtonBackgroundHoverPressedr);                
            }

            public static GUIStyle GetRowStyle(int index) {
                return index.IsEven() ? evenRowItem : oddRowItem;
            }

            private static GUIStyle CreateGUIStyle(Color color) {
                var background = new Texture2D(1, 1);
                background.SetPixel(0, 0, color);
                background.Apply();

                var style = new GUIStyle {
                    fixedHeight = ROW_HEIGHT
                };
                style.normal.background = background;

                return style;
            }
        }
    }
}
#endif