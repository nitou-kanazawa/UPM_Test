using System;
using System.Collections.Generic;

namespace Nitou {

    /// <summary>
    /// <see cref="Array"/>型の汎用メソッド．
    /// </summary>
    public static partial class ArrayUtils {

        public static void Add<T>(ref T[] array, T value) {
            int length = array.Length;
            Array.Resize(ref array, length + 1);
            array[length] = value;
        }

        public static T[] AddReturn<T>(T[] array, T value) {
            int length = array.Length;
            Array.Resize(ref array, length + 1);
            array[length] = value;
            return array;
        }

        public static void Remove<T>(ref T[] array, T value) {
            array = Array.FindAll(array, item => !EqualityComparer<T>.Default.Equals(item, value));
        }

        public static T[] FindAll<T>(T[] array, Predicate<T> match) {
            return Array.FindAll(array, match);
        }

        public static T Find<T>(T[] array, Predicate<T> match) {
            return Array.Find(array, match);
        }
    }
}
