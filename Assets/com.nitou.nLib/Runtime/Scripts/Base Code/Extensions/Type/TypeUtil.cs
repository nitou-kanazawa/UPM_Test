using System;
using System.Collections.Generic;
using System.Linq;

namespace Nitou {
    
    /// <summary>
    /// <see cref="Type"/>型に対する汎用メソッド集
    /// </summary>
    public static class TypeUtil {

        /// ----------------------------------------------------------------------------
        #region 型情報

        /// <summary>
        /// デフォルトコンストラクタを持っているか確認する
        /// </summary>
        public static bool HasDefaultConstructor(Type type) {
            return type.GetConstructors().Any(t => t.GetParameters().Count() == 0);
        }

        /// <summary>
        /// 基底クラスやインターフェースを取得する
        /// </summary>
        public static IEnumerable<Type> GetBaseClassesAndInterfaces(Type type, bool includeSelf = false) {
            if (includeSelf) yield return type;

            if (type.BaseType == typeof(object)) {
                foreach (var interfaceType in type.GetInterfaces()) {
                    yield return interfaceType;
                }
            } else {
                foreach (var baseType in Enumerable.Repeat(type.BaseType, 1)
                    .Concat(type.GetInterfaces())
                    .Concat(GetBaseClassesAndInterfaces(type.BaseType))
                    .Distinct()) {
                    yield return baseType;
                }
            }
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region 生成       
        
        /// <summary>
        /// デフォルト値を取得する拡張メソッド
        /// </summary>
        public static object GetDefaultValue(this Type self) {
            if (!self.IsValueType) return null;
            return Activator.CreateInstance(self);
        }

        /// <summary>
        /// デフォルトのインスタンスを生成する拡張メソッド
        /// </summary>
        public static object CreateDefaultInstance(Type type) {
            if (type == typeof(string)) return "";
            if (type.IsSubclassOf(typeof(UnityEngine.Object))) return null;
            return Activator.CreateInstance(type);
        }

        #endregion

    }
}
