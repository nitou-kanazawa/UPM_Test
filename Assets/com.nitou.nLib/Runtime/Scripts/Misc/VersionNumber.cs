using System.Linq;
using System.Collections;
using System.Collections.Generic;

// [参考]
//  コガネブログ: バージョン番号を管理する構造体の例

namespace Nitou {

    /// <summary>
    /// プロジェクトのバージョン番号を管理する構造体
    /// </summary>
    public readonly struct VersionNumber {

        private readonly string _version;

        public int Major { get; }
        public int Minor { get; }
        public int Patch { get; }


        /// ----------------------------------------------------------------------------
        // Public Methord

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public VersionNumber(string version) {

            var array = version
                .Split(".")
                .Select(x => int.TryParse(x, out int result) ? result : default)
                .ToArray();

            Major = array.ElementAtOrDefault(0);
            Minor = array.ElementAtOrDefault(1);
            Patch = array.ElementAtOrDefault(2);
            _version = $"{Major}.{Minor}.{Patch}";
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public VersionNumber(int major, int minor, int patch) {
            Major = major;
            Minor = minor;
            Patch = patch;
            _version = $"{Major}.{Minor}.{Patch}";
        }

        /// <summary>
        /// 暗黙的キャスト
        /// </summary>
        public static implicit operator string(in VersionNumber versionNumber) {
            return versionNumber._version;
        }

        /// <summary>
        /// 文字列への変換
        /// </summary>
        public override string ToString() {
            return _version;
        }

    }
}
