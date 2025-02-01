using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

// [REF]
// _: ファイル・ディレクトリ関連util https://ameblo.jp/ka-neda/entry-12779824591.html

namespace Nitou {
    
    /// <summary>
    /// ファイル操作に関する汎用メソッド集．
    /// </summary>
    public static class FileUtils {

        /// ----------------------------------------------------------------------------
        #region 判定

        /// <summary>
        /// ファイルの存在チェック．
        /// </summary>
        public static void ExistsWithExp(string path) {
            if (!File.Exists(path)) {
                throw new DirectoryNotFoundException("File is not exist :" + path);
            }
        }

        /// <summary>
        /// ファイルの存在チェック．
        /// </summary>
        public static void ExistsWithExp(IEnumerable<string> paths) {
            paths.ForEach(ExistsWithExp);
        }
        #endregion

    }
}
