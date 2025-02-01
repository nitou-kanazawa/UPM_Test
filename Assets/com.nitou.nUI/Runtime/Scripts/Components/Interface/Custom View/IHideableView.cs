using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Nitou.UI.Components {

    /// <summary>
    /// 表示・非表示の切り替えが可能な基本UI．
    /// </summary>
    public interface IHideableView {

        /// <summary>
        /// 表示状態に遷移する．
        /// </summary>
        public Tweener DOShow(float duration);

        /// <summary>
        /// 非表示状態に遷移する．
        /// </summary>
        public Tweener DOHide(float duration);
    }
}
