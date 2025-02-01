using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Page;
using UnityScreenNavigator.Runtime.Core.Modal;
using Sirenix.OdinInspector;

namespace Nitou.UI {

    /// <summary>
    /// 画面タイプ
    /// </summary>
    public enum ScreenType {
        None,
        Page,
        Modal,
        Overlay,
    }


    /// <summary>
    /// 
    /// </summary>
    public sealed partial class ScreenContiner : MonoBehaviour, IScreenContainer {

        [Title("Containers")]
        [SerializeField, Indent] private PageContainer _pageContainer;
        [SerializeField, Indent] private ModalContainer _modalContainer;
        [SerializeField, Indent] private ModalContainer _overlayContainer;

        private readonly ReactiveProperty<ScreenType> _currentType = new(ScreenType.None);


        /// ----------------------------------------------------------------------------
        #region Properity

        /// <summary>
        /// 通常画面のコンテナ
        /// </summary>
        public PageContainer PageContainer {
            get => _pageContainer;
            internal set => _pageContainer = value;
        }

        /// <summary>
        /// ポップアップ画面のコンテナ
        /// </summary>
        public ModalContainer ModalContainer {
            get => _modalContainer;
            internal set => _modalContainer = value;
        }

        /// <summary>
        /// 遷移画面のコンテナ
        /// </summary>
        public ModalContainer OverlayContainer {
            get => _modalContainer;
            internal set => _modalContainer = value;
        }


        /// <summary>
        /// アクティブな画面タイプ
        /// </summary>
        [Title("State Informatin")]
        [GUIColor(0f, 1f, 0.5f)]
        [ShowInInspector, ReadOnly, Indent]
        public IReadOnlyReactiveProperty<ScreenType> CurrentType => _currentType;

        /// <summary>
        /// Pushされているページ数
        /// </summary>
        [ShowInInspector, ReadOnly, Indent]
        public int PageCount => (_pageContainer != null) ? _pageContainer.Pages.Count : 0;

        /// <summary>
        /// Pushされているモーダル数
        /// </summary>
        [ShowInInspector, ReadOnly, Indent]
        public int ModalCount => (_modalContainer != null) ? _modalContainer.Modals.Count : 0;

        /// <summary>
        /// ページが存在するかどうか
        /// </summary>
        public bool ExistPage => PageCount >= 1;

        /// <summary>
        /// モーダルが存在するかどうか
        /// </summary>
        public bool ExistModal => ModalCount >= 1;

        /// <summary>
        /// 画面遷移中かどうか
        /// </summary>
        public bool IsInTransition => _pageContainer.IsInTransition || _modalContainer.IsInTransition;

        #endregion


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method


        private void OnDestroy() {
            _currentType?.Dispose();
        }


        /// ----------------------------------------------------------------------------
        // Public Method (Push)

        /// <summary>
        /// Pageを追加して，更新処理を行う．
        /// </summary>
        public async UniTask<TPage> PushPage<TPage>(string resourceKey, bool playAnimation,
            bool stack = true, string pageId = null, bool loadAsync = true,
            Action<(string pageId, TPage page)> onLoad = null) where TPage : Page {

            if (IsInTransition) {
                Debug_.LogWarning("遷移中はPageをPushできません．", Colors.Red);
                return null;
            }
            if (ExistModal) {
                Debug_.LogWarning("Modalが表示されているため，Pageを追加できません", Colors.Red);
                return null;
            }

            // Pageがある場合，
            if (ExistPage) {
                _pageContainer.GetActivePage().SetInteractable(false);
            }


            // Pageの追加
            TPage page = await _pageContainer.PushPage<TPage>(resourceKey, playAnimation, stack, pageId, loadAsync, onLoad);
            page.SetInteractable(true);

            // ステート情報の更新
            UpdateStateInfo();

            return page;
        }


