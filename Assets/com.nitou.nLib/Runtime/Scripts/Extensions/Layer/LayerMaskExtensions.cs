using UnityEngine;

namespace Nitou {

    /// <summary>
    /// <see cref="LayerMask"/>型の基本的な拡張メソッド集．
    /// </summary>
    public static class LayerMaskExtensions {

        /// ----------------------------------------------------------------------------
        #region Layerの判定

        /// <summary>
        /// LayerMaskに指定したレイヤーが含まれているかどうかを調べる拡張メソッド
        /// </summary>
        public static bool Contains(this LayerMask self, int layerId) {
            return ((1 << layerId) & self) != 0;
        }

        /// <summary>
        /// LayerMaskに指定したレイヤーが含まれているかどうかを調べる拡張メソッド
        /// </summary>
        public static bool Contains(this LayerMask self, string layerName) {
            return self.Contains(LayerMask.NameToLayer(layerName));
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region Layerの追加/削除

        /// <summary>
        /// LayerMaskに指定したレイヤーを追加する拡張メソッド
        /// </summary>
        public static LayerMask Add(this LayerMask self, LayerMask layerId) {
            return self | (1 << layerId);
        }

        /// <summary>
        /// LayerMaskに指定したレイヤーを追加する拡張メソッド
        /// </summary>
        public static LayerMask Add(this LayerMask self, string layerName) {
            return self.Add(LayerMask.NameToLayer(layerName));
        }

        /// <summary>
        /// LayerMaskから指定したレイヤーを削除する拡張メソッド
        /// </summary>
        public static LayerMask Remove(this LayerMask self, LayerMask layerId) {
            return self & ~(1 << layerId);
        }

        /// <summary>
        /// LayerMaskから指定したレイヤーを削除する拡張メソッド
        /// </summary>
        public static LayerMask Remove(this LayerMask self, string layerName) {
            return self.Remove(LayerMask.NameToLayer(layerName));
        }

        /// <summary>
        /// LayerMaskに指定したレイヤーを追加/削除の切り替え
        /// </summary>
        public static LayerMask Toggle(this LayerMask self, LayerMask layerId) {
            return self ^ (1 << layerId);
        }

        /// <summary>
        /// LayerMaskに指定したレイヤーを追加/削除の切り替え
        /// </summary>
        public static LayerMask Toggle(this LayerMask self, string layerName) {
            return self.Toggle(LayerMask.NameToLayer(layerName));
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region その他

        /// <summary>
        /// 
        /// </summary>
        public static string ToBinaryString(this LayerMask self) {
            // LayerMask の値を取得し、2進数の文字列に変換する
            return System.Convert.ToString(self.value, 2).PadLeft(32, '0');
        }

        #endregion
    }
}
