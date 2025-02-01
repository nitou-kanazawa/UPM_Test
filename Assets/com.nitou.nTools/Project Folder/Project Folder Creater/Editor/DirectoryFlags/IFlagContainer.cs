using System.Reflection;
using System.Linq;

namespace Nitou.Tools.ProjectWindow {

    /// <summary>
    /// フラグを定義しているクラス
    /// </summary>
    public interface IFlagContainer {}


    /// <summary>
    /// <see cref="IFlagContainer"/>に対する拡張メソッド
    /// </summary>
    public static class FlagContainerExtensions {

        public static bool GetFlagValue(this IFlagContainer container, string flagName) {
            var field = container.GetType().GetField(flagName);
            return field != null && (bool)field.GetValue(container);
        }

        public static void SetFlagValue(this IFlagContainer container, string flagName, bool value) {
            var field = container.GetType().GetField(flagName);
            if (field != null && field.FieldType == typeof(bool)) {
                field.SetValue(container, value);
            }
        }

        /// <summary>
        /// フラグ名のリストを取得する
        /// </summary>
        public static string[] GetFlagNames(this IFlagContainer container) {
            return container.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance)
                             .Where(f => f.FieldType == typeof(bool))
                             .Select(f => f.Name)
                             .ToArray();
        }
    }
}
