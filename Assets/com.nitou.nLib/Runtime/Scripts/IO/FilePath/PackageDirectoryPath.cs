using System.IO;
using UnityEngine;

namespace Nitou {

    /// <summary>
    /// 自作パッケージのディレクトリパス指定用のクラス
    /// </summary>
    [System.Serializable]
    public sealed class PackageDirectoryPath : IUnityProjectPath{

        public enum Mode {
            // 配布後
            Upm,
            // 開発プロジェクト内
            Normal,
            // 
            NotExist,
        }

        // 相対パス
        private readonly string _upmRelativePath;
        private readonly string _normalRelativePath;

        private Mode _mode;

        /// <summary>
        /// Package配布後のパッケージパス
        /// </summary>
        public string UpmPath => $"Packages/{_upmRelativePath}";

        /// <summary>
        /// 開発プロジェクトでのアセットパス
        /// </summary>
        public string NormalPath => $"Assets/{_normalRelativePath}".ToAssetsPath();


        /// ----------------------------------------------------------------------------
        // Pubic Method

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PackageDirectoryPath(string relativePath = "com.nitou.nLib") {
            _upmRelativePath = relativePath;
            _normalRelativePath = relativePath;

            // 判定する
            _mode = CheckDirectoryLocation();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PackageDirectoryPath(string upmRelativePath = "com.nitou.nLib", string normalRelativePath = "Plugins/com.nitou.nLib") {
            _upmRelativePath = upmRelativePath;
            _normalRelativePath = normalRelativePath;

            // 判定する
            _mode = CheckDirectoryLocation();
        }


        /// ----------------------------------------------------------------------------
        // Pubic Method

        /// <summary>
        /// Projectディレクトリを起点としたパス
        /// </summary>
        public string ToProjectPath() {
            return _mode switch {
                Mode.Upm => UpmPath,
                Mode.Normal => NormalPath,
                _ => ""
            };
        }

        /// <summary>
        /// 絶対パス
        /// </summary>
        public string ToAbsolutePath() => Path.GetFullPath(ToProjectPath());


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// ディレクトリの位置を判定する
        /// </summary>
        private Mode CheckDirectoryLocation() {

            if (Directory.Exists(UpmPath)) return Mode.Upm;
            if (Directory.Exists(NormalPath)) return Mode.Normal;

            Debug.LogError($"Directory not found in both UPM and normal paths: \n" +
                    $"  [{UpmPath}] and \n" +
                    $"  [{NormalPath}]");
            return Mode.NotExist;
        }
    }
}
