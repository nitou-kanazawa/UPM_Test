using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
using Nitou.EditorShared;
#endif

namespace Nitou.DebugInternal {


    // [参考]
    // Unity IssueTracker: Screen.width and Screen.height values in "OnEnable" function are incorrect https://issuetracker.unity3d.com/issues/screen-dot-width-and-screen-dot-height-values-in-onenable-function-are-incorrect
    // Search Issue Tracker:  https://issuetracker.unity3d.com/issues/screen-dot-width-slash-screen-dot-height-in-onenable-shows-inspector-window-size-when-the-component-is-enabled-by-a-toggle-in-inspector-window

    //[ExecuteAlways]
    internal class ScreenSizeDebugger : MonoBehaviour {

        // サイズの記録
        public Vector2 size_Update;
        public Vector2 size_OnEnable;
        public Vector2 size_OnDisable;

        // 
        public DateTime lastEnableTime = DateTime.MinValue;
        public DateTime lastDisableTime = DateTime.MinValue;


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method

        private void Update() {
            size_Update = new(Screen.width, Screen.height);
        }

        private void OnEnable() {
            size_OnEnable = new(Screen.width, Screen.height);
            lastEnableTime = DateTime.Now;
        }

        private void OnDisable() {
            size_OnDisable = new(Screen.width, Screen.height);
            lastDisableTime = DateTime.Now;
        }
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(ScreenSizeDebugger))]
    internal class ScreenSizeDebuggerEditor : Editor {

        public override void OnInspectorGUI() {
            var instance = target as ScreenSizeDebugger;

            // MonoBehaviour
            var modeText = Application.isPlaying ? "Play mode" : "Edit mode";
            DrawScreenSizeInfo($"MonoBehaviour.<b>Update</b> ({modeText})", instance.size_Update.x, instance.size_Update.y);

            DrawScreenSizeInfo($"MonoBehaviour.<b>OnEnable</b> ({modeText})", instance.size_OnEnable.x, instance.size_OnEnable.y);
            EditorGUILayout.LabelField($"elaposed time {(DateTime.Now - instance.lastEnableTime).ToString("ss")} [s]");

            DrawScreenSizeInfo($"MonoBehaviour.<b>OnDisable</b> ({modeText})", instance.size_OnDisable.x, instance.size_OnDisable.y);
            EditorGUILayout.LabelField($"elaposed time {(DateTime.Now - instance.lastDisableTime).ToString("ss")} [s]");


            // Editor
            DrawScreenSizeInfo($"Editor.OnInspector", Screen.width, Screen.height);

            EditorUtil.GUI.HorizontalLine();
            // -------------------

            // インスペクタが表示されている画面の解像度
            EditorGUILayout.LabelField("Current Resolution", $"{Screen.currentResolution.width} x {Screen.currentResolution.height} @ {Screen.currentResolution.refreshRate}Hz");

            // 登録されている全ての解像度
            EditorGUILayout.LabelField("Supported Resolutions:");
            using (new EditorGUI.IndentLevelScope()) {
                foreach (var resolution in Screen.resolutions) {
                    EditorGUILayout.LabelField($"{resolution.width} x {resolution.height} @ {resolution.refreshRate}Hz");
                }
            }
        }


        private void DrawScreenSizeInfo(string labelText, float width, float height) {
            EditorUtil.GUI.HorizontalLine();

            // SCREEN SIZE
            EditorGUILayout.LabelField(labelText, Styles.label);
            using (new EditorGUILayout.VerticalScope(GUI.skin.box))
            using (new EditorGUI.IndentLevelScope()) {
                EditorGUILayout.LabelField("Screen Width", width.ToString(), Styles.label);
                EditorGUILayout.LabelField("Screen Height", height.ToString(), Styles.label);
            }
        }


        private static class Styles {
            public static GUIStyle label;
            static Styles() {
                label = new GUIStyle(GUI.skin.label) {
                    richText = true,
                };
            }
        }
    }
#endif

}