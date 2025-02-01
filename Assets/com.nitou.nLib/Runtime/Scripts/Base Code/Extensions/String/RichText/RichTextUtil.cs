using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [参考]
//  Document : サポートされるリッチテキストタグ https://docs.unity3d.com/ja/2022.3/Manual/UIE-supported-tags.html
//  _: TextMeshProのリッチテキストタグ一覧 https://madnesslabo.net/utage/?page_id=12903
//  _: TextMeshPro で使えるリッチテキストタグまとめ https://www.midnightunity.net/textmeshpro-richtext-tags/#google_vignette

namespace Nitou.RichText {

    /// <summary>
    /// 文字列をリッチテキストへ変換する拡張メソッド集
    /// </summary>
    public static class RichTextUtil {

        /// <summary>
        /// カラータグを挿入する
        /// </summary>
        public static string WithColorTag(this string self, Color color) {
            return string.Format("<color=#{0:X2}{1:X2}{2:X2}>{3}</color>",
                (byte)(color.r * 255f),
                (byte)(color.g * 255f),
                (byte)(color.b * 255f),
                self
            );
        }

        /// <summary>
        /// 太字タグを挿入する
        /// </summary>
        public static string WithBoldTag(this string self) {
            return $"<b>{self}</b>";
        }

        /// <summary>
        /// 斜体タグを挿入する
        /// </summary>
        public static string WithItalicTag(this string self) {
            return $"<i>{self}</i>";
        }

        /// <summary>
        /// アンダーラインタグを挿入する
        /// </summary>
        public static string WithUnderlineTag(this string self) {
            return $"<u>{self}</u>";
        }

        /// <summary>
        /// 取り消し線タグを挿入する
        /// </summary>
        public static string WithStrikethroughTag(this string self) {
            return $"<s>{self}</s>";
        }

        /// <summary>
        /// サイズタグを挿入する
        /// </summary>
        public static string WithSizeTag(this string self, int size) {
            return $"<size={size}>{self}</size>";
        }

        /// <summary>
        /// インデントタグを挿入する
        /// </summary>
        public static string WithIndentTag(this string self, int charNum = 1) {
            return $"<indent={Mathf.Clamp(charNum, 1, 20)}em>{self}</indent>";
        }
    }
}