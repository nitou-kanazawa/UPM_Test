using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Sirenix.OdinInspector;

namespace Nitou.UI.View {

    /// <summary>
    /// スロットを一覧表示するViewクラス
    /// </summary>
    public abstract class SlotListView<Slot, SlotInfo> : MonoBehaviour
        where Slot : Selectable {

        [Title("Slot")]

        [AssetsOnly]
        [SerializeField] private Slot _slotPrefab;

        // 格納する親オブジェクト
        [SerializeField] private Transform _contentParent;

        protected readonly List<Slot> _slotList = new();
        private bool _isInitialized = false;

        // 定数
        protected const int DEFAULT_SLOT_NUM = 20;


        /// --------------------------------------------------------------------
        // Properity
        protected List<Slot> FilledSlotList => _slotList.GetRange(0, FilledSlotNum);

        /// <summary>
        /// 情報が登録済みのスロット数
        /// </summary>
        public int FilledSlotNum => (_slotList != null) ? _slotList.Count(x => x.interactable) : 0;

        /// <summary>
        /// 生成されている全スロット数
        /// </summary>
        public int AllSlotNum { get; private set; }

        /// <summary>
        /// 初期化が完了しているかどうか
        /// </summary>
        public bool IsInitialized => _isInitialized;

        // ---

        /// <summary>
        /// UI Selectionの対象リスト
        /// </summary>
        public List<Selectable> Selectables => _slotList.OfType<Selectable>().ToList();

        /// <summary>
        /// デフォルトの選択対象
        /// </summary>
        public virtual Selectable DefaultSelection => _slotList.Count > 0 ? _slotList[0] : null;


        /// --------------------------------------------------------------------
        // Lifecycle Events

        protected virtual void OnDestroy() {
            DestroySlots();
        }


        /// --------------------------------------------------------------------
        // Public Method 

        /// <summary>
        /// Viewの初期化
        /// </summary>
        public virtual void Initialize() {
            if (_isInitialized) return;

            CreateSlots(DEFAULT_SLOT_NUM);
            _isInitialized = true;
        }

        /// <summary>
        /// スロット情報を登録する
        /// </summary>
        public abstract void SetSlotInfo(IReadOnlyList<SlotInfo> slotDataList, System.Action<SlotInfo> onClick);

        /// <summary>
        /// スロット情報を消去する
        /// </summary>
        public abstract void ClearSlotInfo();


        /// --------------------------------------------------------------------
        // private Method

        /// <summary>
        /// スロットリストを生成する
        /// </summary>
        protected void CreateSlots(int slotNum) {
            // 初期化
            DestroySlots();

            // スロット数
            AllSlotNum = slotNum;

            // スロットリストの生成
            for (int i = 0; i < slotNum; i++) {
                var slot = InstantiateSlot();
                slot.name = $"Slot[{i + 1}/{AllSlotNum}]";

                _slotList.Add(slot);
            }
        }

        /// <summary>
        /// スロットリストを削除する
        /// </summary>
        protected virtual void DestroySlots() {
            // リスト初期化
            if (_slotList != null) {
                _slotList.Clear();
            }

            // ヒエラルキー初期化
            foreach (Transform child in _contentParent) {
                Destroy(child.gameObject);
            }
        }

        /// <summary>
        /// スロットのインスタンス化
        /// </summary>
        protected virtual Slot InstantiateSlot() {
            var slot = GameObject.Instantiate<Slot>(_slotPrefab, parent: _contentParent);
            slot.interactable = false;

            return slot;
        }

    }

}