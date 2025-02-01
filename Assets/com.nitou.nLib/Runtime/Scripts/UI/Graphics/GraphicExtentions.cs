using UnityEngine;
using UnityEngine.UI;

namespace Nitou{

    /// <summary>
    /// <see cref="Graphic"/>型の基本的な拡張メソッド集．
    /// </summary>
    public static partial class GraphicExtentions{

        /// ----------------------------------------------------------------------------
        #region カラー設定

        /// <summary>
        /// カラー値(0~1)を設定する拡張メソッド．
        /// </summary>
        public static void SetColor(this Graphic graphic, float r, float g, float b, float a) {
            graphic.color = new Color(r, g, b, a);
        }

        /// <summary>
        /// カラー値(0~255)を設定する拡張メソッド．
        /// </summary>
        public static void SetColor32(this Graphic graphic, byte r, byte g, byte b, byte a) {
            graphic.color = new Color32(r, g, b, a);
        }

        /// <summary>
        /// RGB値を設定する拡張メソッド．
        /// </summary>
        public static void SetRGB(this Graphic graphic, float r, float g, float b) {
            var color = graphic.color;
            color.r = r;
            color.g = g;
            color.b = b;
            graphic.color = color;
        }

        /// <summary>
        /// R値を設定する拡張メソッド．
        /// </summary>
        public static void SetR(this Graphic graphic, float r) {
            var color = graphic.color;
            color.r = r;
            graphic.color = color;
        }

        /// <summary>
        /// G値を設定する拡張メソッド．
        /// </summary>
        public static void SetG(this Graphic graphic, float g) {
            var color = graphic.color;
            color.g = g;
            graphic.color = color;
        }

        /// <summary>
        /// B値を設定する拡張メソッド．
        /// </summary>
        public static void SetB(this Graphic graphic, float b) {
            var color = graphic.color;
            color.b = b;
            graphic.color = color;
        }

        /// <summary>
        /// α値を設定する拡張メソッド．
        /// </summary>
        public static void SetAlpha(this Graphic graphic, float alpha) {
            var color = graphic.color;
            color.a = alpha;
            graphic.color = color;
        }
        #endregion

    }
}
