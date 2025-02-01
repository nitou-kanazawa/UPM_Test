using UnityEngine;
using UnityEngine.UI;

// [REF]
//  _: Image クラスの拡張メソッド https://kazupon.org/unity-image-extension/
//  _: 2DRPG開発日誌 #79 ImageのFill方向をスクリプトから変更する https://kitty-pool.com/ss079/

namespace Nitou{

    /// <summary>
    /// <see cref="Image"/>型の基本的な拡張メソッド集．
    /// </summary>
    public static partial class ImageExtensions {

        /// ----------------------------------------------------------------------------
        // Fill 

        /// <summary>
        /// Fill Originを設定する拡張メソッド．
        /// </summary>
        public static void SetFillMethodOrigin(this Image self, Image.OriginHorizontal origin) {
            self.fillOrigin = (int)origin;
        }

        /// <summary>
        /// Fill Originを設定する拡張メソッド．
        /// </summary>
        public static void SetFillMethodOrigin(this Image self, Image.OriginVertical origin) {
            self.fillOrigin = (int)origin;
        }

        /// <summary>
        /// Fill Originを設定する拡張メソッド．
        /// </summary>
        public static void SetFillMethodOrigin(this Image self, Image.Origin90 origin) {
            self.fillOrigin = (int)origin;
        }

        /// <summary>
        /// Fill Originを設定する拡張メソッド．
        /// </summary>
        public static void SetFillMethodOrigin(this Image self, Image.Origin360 origin) {
            self.fillOrigin = (int)origin;
        }

        /// <summary>
        /// Horizontal Fillに設定する拡張メソッド．
        /// </summary>
        public static void SetHorizontalFillMode(this Image self, Image.OriginHorizontal origin) {
            self.type = Image.Type.Filled;
            self.fillMethod = Image.FillMethod.Horizontal;
            self.fillOrigin = (int)origin;
        }

        /// <summary>
        /// Vertical Fillに設定する拡張メソッド．
        /// </summary>
        public static void SetVerticalFillMode(this Image self, Image.OriginVertical origin) {
            self.type = Image.Type.Filled;
            self.fillMethod = Image.FillMethod.Vertical;
            self.fillOrigin = (int)origin;
        }


        /// ----------------------------------------------------------------------------
        // sprite

        /// <summary>
        /// ImageのSpriteに、テクスチャを設定する拡張メソッド．
        /// </summary>
        public static void SetTexture2D(this Image self, Texture2D tex2D) {
            if (tex2D != null) {
                self.sprite = Sprite.Create(tex2D, new Rect(0, 0, tex2D.width, tex2D.height), Vector2.zero);
            } else {
                Debug_.LogWarning("テクスチャが割り当てられていません。spriteにnullを設定します。");
                self.sprite = null;
            }
        }

    }
}
