using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
using Sirenix.OdinInspector;

using ItemSlotData = Nitou.UI.View.ItemSlot.ItemSlotData;



namespace Nitou.UI.View {

    /// <summary>
    /// ItemSlotのリストを表示するView
    /// </summary>
    public class ItemSlotListView : SlotListView<ItemSlot, ItemSlot.ItemSlotData> {

        /// --------------------------------------------------------------------
        #region Inner Definition

        /// <summary>
        /// 
        /// </summary>
        public enum SortType {
            Rarity,
            Durability,
            Attack,
        }
        #endregion

        /// --------------------------------------------------------------------

        [Title("Information Bar")]
        [SerializeField] TextMeshProUGUI _sortTypeText;
        [SerializeField] TextMeshProUGUI _countText;

        // 現在のタイプ
        public SortType CurrentType { get; private set; }

        // イベント通知
        public event System.Action OnSetSlotInfo;
        public event System.Action OnClearSlotInfo;
        public event System.Action OnSlotSorted;


        /// --------------------------------------------------------------------
        // Public Method 

        /// <summary>
        /// スロット情報を登録する
        /// </summary>
        public override void SetSlotInfo(IReadOnlyList<ItemSlotData> slotDataList, System.Action<ItemSlotData> onCleck = null) {
            if (FilledSlotNum > 0) ClearSlotInfo();

            var itemNum = slotDataList.Count;
            for (int i = 0; i < itemNum && i < AllSlotNum; i++) {
                // スロットのセットアップ
                var slot = _slotList[i];
                slot.SetItemData(slotDataList[i]);
                slot.interactable = true;
                slot.OnClickSubject.Subscribe(x => onCleck?.Invoke(x));
            }

            // 後処理
            OnSetSlotInfo?.Invoke();    // イベント通知
            UpdateInfomationBar();      // View更新
        }

        /// <summary>
        /// スロット情報を消去する
        /// </summary>
        public override void ClearSlotInfo() {
            foreach (var slot in _slotList) {
                slot.ClearItemData();
                slot.interactable = false;
            }

            // 後処理
            OnClearSlotInfo?.Invoke();  // イベント通知
        }

        /// <summary>
        /// スロットリストをソートする
        /// </summary>
        public virtual void SortSlots(SortType sortType = SortType.Attack) {
            if (FilledSlotNum <= 1 || CurrentType == sortType) return;
            CurrentType = sortType;

            // 
            var filledSlots = _slotList.GetRange(0, FilledSlotNum);                             // データが入力されたスロット
            var emptySlots = _slotList.GetRange(FilledSlotNum, AllSlotNum - FilledSlotNum);     // 未入力のスロット

            // 指定要素でソート
            var orderdList = CurrentType switch {
                SortType.Rarity => filledSlots.OrderByDescending(x => x.ViewState.Rarity).ToList(),
                SortType.Attack => filledSlots.OrderByDescending(x => x.ViewState.Attack).ToList(),
                SortType.Durability => filledSlots.OrderByDescending(x => x.ViewState.Durability).ToList(),
                _ => throw new System.NotImplementedException()
            };

            // リスト更新
            orderdList.AddRange(emptySlots);
            //_slotList = orderdList;

            // ヒエラルキー更新
            for (int i = 0; i < FilledSlotNum; i++) {
                _slotList[i].transform.SetSiblingIndex(i);
            }

            // 後処理
            OnSlotSorted?.Invoke();
            UpdateInfomationBar();      // View更新
        }


        /// --------------------------------------------------------------------
        // Private Method 

        /// <summary>
        ///  ソート方法テキストを更新する
        /// </summary>
        private void UpdateSortTypeText() {

            // アイコンテキスト更新
            foreach (var slot in FilledSlotList) {
                var str = CurrentType switch {
                    SortType.Rarity => slot.ViewState.Rarity.ToString(),
                    SortType.Durability => $"{slot.ViewState.Durability:#}",    // ※実数部のみ表示
                    SortType.Attack => $"{slot.ViewState.Attack}",
                    _ => ""
                };
                slot.SetIconText(str);
            }

            // 
            // ソートテキスト
            _sortTypeText.text = $"> {CurrentType}";
            //_sortTypeText.DOFade(1, .2f).From(0);
        }

        /// <summary>
        /// スロット数テキストを更新する
        /// </summary>
        private void UpdateSlotCountText() {
            _countText.text = $"{FilledSlotNum}/{AllSlotNum}";
        }

        /// <summary>
        /// インフォメーションバーの表示を更新する
        /// </summary>
        private void UpdateInfomationBar() {
            UpdateSortTypeText();
            UpdateSlotCountText();
        }




        /// --------------------------------------------------------------------
        // Test Code

        //private void Update() {
        //    if (Keyboard.current.qKey.wasPressedThisFrame) {
        //        SortSlots(Type.Attack);

        //    } else if (Keyboard.current.wKey.wasPressedThisFrame) {
        //        SortSlots(Type.Rarity);

        //    } else if (Keyboard.current.eKey.wasPressedThisFrame) {
        //        SortSlots(Type.Durability);

        //    }
        //}


    }


}