using UnityEngine;
using UnityEngine.UI;

namespace Nitou.UI {

    public static class UIHelper {

        private static readonly Vector2Int DEFAULT_RESOLUTION = new Vector2Int(1920, 1080);


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// UIキャンバスを持つGameObjectを生成する．
        /// </summary>
        public static Canvas CreateCanvas(string name, int sortingOrder = 0, bool needRayCaster = false) {
            var canvasObj = new GameObject(name);

            // Canvas
            var canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = sortingOrder;

            // Scaler
            var scaler = canvasObj.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            //scaler.referenceResolution = ProjectSettingsSO.Instance.ReferenceResolution;
            scaler.referenceResolution = DEFAULT_RESOLUTION;
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 1;          // ※横画面アプリを想定
            scaler.referencePixelsPerUnit = 100;

            // Graphics RayCaster
            if (needRayCaster) {
                var rayCaster = canvasObj.AddComponent<GraphicRaycaster>();
            }

            return canvas;
        }

        /// <summary>
        /// RectTransformの子要素を生成する拡張メソッド．
        /// </summary>
        public static RectTransform CreateChildRect(this RectTransform parent, string name) {
            var rect = new GameObject(name).AddComponent<RectTransform>();

            // ※デフォルトでは親と同じサイズにしておく
            rect.transform.SetParent(parent, false);
            rect.SetRectBasedOnParentEdiges(0, 0, 0, 0);

            return rect;
        }
    }

}
