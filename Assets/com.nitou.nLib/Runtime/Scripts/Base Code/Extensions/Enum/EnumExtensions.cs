using System;

namespace Nitou {

    /// <summary>
    /// 列挙型の基本的な拡張メソッド集
    /// </summary>
    public static class EnumExtensions {

        /// <summary>
        /// 指定した値のいずれかと一致するか確認する拡張メソッド
        /// </summary>
        public static bool IsAnyOf<TEnum>(this TEnum value, params TEnum[] values)
            where TEnum : Enum {

            if (values == null) {
                throw new ArgumentNullException(nameof(values), "The values array cannot be null.");
            }

            foreach (var val in values) {
                if (value.Equals(val)) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool HasFlags<TEnum>(this TEnum value, params TEnum[] flags)
            where TEnum : Enum {

            if (flags == null) {
                throw new ArgumentNullException(nameof(flags), "The values array cannot be null.");
            }

            foreach (var flag in flags) {
                if (!value.HasFlag(flag)) {
                    return false;
                }
            }
            return true;
        }
    }
}
