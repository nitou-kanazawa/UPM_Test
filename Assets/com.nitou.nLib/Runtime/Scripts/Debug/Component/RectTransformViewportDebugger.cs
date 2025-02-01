using UnityEngine;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
using Nitou.EditorShared;
#endif

namespace Nitou.DebugInternal {

    /// <summary>
    /// <see cref="RectTransform"/>のRect範囲をViewport情報として可視化するデバッグ用コンポーネント
    /// </summary>
    [RequireComponent(typeof(RequireComponent))]
    internal class RectTransformViewportDebugger : DebugComponent<RectTransform> {

        /// <summary>
        /// 描画モード
        /// </summary>
        public enum Mode {
            Screen,
            Viewport,
        }

        private RectTransform _rectTrans;
        private Canvas _canvas;

        //[Title("Mode")]
        [Indent, EnumToggleButtons] public Mode mode;
        
        
        public TextAnchor minAlignment = TextAnchor.UpperRight;
        public TextAnchor maxAlignment = TextAnchor.LowerLeft;

        [Min(1)] public int fontSize = 20;

        public Color _lineColor = Colors.Gray;
        
        public Color screenModeColor = Colors.Cyan;
        public Color viewportModeColor = Colors.Orange;


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method

        private void OnValidate() {
            _rectTrans = gameObject.GetComponent<RectTransform>();
        }

#if UNITY_EDITOR
        private void OnGUI() {
            if (_rectTrans == null) return;

            if (_canvas == null) {
                _canvas = _rectTrans.GetComponentInParent<Canvas>();
            }

            var rect = _rectTrans.GetScreenRect();

            //EditorUtil.ScreenGUI.Box(rect);

            var textColor = mode == Mode.Screen ? screenModeColor : viewportModeColor;
            (var minText, var maxText) = GetPositionString(mode);

            // Min point
            EditorUtil.ScreenGUI.AuxiliaryLine(rect.min, 2f, _lineColor);
            EditorUtil.ScreenGUI.Label(rect.min, minText, fontSize, minAlignment, textColor);

            // Max point
            EditorUtil.ScreenGUI.AuxiliaryLine(rect.max, 2f, _lineColor);
            EditorUtil.ScreenGUI.Label(rect.max, maxText, fontSize, maxAlignment, textColor);
        }
#endif


        /// ----------------------------------------------------------------------------
        // Private Method

        public (string min, string max) GetPositionString(Mode mode) {
            var rect = mode switch {
                Mode.Screen => _rectTrans.GetScreenRect(ref _canvas),
                Mode.Viewport => _rectTrans.GetViewportRect(ref _canvas),
                _ => throw new System.NotImplementedException()
            };

            return (rect.min.ToString("F2"), rect.max.ToString("F2"));
        }
    }
}
