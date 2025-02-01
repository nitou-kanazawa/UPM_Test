using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

// [TODO]　2年近くメンテナンスされてないので，動作検証から取り組む (2024.08.01)

namespace Nitou {

    /// <summary>
    /// 配列に対する汎用機能を提供するライブラリ．
    /// </summary>
    public static partial class MultiDimensionArrayUtils {

        /// ----------------------------------------------------------------------------
        // 配列の生成

        /// <summary>
        /// 初期値を指定して2次元配列を生成
        /// </summary>
        public static T[,] Make2dArray<T>(T value, int xnum = 10, int ynum = 10) {
            var array = Enumerable.Repeat(value, ynum * xnum).ToArray();
            return array.To2dArray(ynum, xnum);
        }


        /// ----------------------------------------------------------------------------
        // 入力配列srcからの新規配列生成

        /// <summary>
        /// 指定した2次元配列を複製する
        /// </summary>
        public static T[,] Clone<T>(T[,] src) {
            // 配列準備
            int ynum = src.GetLength(0);
            int xnum = src.GetLength(1);
            var dest = new T[ynum, xnum];

            // 値挿入
            for (int y = 0; y < ynum; ++y) {
                for (int x = 0; x < xnum; ++x) {
                    dest[y, x] = src[y, x];
                }
            }
            return dest;
        }

        /// <summary>
        /// 指定した2次元配列の位置 (x, y) から (w to h) の範囲を切り取る
        /// </summary>
        public static T[,] Cut<T>(T[,] src, int xID, int yID, int dx, int dy) {
            // 要素数の検証
            int ynum = src.GetLength(0);
            int xnum = src.GetLength(1);
            if (xID < 0 || xID > xnum || yID < 0 || yID > ynum) {
                string msg = string.Format("Parameter is out of range. src[y={0},x={1}], x={2}, y={3}", ynum, xnum, xID, yID);
                throw new ArgumentOutOfRangeException(msg);
            }
            if (dx == 0 || dy == 0) {
                throw new ArgumentException(string.Format("Invalid parameter. w={0}, h={1}", dx, dy));
            }

            // コピー先の配列の大きさ
            int rh = yID + dy <= ynum ? dy : dy - (yID + dy - ynum);
            int rw = xID + dx <= xnum ? dx : dx - (xID + dx - xnum);
            // 元の大きさからコピー
            var map = new T[rh, rw];
            for (int my = 0, _y = yID; my < rh; my++, _y++) {
                for (int mx = 0, _x = xID; mx < rw; mx++, _x++) {
                    map[my, mx] = src[_y, _x];
                }
            }
            return map;
        }

        /// <summary>
        /// 入力配列の周囲にマージンを追加した配列を生成
        /// (※追加領域は(T)の初期値になる)
        /// </summary>
        public static T[,] AddMargin<T>(T[,] src, int margin) {
            // 要素数の検証
            int ynum = src.GetLength(0);
            int xnum = src.GetLength(1);
            if (margin == 0) {
                throw new ArgumentException(string.Format("Invalid parameter. margin={0}", margin));
            }
            // 
            var map = new T[ynum + margin * 2, xnum + margin * 2];
            for (int y = 0; y < ynum; ++y) {
                for (int x = 0; x < xnum; ++x) {
                    map[y + margin, x + margin] = src[y, x];
                }
            }
            return map;
        }


        /// ----------------------------------------------------------------------------
        // 配列形式・サイズの変換

        /// <summary>
        /// 指定した2次元配列を1次元配列に変換する
        /// </summary>
        public static T[] To1dArray<T>(T[,] src) {
            // 配列準備 -----
            int rowNum = src.GetLength(0);
            int colNum = src.GetLength(1);
            var dest = new T[colNum * rowNum];

            // 値挿入 -----
            for (int r = 0, i = 0; r < rowNum; ++r) {
                for (int c = 0; c < colNum; ++c, ++i) {
                    dest[i] = src[r, c];
                }
            }
            return dest;
        }

        /// <summary>
        /// 指定した3次元配列を1次元配列に変換する
        /// </summary>
        public static T[] To1dArray<T>(T[,,] src) {
            // 配列準備 -----
            int xnum = src.GetLength(0);
            int ynum = src.GetLength(1);
            int znum = src.GetLength(2);

            var dest = new T[xnum * ynum * znum];
            // 値挿入 -----
            for (int x = 0, i = 0; x < xnum; ++x) {
                for (int y = 0; y < ynum; ++y) {
                    for (int z = 0; z < znum; ++z, ++i) {
                        dest[i] = src[x, y, z];
                    }
                }
            }
            return dest;
        }

        /// <summary>
        /// 指定した1次元配列を2次元配列に変換する
        /// (※範囲を超える分は切り捨て、不足している分は(T)の初期値になる)
        /// </summary>
        public static T[,] To2dArray<T>(T[] src, int xnum, int ynum) {
            var dest = new T[ynum, xnum];
            int len = xnum * ynum;
            len = src.Length < len ? src.Length : len;
            for (int y = 0, i = 0; y < ynum; y++) {
                for (int x = 0; x < xnum; x++, i++) {
                    if (i >= len) {
                        return dest;
                    }
                    dest[y, x] = src[i];
                }
            }
            return dest;
        }