        /// <summary>
        /// モーダルをPushする
        /// </summary>
        public async UniTask<TModal> PushModal<TModal>(string resourceKey, bool playAnimation,
            string modalId = null, bool loadAsync = true,
            Action<(string modalId, TModal modal)> onLoad = null) where TModal : Modal {

            if (IsInTransition) {
                Debug_.LogWarning("遷移中はModalをPushできません．", Colors.Red);
                return null;
            }

            // Modalがある場合
            if (ExistModal) {
                _modalContainer.GetActiveModal().SetInteractable(false);
            }
            // Pageがある場合
            else if (ExistPage) {
                _pageContainer.GetActivePage().SetInteractable(false);
            }


            // Modalの追加
            TModal modal = await _modalContainer.PushModal<TModal>(resourceKey, playAnimation, modalId, loadAsync, onLoad);
            modal.SetInteractable(true);

            // ステート情報の更新
            UpdateStateInfo();

            return modal;
        }


        /// ----------------------------------------------------------------------------
        // Public Method (Pop)

        /// <summary>
        /// アクティブな<see cref="Page"/>をポップする.
        /// </summary>
        public async UniTask PopPage(bool playAnimation, int popCount = 1) {
            if (!ExistPage) return;
            if (popCount <= 0 || PageCount < popCount) return;

            if (IsInTransition) {
                Debug_.LogWarning("遷移中はPop遷移を行えません．", Colors.Red);
                return;
            }
            if (ExistModal) {
                Debug_.LogWarning("Modalが表示されているため，Pageを削除できません", Colors.Red);
                return;
            }

            _pageContainer.GetActivePage().SetInteractable(false);

            // Pageの削除
            await _pageContainer.Pop(playAnimation, popCount);

            // ステート情報の更新
            UpdateStateInfo();

            // Selectable設定
            if (ExistPage) {
                var page = _pageContainer.GetActivePage();
                page.SetInteractable(true);
            }
        }

        /// <summary>
        /// アクティブな<see cref="Modal"/>をポップする.
        /// </summary>
        public async UniTask PopModal(bool playAnimation, int popCount = 1) {
            if (!ExistModal) return;
            if (popCount <= 0 || ModalCount < popCount) return;

            if (IsInTransition) {
                Debug_.LogWarning("遷移中はモーダルをPopできません．", Colors.Red);
                return;
            }

            // 選択状態の解除
            _modalContainer.GetActiveModal().SetInteractable(false);

            // Modalの削除
            await _modalContainer.Pop(playAnimation, popCount);

            // ステート情報の更新
            UpdateStateInfo();

            // まだモーダルが存在する場合，
            if (ExistModal) {
                Debug_.Log($"まだモーダルはあるぜ．name :{_modalContainer.GetActiveModal().name}");
                var modal = _modalContainer.GetActiveModal();
                modal.SetInteractable(true);
            }
            // ※ModalからPageに制御が変わった場合，
            else if (ExistPage) {
                Debug_.Log($"Switch controll [Modal => Page]", Colors.Orange);
                var page = _pageContainer.GetActivePage();
                page.SetInteractable(true);
            }
        }

        /// <summary>
        /// アクティブな画面をポップする
        /// </summary>
        public async UniTask Pop(bool playAnimation) {
            // 遷移中の場合，エラーを投げる
            if (IsInTransition)
                throw new InvalidOperationException("遷移中はPage/ModalのPopを行えません．");

            if (ExistModal) {
                await PopModal(playAnimation);

            } else if (ExistPage) {
                await PopPage(playAnimation);

            } else {
                throw new InvalidOperationException("コンテナにPage/Modalが存在しないため，Pop遷移を行えません.");
            }
        }

        /// <summary>
        /// 全てのPage，Modalをポップする
        /// </summary>
        public async UniTask PopAll(bool playAnimation = false) {
            await PopModal(playAnimation, ModalCount);
            await PopPage(playAnimation, PageCount);
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// スタック状態に基づいてアクティブモードを更新する
        /// </summary>
        private void UpdateStateInfo() {

            if (ExistModal) {
                _currentType.Value = ScreenType.Modal;

            } else if (ExistPage) {
                _currentType.Value = ScreenType.Page;

            } else {
                _currentType.Value = ScreenType.None;
            }
        }
    }
}
