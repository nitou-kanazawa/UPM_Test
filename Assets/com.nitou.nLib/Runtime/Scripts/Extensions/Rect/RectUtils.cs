using UnityEngine;

namespace Nitou {

    /// <summary>
    /// <see cref="Rect"/>に関する汎用メソッド集
    /// </summary>
    public static class RectUtils {

        /// ----------------------------------------------------------------------------
        // 相対座標

        /// <summary>
        /// 基準<see cref="Rect"/>に対する相対位置を取得する．
        /// </summary>
        public static Vector2 GetRelativePosition(Vector2 targetPos, Rect baseRect) {
            float x = (targetPos.x - baseRect.x) / baseRect.width;
            float y = (targetPos.y - baseRect.y) / baseRect.height;
            return new Vector2(x, y);
        }

        /// <summary>
        /// 基準<see cref="Rect"/>に対する相対サイズを取得する．
        /// </summary>
        public static Vector2 GetRelativeSize(Vector2 targetSize, Rect baseRect) {
            float width = targetSize.x / baseRect.width;
            float height = targetSize.y / baseRect.height;
            return new Vector2(width, height);
        }

        /// <summary>
        /// 基準<see cref="Rect"/>に対する相対位置・サイズを取得する．
        /// </summary>
        public static Rect GetRelativeRect(Rect targetRect, Rect baseRect) {
            Vector2 position = GetRelativePosition(targetRect.min, baseRect);
            Vector2 size = GetRelativeSize(targetRect.size, baseRect);
            return new Rect(position, size);
        }


        /// ----------------------------------------------------------------------------
        // Factory

        /// <summary>
        /// 中心位置とサイズから<see cref="Rect"/>を生成する．
        /// </summary>
        public static Rect CenterSizeRect(Vector2 center, Vector2 size) {
            float x = center.x - size.x / 2;
            float y = center.y - size.y / 2;
            return new Rect(x, y, size.x, size.y);
        }

        /// <summary>
        /// 最小・最大点からRectを生成する．
        /// </summary>
        public static Rect MinMaxRect(Vector2 min, Vector2 max) {
            return Rect.MinMaxRect(min.x, min.y, max.x, max.y);
        }
    }
}
