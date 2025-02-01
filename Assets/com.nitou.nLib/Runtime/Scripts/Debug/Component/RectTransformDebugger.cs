using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.AnimatedValues;
using Nitou.EditorShared;
#endif

// [参考]
// ねこじゃらシティ: RectTransformのサイズをスクリプトから変更する https://nekojara.city/unity-rect-transform-size

namespace Nitou.DebugInternal {

    /// <summary>
    /// <see cref="RectTransform"/>の各プロパティを可視化するためのデバッグ用コンポーネント
    /// </summary>
    [RequireComponent(typeof(RequireComponent))]
    internal class RectTransformDebugger : DebugComponent<RectTransform> {

        private RectTransform _rectTrans;
        private Canvas _canvas;

        public RectTransform RectTrans => _rectTrans;

        public static bool showGui = true;


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method

        private void OnValidate() {
            _rectTrans = gameObject.GetComponent<RectTransform>();
        }

#if UNITY_EDITOR
        private void OnGUI() {
            if (_rectTrans == null || !showGui) return;

            var rect = _rectTrans.GetScreenRect(ref _canvas);

            var boxSize = Vector2.one * 100;
            var minRect = RectUtils.CenterSizeRect(rect.min, boxSize);
            var maxRect = RectUtils.CenterSizeRect(rect.max, boxSize);
            EditorUtil.ScreenGUI.Box(minRect);
            EditorUtil.ScreenGUI.Box(maxRect);

        }
#endif
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(RectTransformDebugger))]
    internal class RectTransformDebuggerEditor : Editor {

        // 計算用
        private static readonly Vector3[] _corners = new Vector3[4];

        // 描画用
        private AnimBool _positionAnim;
        private AnimBool _localCornersAnim;
        private AnimBool _globalCornersAnim;

        private void OnEnable() {
            EditorApplication.update += Repaint;

            _positionAnim = new AnimBool(false);
            _localCornersAnim = new AnimBool(false);
            _globalCornersAnim = new AnimBool(false);
            _positionAnim.valueChanged.AddListener(Repaint);
            _localCornersAnim.valueChanged.AddListener(Repaint);
            _globalCornersAnim.valueChanged.AddListener(Repaint);
        }

        private void OnDisable() {
            EditorApplication.update -= Repaint;
        }

        public override void OnInspectorGUI() {

            var instance = target as RectTransformDebugger;
            var rectTrans = instance.RectTrans;

            EditorUtil.GUI.MonoBehaviourField<RectTransformDebugger>(instance);

            if (rectTrans != null) {
                EditorGUILayout.LabelField("RectTransform Properties", EditorStyles.boldLabel);

                // Info
                Canvas canvas = null;
                DrawRectSizeInfo(rectTrans, ref canvas);

                // Properties
                DrawPoisitonProperty(rectTrans);
                DrawSizeProperty(rectTrans);
                DrawAnchorPivotProperty(rectTrans);

            } else {
                EditorGUILayout.HelpBox("Please assign a RectTransform.", MessageType.Warning);
            }
        }



        /// ----------------------------------------------------------------------------
        // Private Method (Inspector Drawer)

        private void DrawRectSizeInfo(RectTransform rectTransform, ref Canvas canvas) {
            using (new EditorGUILayout.VerticalScope(Styles.box))
            using (new EditorGUI.DisabledScope(true)) {
                // ビューポート座標
                EditorGUILayout.Vector2Field("Viewport", rectTransform.GetViewportPos(ref canvas));
            }

        }

        /// <summary>
        /// 位置関連のプロパティ
        /// </summary>
        private void DrawPoisitonProperty(RectTransform rectTrans) {

            using (var group = new EditorUtil.GUI.FoldoutGroupScope("Transform Info", _positionAnim)) {
                if (group.Visible) {

                    // Local
                    rectTrans.localPosition = EditorGUILayout.Vector3Field("Local Position", rectTrans.localPosition);
                    rectTrans.eulerAngles = EditorGUILayout.Vector3Field("Local Rotation", rectTrans.localRotation.eulerAngles);
                    rectTrans.localScale = EditorGUILayout.Vector3Field("Local Scale", rectTrans.localScale);

                    EditorUtil.GUI.HorizontalLine();

                    // Global
                    rectTrans.position = EditorGUILayout.Vector3Field("Global Position", rectTrans.position);
                    rectTrans.eulerAngles = EditorGUILayout.Vector3Field("Global Rotation", rectTrans.rotation.eulerAngles);
                }
            }

            //EditorGUILayout.Space();

            // Local Corners
            rectTrans.GetLocalCorners(_corners);
            using (var group = new EditorUtil.GUI.FoldoutGroupScope("Local Corners", _localCornersAnim)) {
                if (group.Visible) {
                    for (int i = 0; i < _corners.Length; i++) {
                        EditorGUILayout.Vector3Field($"Corner {i}", _corners[i]);
                    }
                }
            }

            // World Corners
            rectTrans.GetWorldCorners(_corners);
            using (var group = new EditorUtil.GUI.FoldoutGroupScope("World Corners", _globalCornersAnim)) {
                if (group.Visible) {
                    for (int i = 0; i < _corners.Length; i++) {
                        EditorGUILayout.Vector3Field($"Corner {i}", _corners[i]);
                    }
                }
            }

            EditorGUILayout.Space();
        }

        /// <summary>
        /// サイズ関連のプロパティ
        /// </summary>
        private void DrawSizeProperty(RectTransform rectTrans) {
            EditorUtil.GUI.HorizontalLine();
            using (new EditorGUILayout.VerticalScope(Styles.box)) {

                Color labelColor = rectTrans.IsFixedSize() ? Colors.Orange : Colors.Cyan;
                string label = rectTrans.IsFixedSize() ? "Fixed Size" : "Flexible Size";
                using (new EditorUtil.GUIColorScope(labelColor)) {
                    EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
                }
                using (new EditorGUI.IndentLevelScope()) {
                    EditorGUILayout.Vector2Field("Rect Pos", rectTrans.rect.position);
                    EditorGUILayout.Vector2Field("Rect Size", rectTrans.rect.size);
                    rectTrans.sizeDelta = EditorGUILayout.Vector2Field("Size Delta", rectTrans.sizeDelta);
                }

            }
            EditorGUILayout.Space();
        }

        /// <summary>
        /// アンカー関連のプロパティ
        /// </summary>
        private void DrawAnchorPivotProperty(RectTransform rectTrans) {
            EditorUtil.GUI.HorizontalLine();
            EditorGUILayout.Vector2Field("Anchor Min", rectTrans.anchorMin);
            EditorGUILayout.Vector2Field("Anchor Max", rectTrans.anchorMax);
            EditorGUILayout.Vector2Field("Anchored Position", rectTrans.anchoredPosition);
            EditorGUILayout.Vector2Field("Pivot", rectTrans.pivot);
        }


        /// ----------------------------------------------------------------------------
        // Private Method ()


        /// ----------------------------------------------------------------------------
        private static class Styles {
            public static readonly GUIStyle box;
            static Styles() {
                box = new GUIStyle(GUI.skin.box) {

                };
            }
        }
    }
#endif
}