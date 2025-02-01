using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

// [REF]
//  コガネブログ: 区切り文字にスラッシュを使用して指定したディレクトリ内のファイル名を返す関数 https://baba-s.hatenablog.com/entry/2015/07/29/100000
//  _: C#ファイル／フォルダ操作術。すぐに使えるサンプルコード付き https://resanaplaza.com/2024/02/23/%E3%80%90%E5%AE%9F%E8%B7%B5%E3%80%91c%E3%83%95%E3%82%A1%E3%82%A4%E3%83%AB%EF%BC%8F%E3%83%95%E3%82%A9%E3%83%AB%E3%83%80%E6%93%8D%E4%BD%9C%E8%A1%93%E3%80%82%E3%81%99%E3%81%90%E3%81%AB%E4%BD%BF%E3%81%88/#google_vignette

namespace Nitou {

    /// <summary>
    /// ディレクトリ操作に関する汎用メソッド集．
    /// </summary>
    public static class DirectoryUtils {

        /// ----------------------------------------------------------------------------
        #region 判定

        /// <summary>
        /// ディレクトリ存在チェック．
        /// </summary>
        public static void ExistsWithExp(string path) {
            if (!Directory.Exists(path)) {
                throw new DirectoryNotFoundException("Directory is not exist :" + path);
            }
        }

        /// <summary>
        /// ディレクトリ存在チェック．
        /// </summary>
        public static void ExistsWithExp(IEnumerable<string> paths) {
            paths.ForEach(ExistsWithExp);
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region 

        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<string> EnumerateDirectoryNames(string path) {
            return Directory
                .EnumerateDirectories(path)
                .Select(dirPath => Path.GetFileName(dirPath));
        }

        #endregion


        /// ----------------------------------------------------------------------------
        #region 操作（コピー）

        //// [REF] Microsoft Learn: ディレクトリをコピーする https://learn.microsoft.com/ja-jp/dotnet/standard/io/how-to-copy-directories

        /// <summary>
        /// ディレクトリを再帰的にコピーする．
        /// </summary>
        public static void CopyDirectory(string sourceDir, string destDir) {

            // コピー先のフォルダが存在しない場合は作成する
            if (!Directory.Exists(destDir)) {
                Directory.CreateDirectory(destDir);
            }

            // Copy files
            foreach (string file in Directory.GetFiles(sourceDir)) {
                try {
                    string destFile = Path.Combine(destDir, Path.GetFileName(file));
                    File.Copy(file, destFile, true); 
                } catch (Exception ex) {
                    // エラーが発生した場合はエラーメッセージを表示して処理を継続する
                    Debug_.LogWarning("ファイルのコピー中にエラーが発生しました: " + ex.Message);
                }
            }

            // Copy directories
            foreach (string folder in Directory.GetDirectories(sourceDir)) {
                try {
                    string destFolder = Path.Combine(destDir, Path.GetFileName(folder));
                    CopyDirectory(folder, destFolder); // サブフォルダを再帰的にコピーする
                } catch (Exception ex) {
                    // エラーが発生した場合はエラーメッセージを表示して処理を継続する
                    Debug_.LogWarning("フォルダのコピー中にエラーが発生しました: " + ex.Message);
                }
            }
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        public static void Clear(string path) {
            Directory.Delete(path);
            Directory.CreateDirectory(path);
        }

        /// <summary>
        /// ディレクトリ検索．
        /// </summary>
        public static List<string> Find(string directoryPath) {
            ExistsWithExp(directoryPath);
            return Directory.GetDirectories(directoryPath).ToList();
        }


        //public static string GetUniqDirectoryName() {


        //}

        /// <summary>
        /// <para>指定したディレクトリ内のファイルの名前 (パスを含む) を返します</para>
        /// <para>パスの区切り文字は「\\」ではなく「/」です</para>
        /// </summary>
        public static string[] GetFiles(string path) {
            return Directory
                .GetFiles(path)
                .Select(c => c.Replace("\\", "/"))
                .ToArray();
        }

        /// <summary>
        /// <para>指定したディレクトリ内の指定した検索パターンに一致するファイル名 (パスを含む) を返します</para>
        /// <para>パスの区切り文字は「\\」ではなく「/」です</para>
        /// </summary>
        public static string[] GetFiles(string path, string searchPattern) {
            return Directory
                .GetFiles(path, searchPattern)
                .Select(c => c.Replace("\\", "/"))
                .ToArray();
        }

        /// <summary>
        /// <para>指定したディレクトリの中から、指定した検索パターンに一致し、</para>
        /// <para>サブディレクトリを検索するかどうかを決定する値を持つファイル名 (パスを含む) を返します</para>
        /// <para>パスの区切り文字は「\\」ではなく「/」です</para>
        /// </summary>
        public static string[] GetFiles(string path, string searchPattern, SearchOption searchOption) {
            return Directory
                .GetFiles(path, searchPattern, searchOption)
                .Select(c => c.Replace("\\", "/"))
                .ToArray();
        }
    }
}
