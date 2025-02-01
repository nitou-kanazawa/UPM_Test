using UnityEngine;
using Nitou;

// [参考]
//  ねこじゃらシティ: RectTransformのサイズをスクリプトから変更する https://nekojara.city/unity-rect-transform-size

namespace DG.Tweening{

    /// <summary>
    /// RectTransformのTweenライブラリクラス
    /// </summary>
    public static class RectTransformTweenExtensions{

        /// <summary>
        /// アンカーを固定させてSizeDeltaをアニメーションさせる拡張メソッド
        /// </summary>
        public static Tweener DOSizeDeltaWithCurrentAnchers(this RectTransform self, Vector2 endValue, float duration) {
            return DOTween.To(
                () => self.sizeDelta, 
                x => self.SetSize(x), 
                endValue, 
                duration
            );
        }
        
    }
}
