using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Nitou.RichText {

    /// <summary>
    /// XML文字列にRichTextタグを挿入する
    /// </summary>
    public static class XmlTextDecorator {

        // Define colors
        private static readonly string valueColor = Colors.White.ToHtmlString(); 
        private static readonly string specificTagColor = Colors.Cyan.ToHtmlString();

        // Regex pattern for text nodes
        private static string textNodePattern = @"(?<=>)[^<]*(?=<)";

        public static string ColorizeXml(string xml, string[] specificTags) {
            if (string.IsNullOrEmpty(xml)) return xml;

            // Colorize text nodes
            xml = Regex.Replace(xml, textNodePattern, textMatch => {
                return $"<color={valueColor}>{textMatch.Value}</color>";
            });

            // Generate regex pattern for specific tags
            string tagPattern = GenerateTagPattern(specificTags);

            // Colorize specific tags
            xml = Regex.Replace(xml, tagPattern, tagMatch => {
                return $"<color={specificTagColor}>{tagMatch.Value}</color>";
            });

            return xml;
        }

        private static string GenerateTagPattern(string[] tags) {
            // Escape special characters in tags and join them with | (OR operator)
            string escapedTags = string.Join("|", tags.Select(Regex.Escape));
            return $@"<\/?({escapedTags})\b[^>]*>";
        }
    }

    public static class ColorExtensions {

        /// <summary>
        /// カラーを変換する拡張メソッド
        /// </summary>
        public static string ToHtmlString(this Color color) {
            return $"#{ColorUtility.ToHtmlStringRGB(color)}";
        }
    }
}
