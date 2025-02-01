using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Mathf;
using Random = UnityEngine.Random;

// [REF]
//  UnityDocument: Random https://docs.unity3d.com/ja/2021.1/Manual/class-Random.html
//  PG日誌: リストから要素をランダムにN個取得する https://takap-tech.com/entry/2019/10/17/003706

namespace Nitou {

    /// <summary>
    /// 乱数に関する汎用メソッド集．
    /// </summary>
    public static class RandomUtils {

        /// ----------------------------------------------------------------------------
        #region 範囲内からの選出

        public static int Range(int min, int max) {
            return Random.Range(min, max);
        }

        public static float Range(float min, float max) {
            return Random.Range(min, max);
        }

        public static float Range(RangeFloat range) {
            return Random.Range(range.Min, range.Max);
        }

        /// <summary>
        /// 0~1の範囲内の乱数
        /// </summary>
        public static float Range01() {
            return Random.value;
        }

        public static float RangePlusMinus(float value) {
            return Random.Range(-value, value);
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region 要素内からの選択

        /// <summary>
        /// 任意型の２値からランダムに取得する
        /// </summary>
        public static T EitherOne<T>(T a, T b) {
            return RandomBool() ? a : b;
        }

        /// <summary>
        /// 任意型の３値からランダムに取得する
        /// </summary>
        public static T WhichOne<T>(T a, T b, T c) {
            return Range(0, 3) switch {
                0 => a,
                1 => b,
                2 => c,
                _ => throw new System.NotImplementedException(),
            };
        }

        /// <summary>
        /// リスト内の値からランダムに取得する
        /// </summary>
        public static T WhichOne<T>(this IReadOnlyList<T> list) {
            if (list.IsNullOrEmptyEnumerable()) throw new System.InvalidOperationException();
            return list[Range(0, list.Count)];
        }

        /// <summary>
        /// リスト内の値からランダムに取得する拡張メソッド
        /// </summary>
        public static T WhichOne<T>(this IEnumerable<T> collection) {
            return collection.ElementAt(Range(0, collection.Count()));
        }

        /// <summary>
        /// リスト内の値からランダムに取得する
        /// </summary>
        public static void RandomValueWithoutCurrent<T>(IReadOnlyList<T> list, ref int currentIndex) {
            if (list.Count < 1) {
                throw new System.ArgumentOutOfRangeException();
            }

            if (!GenericExtensions.IsInRange(currentIndex, 0, list.Count)) {
                throw new System.ArgumentOutOfRangeException(nameof(currentIndex));
            }

            int index;
            do {
                index = Range(0, list.Count);
            } while (index == currentIndex);

            // インデックス更新
            currentIndex = index;
        }



        /// <summary>
        /// リスト内の値からランダムに取得する拡張メソッド
        /// </summary>
        public static IEnumerable<T> RandomValues<T>(this IReadOnlyList<T> collection, int num) {
            if (num > collection.Count) {
                throw new System.ArgumentOutOfRangeException("リストの要素数よりnが大きいです。");
            }

            var indexList = new List<int>(collection.Count);
            for (int p = 0; p < collection.Count; p++) indexList.Add(p);


            for (int i = 0; i < num; i++) {
                int index = Random.Range(0, indexList.Count);
                int value = indexList[index];
                indexList.RemoveAt(index);
                yield return collection[value];
            }
        }

        #endregion


        /// ----------------------------------------------------------------------------
        #region  真偽

        /// <summary>
        /// bool型の乱数を取得する
        /// </summary>
        public static bool RandomBool() {
            return Random.Range(0, 2) == 0;
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region 座標

        /// <summary>
        /// 円上のランダムな点を取得する
        /// </summary>
        public static Vector2 PointInCircle(float radius, float startDeg = 0.0f, float endDeg = 359.99999f) {
            float deg = Range(startDeg, endDeg);
            float rad = deg * Deg2Rad;
            Vector2 position = new Vector2(Cos(rad), Sin(rad));

            position *= Range(0.0f, radius);
            return position;
        }

        /// <summary>
        /// 円周上のランダムな点を取得する
        /// </summary>
        public static Vector2 PointInCircumference(float radius, float startDeg = 0.0f, float endDeg = 359.99999f) {
            float deg = Range(startDeg, endDeg);
            float rad = deg * Deg2Rad;
            Vector2 position = new Vector2(Cos(rad), Sin(rad)) * radius;
            return position;
        }

        /// <summary>
        /// 四角形上のランダムな点を取得する
        /// </summary>
        public static Vector2 PointInBox2D(float halfSizeX, float halfSizeY) {
            return new Vector2(
                x: RangePlusMinus(halfSizeX),
                y: RangePlusMinus(halfSizeY));
        }

        /// <summary>
        /// 四角形上のランダムな点を取得する
        /// </summary>
        public static Vector2 PointInBox2D(Vector2 halfSize) {
            return PointInBox2D(halfSize.x, halfSize.y);
        }

        /// <summary>
        /// Box上のランダムな点を取得する
        /// </summary>
        public static Vector3 PointInBox(float halfSizeX, float halfSizeY, float halfSizeZ) {
            return new Vector3(
                x: RangePlusMinus(halfSizeX),
                y: RangePlusMinus(halfSizeY),
                z: RangePlusMinus(halfSizeZ));
        }

        /// <summary>
        /// Box内のランダムな点を取得する
        /// </summary>
        public static Vector3 PointInBox(Vector3 halfSize) {
            return PointInBox(halfSize.x, halfSize.y, halfSize.z);
        }

        #endregion


        /// ----------------------------------------------------------------------------
        #region カラー

        /// <summary>
        /// ランダムな色を生成
        /// </summary>
        public static Color Color(bool alpha = false) {
            return new Color(
                Range(0, 1.0f),
                Range(0, 1.0f),
                Range(0, 1.0f),
                alpha ? Range(0, 1.0f) : 1);
        }

        /// <summary>
        /// ランダムな色を生成
        /// </summary>
        public static Color Color(Vector2 red, Vector2 green, Vector2 blue, Vector2 alpha) {
            return new Color(
                Range(red.x, red.y),
                Range(green.x, green.y),
                Range(blue.x, blue.y),
                Range(alpha.x, alpha.y));
        }
        #endregion
    }


    public static class RandomExtensions {


        
    }

}
