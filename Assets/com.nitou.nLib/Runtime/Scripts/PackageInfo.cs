using UnityEngine;

namespace Nitou.Shared {

    /// <summary>
    /// パッケージの各種設定を管理する静的クラス
    /// </summary>
    internal static class PackageInfo {

        /// <summary>
        /// パッケージのディレクトリパス
        /// </summary>
        internal static readonly PackageDirectoryPath packagePath = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        static PackageInfo() {
            packagePath = new PackageDirectoryPath("com.nitou.nLib");
        }
    }
}
