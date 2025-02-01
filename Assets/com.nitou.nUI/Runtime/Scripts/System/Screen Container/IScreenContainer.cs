using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Page;
using UnityScreenNavigator.Runtime.Core.Modal;

namespace Nitou.UI {

    /// <summary>
    /// 
    /// </summary>
    public interface IScreenContainer {


        int PageCount { get; }
        int ModalCount { get; }

        bool ExistPage { get; }
        bool ExistModal { get; }



        /// ----------------------------------------------------------------------------
        #region Push


        #endregion


        /// ----------------------------------------------------------------------------
        #region Pop

        /// <summary>
        /// アクティブな画面をポップする
        /// </summary>
        public UniTask Pop(bool playAnimation);

        /// <summary>
        /// 指定数の<see cref="Page"/>をポップする
        /// </summary>
        public UniTask PopPage(bool playAnimation, int popCount = 1);

        /// <summary>
        /// 指定数の<see cref="Modal"/>をポップする
        /// </summary>
        public UniTask PopModal(bool playAnimation, int popCount = 1);

        /// <summary>
        /// 全ての画面をポップする
        /// </summary>
        public UniTask PopAll(bool playAnimation);

        #endregion
    }
}
