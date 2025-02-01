using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace Nitou.UI.BasicScreens {
    using Nitou.UI.Components;

    /// <summary>
    /// ２選択肢の確認モーダル
    /// </summary>
    public class TwoChoiceConfirmModal : ConfirmModalBase {

        [TitleGroup("Buttons")]
        [SerializeField, Indent] protected Button _yesButton;

        [TitleGroup("Buttons")]
        [SerializeField, Indent] protected Button _noButton;


        public IObservable<Unit> OnYesButtonClicked => _yesButton.OnClickAsObservable();
        public IObservable<Unit> OnNoButtonClicked => _noButton.OnClickAsObservable();


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// どちらかのボタンが押下されるのを待機するする
        /// </summary>
        public UniTask<bool> WaitUntilClicked() {
            return Observable.Merge(
                   OnYesButtonClicked.Select(x => true),
                   OnNoButtonClicked.Select(x => false))
               .ToUniTask(useFirstValue: true);
        }
    }

}