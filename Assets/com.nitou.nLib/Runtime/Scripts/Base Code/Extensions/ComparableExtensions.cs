using System;

namespace Nitou{

    /// <summary>
    /// <see cref="IComparable"/>型の基本的な拡張メソッド集
    /// </summary>
    public static class ComparableExtensions{

        public static bool LessThan<T>(this T self, T other) where T : IComparable<T> {
            return self.CompareTo(other) < 0;
        }

        public static bool GreaterThan<T>(this T self, T other) where T : IComparable<T> {
            return self.CompareTo(other) > 0;
        }

        public static bool LessThanEqual<T>(this T self, T other) where T : IComparable<T> {
            return self.CompareTo(other) <= 0;
        }

        public static bool GreaterThanEqual<T>(this T self, T other) where T : IComparable<T> {
            return self.CompareTo(other) >= 0;
        }
    }
}
