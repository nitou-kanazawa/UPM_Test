using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

// [参考]
//  PG日誌 : Vector3(構造体)に自分自身の値を変更する拡張メソッドを定義する https://takap-tech.com/entry/2022/12/24/175039
//  コガネブログ:　Vector3 の代入を簡略化する Deconstruction　https://baba-s.hatenablog.com/entry/2019/09/03/230700

namespace Nitou {

    /// <summary>
    /// <see cref="Vector3"/>型の基本的な拡張メソッド集．
    /// </summary>
    public static partial class Vector3Extensions {

        /// <summary>
        /// デコンストラクタ．
        /// </summary>
        public static void Deconstruct(this Vector3 self, out float x, out float y, out float z) {
            x = self.x;
            y = self.y;
            z = self.z;
        }

        /// <summary>
        /// Vector2への変換する拡張メソッド．
        /// </summary>
        public static Vector2 ToVector2(this Vector3 self) {
                return new Vector2(self.x, self.y);
            }

        /// <summary>
        /// 要素同士の割り算を行う拡張メソッド．
        /// </summary>
        public static Vector3 Divide(this Vector3 self, Vector3 other) {
            return new Vector3(
                other.x != 0 ? self.x / other.x : 0,
                other.y != 0 ? self.y / other.y : 0,
                other.z != 0 ? self.z / other.z : 0
            );
        }

        /// <summary>
        /// 要素同士の割り算を行う拡張メソッド．
        /// </summary>
        public static Vector3 Divide(this Vector3 self, Vector3 other, float defaultValue) {
            return new Vector3(
                other.x != 0 ? self.x / other.x : defaultValue,
                other.y != 0 ? self.y / other.y : defaultValue,
                other.z != 0 ? self.z / other.z : defaultValue
            );
        }


        /// ----------------------------------------------------------------------------
        #region 要素の判定

        /// <summary>
        /// 最大の要素の値を取得する
        /// </summary>
        public static float MaxElement(this Vector3 self) {
            return Mathf.Max(self.x, self.y, self.z);
        }

        /// <summary>
        /// 最小の要素の値を取得する
        /// </summary>
        public static float MinElement(this Vector3 self) {
            return Mathf.Min(self.x, self.y, self.z);
        }

        /// <summary>
        /// 最大の要素の軸を取得する
        /// </summary>
        public static Axis MaxAxis(this Vector3 self) {
            // [NOTE] 値が等しい場合は(x , y , z)の順で優先される
            if (self.x >= self.y && self.x >= self.z) {
                return Axis.X;
            } else if (self.y >= self.x && self.y >= self.z) {
                return Axis.Y;
            } else {
                return Axis.Z;
            }
        }

