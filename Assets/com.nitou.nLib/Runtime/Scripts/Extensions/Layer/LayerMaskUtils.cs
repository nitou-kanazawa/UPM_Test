using UnityEngine;

// [REF]
//  _: UnityでLayerMaskを操作する方法色々 https://12px.com/blog/2021/11/layermask/
//  Hatena: 物理演算、衝突判定、コライダーの検出などで使うLayerMaskについて https://indie-game-creation-with-unity.hatenablog.com/entry/layer-mask

namespace Nitou {

    /// <summary>
    /// <see cref="LayerMask"/>型を対象とした汎用メソッド集．
    /// </summary>
    public static class LayerMaskUtils {

        /// ----------------------------------------------------------------------------
        // Public Method　(LayerMAskの生成)

        /// <summary>
        /// 全て入った<see cref="LayerMask"/>．
        /// </summary>
        public static LayerMask AllIn => -1;

        /// <summary>
        /// 空の<see cref="LayerMask"/>．
        /// </summary>
        public static LayerMask Empty => 1;

        /// <summary>
        /// 特定レイヤーのみ入った<see cref="LayerMask"/>．
        /// </summary>
        public static LayerMask Only(int layer) => 1 << layer;

        /// <summary>
        /// 特定レイヤーのみ入った<see cref="LayerMask"/>．
        /// </summary>
        public static LayerMask Only(string layerName) => 1 << LayerMask.NameToLayer(layerName);

        /// <summary>
        /// "Default"のみ入った<see cref="LayerMask"/>．
        /// </summary>
        public static LayerMask OnlyDefault() => Only("Default");   // ※0でも可
    }

}
