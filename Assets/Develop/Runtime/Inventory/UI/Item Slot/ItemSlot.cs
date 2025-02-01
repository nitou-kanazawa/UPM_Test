using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UniRx;
using Sirenix.OdinInspector;


// [参考]
//  Unity Comunity: How to detect interactable state change in selectable objects (Buttons, etc.) https://discussions.unity.com/t/how-to-detect-interactable-state-change-in-selectable-objects-buttons-etc/150644

namespace Nitou.UI.View {

    /// <summary>
    /// アイテムを表示するためのスロットView．
    /// </summary>
    public class ItemSlot : Selectable, 
        IPointerClickHandler, IEventSystemHandler, ISubmitHandler {

        /// --------------------------------------------------------------------
        #region Inner Definition

        /// <summary>
        /// View用のアイテムデータ定義
        /// ※GUIDのみで十分だがPresenterでデータをListで渡したいのでまずはこれで
        /// </summary>
        public struct ItemSlotData {
            public string Name;
            public Sprite Icon;
            public ItemRarity Rarity;   // レア度
            public float Durability;    // 耐久値
            public int Attack;          // 攻撃力
            // -----
            public string Guid;         // GUID
            public object ItemPtr;      // 
        }

        /// <summary>
        /// アイテムのレア度
        /// </summary>
        public enum ItemRarity {
            C,
            B,
            A,
            S
        }
        #endregion


        /// --------------------------------------------------------------------
        // View Component

        [Header("View Components")]

        [SerializeField] private Image _iconImage;
        [SerializeField] private Image _backdropImage;

        // アイコン内テキスト
        [SerializeField] private TextMeshProUGUI _iconText;

        [Header("Switching Objects")]

        [LabelText("when slot is interactable")]
        [SerializeField] private GameObject _activeObject;

        [LabelText("when slot is not interactable")]
        [SerializeField] private GameObject _deactiveObject;

        [LabelText("when slot is selected")]
        [SerializeField] private GameObject _highlitingObj;


        /// --------------------------------------------------------------------
        // View State

        public ItemSlotData ViewState { get; private set; }

        public int Index { get; set; } = -1;

        /// <summary>
        /// クリック時のイベント通知
        /// </summary>
        public Subject<ItemSlotData> OnClickSubject = new();


        /// --------------------------------------------------------------------
        // MonoBehaviour Method

        protected override void Awake() {
            base.Awake();
            _highlitingObj.SetActive(false);
        }

        protected override void OnDestroy() {
            base.OnDestroy();
            OnClickSubject.Dispose();
        }


        /// --------------------------------------------------------------------
        // Interface Method

        public override void OnSelect(BaseEventData eventData) {
            base.OnSelect(eventData);
            _highlitingObj.SetActive(true);
        }

        public override void OnDeselect(BaseEventData eventData) {
            base.OnDeselect(eventData);
            _highlitingObj?.SetActive(false);
        }

        public virtual void OnPointerClick(PointerEventData eventData) {
            if (interactable) {
                OnClickSubject.OnNext(this.ViewState);

            }
        }

        public virtual void OnSubmit(BaseEventData eventData) {
            if (interactable) {
                OnClickSubject.OnNext(this.ViewState);
            }
        }


        /// --------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// アイテム情報を設定する
        /// </summary>
        public void SetItemData(ItemSlotData data) {
            ViewState = data;

            // View更新
            this.interactable = true;
            this._iconImage.sprite = ViewState.Icon;
        }

        /// <summary>
        /// アイテム情報を初期化する
        /// </summary>
        public void ClearItemData() {
            ViewState = new ItemSlotData();

            // View更新
            this.interactable = false;
            this._iconImage.sprite = null;
            this._iconText.text = $"";
            this.Index = -1;
        }

        /// <summary>
        /// アイコン内のテキストを設定する
        /// </summary>
        public void SetIconText(string str) {
            this._iconText.text = str;
        }


        /// --------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// ステートが変化したときの処理
        /// </summary>
        protected override void DoStateTransition(SelectionState state, bool instant) {

            if (state == SelectionState.Disabled) {
                _activeObject.SetActive(false);
                _deactiveObject.SetActive(true);
            } else {
                _activeObject.SetActive(true);
                _deactiveObject.SetActive(false);
            }
        }

    }

}