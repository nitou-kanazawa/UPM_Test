using System.Collections.Generic;
using System.Linq;


namespace Nitou {

    public interface IActivatable {

        /// <summary>
        /// アクティブな状態かどうか
        /// </summary>
        public bool IsActive { get; }

        /// <summary>
        /// アクティブ状態にする
        /// </summary>
        public void Activate();

        /// <summary>
        /// 非アクティブ状態にする
        /// </summary>
        public void Deactivate();
    }


    /// <summary>
    /// <see cref="IActivatable"/>型の拡張メソッド集
    /// </summary>
    public static class ActivatableExtensions {

        /// <summary>
        /// 全てのオブジェクトがアクティブかどうかを確認する
        /// </summary>
        public static bool AllActive(this IEnumerable<IActivatable> activatables) {
            return activatables.All(a => a.IsActive);
        }
    }
}