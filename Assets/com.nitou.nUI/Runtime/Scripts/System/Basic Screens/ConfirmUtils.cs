using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Modal;

namespace Nitou.UI.BasicScreens {

    public static class ConfirmUtils {

        /// <summary>
        /// １選択肢の確認モーダルを表示する
        /// </summary>
        public async static UniTask PushOneChoiceModalAsync(
            this ModalContainer container,
            string resourceKey, bool playAnimation = true
            ) {

            bool isCliced = false;

            await container.Push<OneChiceConfirmModal>(resourceKey, playAnimation,
                onLoad: x => {
                    var modal = x.modal;

                    // ※クリック時の処理
                    void OnClick() {
                        isCliced = true;
                        container.Pop(true);
                    }

                    // バインド
                    modal.OnYesButtonClicked.Subscribe(_ => OnClick());
                });

            await UniTask.WaitUntil(() => isCliced);
        }

        /// <summary>
        /// ２選択肢の確認モーダルを表示する
        /// </summary>
        public async static UniTask<bool> PushTwoChoiceModalAsync(
            this ModalContainer container,
            string resourceKey, bool playAnimation = true
            ) {

            bool isCliced = false;
            bool result = false;

            await container.Push<TwoChoiceConfirmModal>(resourceKey, playAnimation,
                onLoad: x => {
                    var modal = x.modal;

                    // ※クリック時の処理
                    void OnClick(bool value) {
                        isCliced = true;
                        result = value;
                        Debug_.Log(result ? "Yes" : "No");
                        container.Pop(true);
                    }

                    // バインド
                    modal.OnYesButtonClicked.Subscribe(_ => OnClick(true));
                    modal.OnNoButtonClicked.Subscribe(_ => OnClick(false));
                });

            await UniTask.WaitUntil(() => isCliced);
            return result;
        }
    }

}