        /// <summary>
        /// 最小の要素の軸を取得する
        /// </summary>
        public static Axis MinAxis(this Vector3 self) {
            // [NOTE] 値が等しい場合は(x > y > z)の順で優先される
            if (self.x <= self.y && self.x <= self.z) {
                return Axis.X;
            } else if (self.y <= self.x && self.y <= self.z) {
                return Axis.Y;
            } else {
                return Axis.Z;
            }
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region 値の変換 

        /// <summary>
        /// X値のみ変更した値を返す拡張メソッド
        /// </summary>
        public static Vector3 WithX(this Vector3 self, float x) => new Vector3(x, self.y, self.z);

        /// <summary>
        /// Y値のみ変更した値を返す拡張メソッド
        /// </summary>
        public static Vector3 WithY(this Vector3 self, float y) => new Vector3(self.x, y, self.z);

        /// <summary>
        /// Z値のみ変更した値を返す拡張メソッド
        /// </summary>
        public static Vector3 WithZ(this Vector3 self, float z) => new Vector3(self.x, self.y, z);

        // ------ 

        /// <summary>
        /// X値のみ変更した値を返す拡張メソッド
        /// </summary>
        public static Vector3 KeepX(this Vector3 self) => new Vector3(self.x, 0, 0);

        /// <summary>
        /// Y値のみ変更した値を返す拡張メソッド
        /// </summary>
        public static Vector3 KeepY(this Vector3 self) => new Vector3(0, self.y, 0);

        /// <summary>
        /// Z値のみ変更した値を返す拡張メソッド
        /// </summary>
        public static Vector3 KeepZ(this Vector3 self) => new Vector3(0, 0, self.z);

        /// <summary>
        /// Vector3のx, y, zのうち最大の要素のみを保持し、他の要素を0にする拡張メソッド
        /// </summary>
        public static Vector3 KeepMax(this Vector3 self) {

            // 最大の値を持つ要素のみ保持
            if (self.x >= self.y && self.x >= self.z) {
                return self.KeepX();
            } else if (self.y >= self.x && self.y >= self.z) {
                return self.KeepY();
            } else {
                return self.KeepZ();
            }
        }

        /// <summary>
        /// Vector3のx, y, zのうち最小の要素のみを保持し、他の要素を0にする拡張メソッド
        /// </summary>
        public static Vector3 KeepMin(this Vector3 self) {

            // 最大の値を持つ要素のみ保持
            if (self.x <= self.y && self.x <= self.z) {
                return self.KeepX();
            } else if (self.y <= self.x && self.y <= self.z) {
                return self.KeepY();
            } else {
                return self.KeepZ();
            }
        }


        #endregion


        /// ----------------------------------------------------------------------------
        #region 値の変換 

        /// <summary>
        /// 全ての要素を正の値にする拡張メソッド
        /// </summary>
        public static Vector3 Positate(this Vector3 self) => new Vector3(Mathf.Abs(self.x), Mathf.Abs(self.y), Mathf.Abs(self.z));

        /// <summary>
        /// 全ての要素を負の値にする拡張メソッド
        /// </summary>
        public static Vector3 Negate(this Vector3 self) => new Vector3(Mathf.Abs(self.x), Mathf.Abs(self.y), Mathf.Abs(self.z));

        // -----

        /// <summary>
        /// 半分の値を返す拡張メソッド
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Half(this Vector3 self) => self * 0.5f;

        /// <summary>
        /// ２倍の値を返す拡張メソッド
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Twice(this Vector3 self) => self * 2f;

        #endregion




        /// ----------------------------------------------------------------------------
        // 値の加算

        /// <summary>
        /// X値に加算する拡張メソッド
        /// </summary>
        public static void AddX(ref this Vector3 self, float x) => self.x += x;

        /// <summary>
        /// Y値に加算する拡張メソッド
        /// </summary>
        public static void AddY(ref this Vector3 self, float y) => self.y += y;

        /// <summary>
        /// Z値に加算する拡張メソッド
        /// </summary>
        public static void AddZ(ref this Vector3 self, float z) => self.z += z;



        /// ----------------------------------------------------------------------------
        #region 値の変換

        /// <summary>
        /// 全ての要素を指定の範囲内にClampする拡張メソッド
        /// </summary>
        public static Vector3 Clamp(this Vector3 self, float min, float max) {
            return new Vector3(
                Mathf.Clamp(self.x, min, max), 
                Mathf.Clamp(self.y, min, max), 
                Mathf.Clamp(self.z, min, max));
        }

        /// <summary>
        /// <see cref="Vector3.ClampMagnitude(Vector3, float)"/>の拡張メソッド
        /// </summary>
        public static Vector3 ClampMagnitude(this Vector3 self, float maxLength) {
            return Vector3.ClampMagnitude(self, maxLength);
        }

        /// <summary>
        /// <see cref="Vector3.ClampMagnitude(Vector3, float)"/>の拡張メソッド
        /// </summary>
        public static Vector3 ClampMagnitude01(this Vector3 self) {
            return Vector3.ClampMagnitude(self, 1);
        }


        
        #endregion


        public static Vector3 FindMinVector(IEnumerable<Vector3> ptList) {
            // LINQを使用して最小のベクトルを見つける
            return ptList.Aggregate((minVec, nextVec) => Vector3.Min(minVec, nextVec));
        }
    }

}