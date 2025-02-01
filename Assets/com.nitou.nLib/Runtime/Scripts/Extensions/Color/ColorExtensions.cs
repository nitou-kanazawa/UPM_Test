using UnityEngine;
using UnityEngine.UI;

// [REF]
//  ゲームUIネット : DOTweenで作成したモーション17個を含むプロジェクトを公開 https://game-ui.net/?p=975
//  コガネブログ: Color の代入を簡略化する Deconstruction https://baba-s.hatenablog.com/entry/2019/09/03/230300
//  _: Imageの色それぞれ変更する拡張 https://hi-network.sakura.ne.jp/wp/2021/01/26/post-3660/

namespace Nitou {

    /// <summary>
    /// <see cref="Color"/>型の基本的な拡張メソッド集．
    /// </summary>
    public static partial class ColorExtensions {

        /// <summary>
        /// デコンストラクタ．
        /// </summary>
        public static void Deconstruct(this Color self, out float r, out float g, out float b) {
            r = self.r;
            g = self.g;
            b = self.b;
        }

        /// <summary>
        /// デコンストラクタ．
        /// </summary>
        public static void Deconstruct(this Color self, out float r, out float g, out float b, out float a) {
            r = self.r;
            g = self.g;
            b = self.b;
            a = self.a;
        }


        /// ----------------------------------------------------------------------------


        public static Color WithRed(this Color self, float red) => new (red, self.g, self.b, self.a);
        public static Color WithGreen(this Color self, float green) => new (self.r, green, self.b, self.a);
        public static Color WithBlue(this Color self, float blue) => new (self.r, self.g, blue, self.a);
        public static Color WithAlpha(this Color self, float alpha) => new (self.r, self.g, self.b, alpha);


        /// ----------------------------------------------------------------------------

        /// <summary>
        /// カラーを変換する拡張メソッド
        /// </summary>
        public static string ToHtmlStringRGB(this Color color) {
            return $"#{ColorUtility.ToHtmlStringRGB(color)}";
        }

        /// <summary>
        /// カラーを変換する拡張メソッド
        /// </summary>
        public static string ToHtmlStringRGBA(this Color color) {
            return $"#{ColorUtility.ToHtmlStringRGBA(color)}";
        }


        /// ----------------------------------------------------------------------------
        #region Misc

        // [TODO] どのクラスに含めるか？また，そもそも必要か検討するべき．

        /// <summary>
        /// α値を設定する拡張メソッド
        /// </summary>
        public static void SetAlpha(this SpriteRenderer spriteRenderer, float alpha) {
            var color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }

        /// <summary>
        /// α値を設定する拡張メソッド
        /// </summary>
        public static void SetAlphasInChildren(this GameObject obj, float alpha) {
            var spriteRenderers = obj.GetComponentsInChildren<SpriteRenderer>();
            var graphics = obj.GetComponentsInChildren<Graphic>();

            if (spriteRenderers != null) {
                foreach (var spriteRenderer in spriteRenderers) {
                    spriteRenderer.SetAlpha(alpha);
                }
            }

            if (graphics != null) {
                foreach (var graphic in graphics) {
                    graphic.SetAlpha(alpha);
                }
            }
        }
        #endregion
    }
}