        /// <summary>
        /// 指定した1次元配列を3次元配列に変換する
        /// (※範囲を超える分は切り捨て、不足している分は(T)の初期値になる)
        /// </summary>
        public static T[,,] To3dArray<T>(T[] src, int xnum, int ynum, int znum) {
            var dest = new T[xnum, ynum, znum];
            int len = xnum * ynum * znum;
            len = src.Length < len ? src.Length : len;
            for (int x = 0, i = 0; x < xnum; x++) {
                for (int y = 0; y < ynum; y++) {
                    for (int z = 0; z < znum; z++, i++) {

                        if (i >= len) return dest;
                        dest[x, y, z] = src[i];
                    }
                }
            }
            return dest;
        }


        /// ----------------------------------------------------------------------------
        // デバッグ処理

        /// <summary>
        /// 【debug用】 指定した配列の内容を文字列に変換する
        /// </summary>
        public static string ToStringByDebug<T>(T[,] src) {
            var a = new StringBuilder();
            var b = new StringBuilder();
            int ymax = src.GetLength(0);
            int xmax = src.GetLength(1);
            for (int iy = 0; iy < ymax; iy++) {
                for (int ix = 0; ix < xmax; ix++) {
                    b.Append($" {src[iy, ix]},");
                }
                a.Append(b.ToString().Trim(' ', ','));
                a.Append(Environment.NewLine);
                b.Clear();
            }
            return a.ToString();
        }

        /// <summary>
        /// 【debug用】 指定した配列の内容を文字列に変換する
        /// </summary>
        public static string ToStringByDebug<T>(T[,,] src, Func<T, bool> evaluate = null) {
            var a = new StringBuilder();
            var b = new StringBuilder();    // １行文を格納

            int ymax = src.GetLength(0);
            int xmax = src.GetLength(1);
            int zmax = src.GetLength(2);
            for (int iy = 0; iy < ymax; iy++) {

                a.Append($" [y = {iy}] ");
                a.Append(Environment.NewLine);
                a.Append(new string('-',zmax*2) + " Z");
                a.Append(Environment.NewLine);

                for (int ix = 0; ix < xmax; ix++) {

                    b.Append((ix==(xmax-1)) ? " X " : " | ");
                    
                    for (int iz = 0; iz < zmax; iz++) {

                        // 値の追加
                        var value = src[ix, iy, iz];
                        if (evaluate != null && evaluate(value)) {
                            b.Append($" <color=cyan>{value}</color>,");
                        } else {
                            b.Append($" {value},");
                        }

                    }
                    a.Append(b.ToString().Trim(' ', ','));
                    a.Append(Environment.NewLine);
                    b.Clear();
                }
                a.Append(Environment.NewLine);
            }
            return a.ToString();
        }
    }



    /// <summary>
    /// 配列に対する拡張機能を提供する
    /// </summary>
    public static class MultiDimeArrayExtension {

        /// ----------------------------------------------------------------------------

        public static T[,] Clone<T>(this T[,] src) =>
            MultiDimensionArrayUtils.Clone(src);
        public static T[,] Cut<T>(this T[,] src, int x, int y, int w, int h) =>
            MultiDimensionArrayUtils.Cut(src, x, y, w, h);
        public static T[,] AddMargin<T>(this T[,] src, int m)
            => MultiDimensionArrayUtils.AddMargin(src, m);

        /// ----------------------------------------------------------------------------

        public static T[] To1dArray<T>(this T[,] src) =>
            MultiDimensionArrayUtils.To1dArray(src);
        public static T[] To1dArray<T>(this T[,,] src) =>
            MultiDimensionArrayUtils.To1dArray(src);
        public static T[,] To2dArray<T>(this T[] src, int xmax, int ymax) =>
            MultiDimensionArrayUtils.To2dArray(src, xmax, ymax);
        public static T[,,] To3dArray<T>(this T[] src, int xmax, int ymax, int zmax) =>
            MultiDimensionArrayUtils.To3dArray(src, xmax, ymax, zmax);

        public static string ToStringByDebug<T>(this T[,] src) =>
            MultiDimensionArrayUtils.ToStringByDebug(src);
        public static string ToStringByDebug<T>(this T[,,] src, Func<T, bool> evaluate = null) =>
            MultiDimensionArrayUtils.ToStringByDebug(src, evaluate);


        /// ----------------------------------------------------------------------------
        // 配列の分解

        public static void Deconstruct<T>(this IList<T> self, out T first, out IList<T> rest) {
            first = self.Count > 0 ? self[0] : default(T);
            rest = self.Skip(1).ToArray();
        }
        public static void Deconstruct<T>(this IList<T> self, out T first, out T second, out IList<T> rest) {
            first = self.Count > 0 ? self[0] : default(T);
            second = self.Count > 1 ? self[1] : default(T);
            rest = self.Skip(2).ToArray();
        }
        public static void Deconstruct<T>(this IList<T> self, out T first, out T second, out T third, out IList<T> rest) {
            first = self.Count > 0 ? self[0] : default(T);
            second = self.Count > 1 ? self[1] : default(T);
            third = self.Count > 2 ? self[2] : default(T);
            rest = self.Skip(3).ToArray();
        }
        public static void Deconstruct<T>(this IList<T> self, out T first, out T second, out T third, out T four, out IList<T> rest) {
            first = self.Count > 0 ? self[0] : default(T);
            second = self.Count > 1 ? self[1] : default(T);
            third = self.Count > 2 ? self[2] : default(T);
            four = self.Count > 3 ? self[3] : default(T);
            rest = self.Skip(4).ToArray();
        }

    }
}
