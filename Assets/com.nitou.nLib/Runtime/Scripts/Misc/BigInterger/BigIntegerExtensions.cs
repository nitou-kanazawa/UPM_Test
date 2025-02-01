using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

// [参考]
//  PG日誌: 放置ゲームやクリッカーゲームに出てくる単位を表現する https://takap-tech.com/entry/2023/03/25/235545

namespace Nitou {

    /// <summary>
    /// 単位を管理するクラス
    /// </summary>
    public class UnitMgr {

        // 最大の桁数
        private readonly int _maxDigit = 1000;
        // 単位のリスト
        private static string[] _units;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UnitMgr(int maxDigit) {
            _maxDigit = maxDigit;
        }

        /// <summary>
        /// 単位を初期化する
        /// </summary>
        private void Init() {
            if (_units == null) // 一回目に初期化
            {
                _units = CreateUnits(_maxDigit).ToArray();
            }
        }


        /// ----------------------------------------------------------------------------
        // Public Method (単位の取得)

        /// <summary>
        /// 指定した位置の単位を取得する
        /// </summary>
        public string GetUnit(int index) {
            Init();
            if (index >= _units.Length) {
                return "ERROR";
            }
            return _units[index];
        }

        /// <summary>
        /// 指定した位置の単位をSpanとして取得する
        /// </summary>
        public ReadOnlySpan<char> GetUnitSpan(int index) {
            Init();
            if (index >= _units.Length) {
                //Trace.WriteLine("Overflow");
                return "ERROR";
            }
            return _units[index].AsSpan();
        }


        /// ----------------------------------------------------------------------------
        // Public Method (単位の設定)

        /// <summary>
        /// 指定した数の分だけ単位を生成する
        /// </summary>
        private IEnumerable<string> CreateUnits(int count) {
            var sb = new StringBuilder();
            string f(int i) {
                sb.Clear();
                int d = i;
                while (d > 0) {
                    int mod = (d - 1) % 26;
                    sb.Insert(0, Convert.ToChar(97 + mod));
                    d = (d - mod) / 26;
                }
                return sb.ToString();
            }
            yield return "";
            yield return "K";
            yield return "M";
            yield return "B";
            yield return "T";
            for (int i = 1; i < count - 4; i++) {
                yield return f(i);
            }
        }
    }



    public static partial class BigIntegerExtensions {

        // サポートする最大の桁数
        private const int MaxDigit = 1024; // 10^512まで
        private const char Dot = '.';
        private const string Error = "ERROR";

        // 単位のリスト
        private static readonly UnitMgr conv = new (MaxDigit);

        // 1234564789 → 1.234b のような形式に変換する
        // ** Spanを用いて可能な限りメモリアロケーションを抑えるように実装している
        public static string ToReadableString(this BigInteger src) {

            Span<char> buffer = stackalloc char[MaxDigit];
            if (!src.TryFormat(buffer, out int length)) {
                return Error; // サポートしてる桁数を超えた
            }
            if (length < 7) // 7桁未満はそのまま数字を返す
            {
                return buffer.Slice(0, length).ToString();
            }

            // 7桁以上は桁数に応じて加工する
            int _len = length - 1;
            ReadOnlySpan<char> unitSpan = conv.GetUnitSpan(_len / 3);
            int d = _len % 3;

            // 結果を入れるいれもの
            Span<char> result = stackalloc char[5 + d + unitSpan.Length];

            d++;
            var a = buffer.Slice(0, d);
            var c = buffer.Slice(d, 3);

            a.CopyTo(result);
            result[a.Length] = Dot;
            c.CopyTo(result.Slice(a.Length + 1));
            unitSpan.CopyTo(result.Slice(a.Length + 1 + c.Length));
            return result.ToString();
        }
    }
}