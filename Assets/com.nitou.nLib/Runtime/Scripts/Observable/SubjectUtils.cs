using UniRx;
using UnityEngine;

namespace Nitou {

    /// <summary>
    /// <see cref="Subject{T}"/>型の基本的な拡張メソッド集．
    /// </summary>
    public static partial class SubjectUtils {

        /// <summary>
        /// <see cref="Subject{T}.OnCompleted"/>を発行する終了処理．
        /// </summary>
        public static void DisposeSubject<T>(ref Subject<T> subject) {

            if (subject != null) {

                try {
                    subject.OnCompleted();
                } finally {
                    subject.Dispose();
                    subject = null;
                }
            }

        }

    }
}
