using System.IO;
using UnityEngine;

namespace Nitou.Tools
{
    public static class CodeTemplate{


        public static readonly string CODE_TEMPLATE = @"
            public class #CLASS_NAME#{
              public void #METHOD_NAME#() {
              }
            }
            ";

        public static string FromFile(string templatePath) {

            // テンプレートファイルの読み込み
            if (!File.Exists(templatePath)) {
                Debug.LogError($"テンプレートファイルが見つかりません: {templatePath}");
                return null;
            }

            return File.ReadAllText(templatePath);
        }
    }
}
