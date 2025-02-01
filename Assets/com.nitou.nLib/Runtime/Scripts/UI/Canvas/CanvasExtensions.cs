using UnityEngine;

namespace Nitou {

    /// <summary>
    /// <see cref="Canvas"/>型の基本的な拡張メソッド集．
    /// </summary>
    public static class CanvasExtensions {

        // ----------------------------------------------------------------------------
        #region 座標

        /// <summary>
        /// スクリーン座標を取得する拡張メソッド．
        /// </summary>
        public static Vector2 GetScreenPosition(this Canvas canvas, Vector3 worldPos, Camera camera = null) {

            // スクリーン座標
            return canvas.renderMode switch {
                RenderMode.ScreenSpaceOverlay => worldPos,
                RenderMode.ScreenSpaceCamera => RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, worldPos),
                RenderMode.WorldSpace => RectTransformUtility.WorldToScreenPoint(camera ?? Camera.main, worldPos),
                _ => throw new System.NotImplementedException()
            };
        }

        /// <summary>
        /// スクリーン座標を取得する拡張メソッド．
        /// </summary>
        public static Rect GetScreenRect(this Canvas canvas, Vector3 worldMin, Vector3 worldMax, Camera camera = null) {
            Vector2 screenMin, screenMax;

            // スクリーン座標
            switch (canvas.renderMode) {
                case RenderMode.ScreenSpaceOverlay:
                    screenMin = worldMin;
                    screenMax = worldMax;
                    break;
                case RenderMode.ScreenSpaceCamera:
                    screenMin = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, worldMin);
                    screenMax = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, worldMax);
                    break;
                case RenderMode.WorldSpace:
                    camera ??= Camera.main;
                    screenMin = RectTransformUtility.WorldToScreenPoint(camera, worldMin);
                    screenMax = RectTransformUtility.WorldToScreenPoint(camera, worldMax);
                    break;
                default:
                    throw new System.NotImplementedException();
            }

            return RectUtils.MinMaxRect(screenMin, screenMax);
        }

        /// <summary>
        /// ビューポート座標に変換する拡張メソッド．
        /// </summary>
        public static Vector2 GetViewportPosition(this Canvas canvas, Vector3 worldPos, Camera camera = null) {
            return canvas.renderMode switch {
                RenderMode.ScreenSpaceOverlay => new Vector2(worldPos.x / Screen.width, worldPos.y / Screen.height),
                RenderMode.ScreenSpaceCamera => canvas.worldCamera.WorldToViewportPoint(worldPos),
                RenderMode.WorldSpace => (camera ?? Camera.main).WorldToViewportPoint(worldPos),
                _ => throw new System.NotImplementedException()
            };
        }

        /// <summary>
        /// ビューポート座標に変換する拡張メソッド．
        /// </summary>
        public static Rect GetViewportRect(this Canvas canvas, Vector3 worldMin, Vector3 worldMax, Camera camera = null) {
            Vector2 viewportMin, viewportMax;

            // ビューポート座標
            switch (canvas.renderMode) {
                case RenderMode.ScreenSpaceOverlay:
                    (var screenMin, var screenMax) = (worldMin, worldMax);
                    viewportMin = new Vector2(screenMin.x / Screen.width, screenMin.y / Screen.height);
                    viewportMax = new Vector2(screenMax.x / Screen.width, screenMax.y / Screen.height);
                    break;
                case RenderMode.ScreenSpaceCamera:
                    viewportMin = canvas.worldCamera.WorldToViewportPoint(worldMin);
                    viewportMax = canvas.worldCamera.WorldToViewportPoint(worldMax);
                    break;
                case RenderMode.WorldSpace:
                    camera ??= Camera.main;
                    viewportMin = camera.WorldToViewportPoint(worldMin);
                    viewportMax = camera.WorldToViewportPoint(worldMax);
                    break;
                default:
                    throw new System.NotImplementedException();
            }

            return RectUtils.MinMaxRect(viewportMin, viewportMax);
        }
        #endregion
    }
}
