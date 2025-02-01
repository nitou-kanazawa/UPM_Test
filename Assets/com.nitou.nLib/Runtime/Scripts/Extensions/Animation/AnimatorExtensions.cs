using UnityEngine;

namespace Nitou {

    /// <summary>
    /// <see cref="Animator"/>型の基本的な拡張メソッド集
    /// </summary>
    public static partial class AnimatorExtensions {

        /// <summary>
        /// 現在再生しているアニメーションが終了しているか？
        /// </summary>
        public static bool IsCompleted(this Animator self) {
            return self.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f;
        }

        /// <summary>
        /// 現在再生しているアニメーションが指定ステートかつ終了しているか？
        /// </summary>
        public static bool IsCompleted(this Animator self, int stateHash) {
            return self.GetCurrentAnimatorStateInfo(0).shortNameHash == stateHash && self.IsCompleted();
        }

        /// <summary>
        /// 現在再生しているアニメーションの指定時間(割合)を過ぎているか？
        /// </summary>
        public static bool IsPassed(this Animator self, float normalizedTime) {
            return self.GetCurrentAnimatorStateInfo(0).normalizedTime > normalizedTime;
        }

        /// <summary>
        /// アニメーションを最初から再生する
        /// </summary>
        public static void PlayBegin(this Animator self, int shortNameHash) {
            self.Play(shortNameHash, 0, 0.0f);
        }
    }

}