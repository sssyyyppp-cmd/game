using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MFarm.Invrntory
{
    public class SlotUI : MonoBehaviour , IPointerClickHandler, IBeginDragHandler,IDragHandler,IEndDragHandler
    {
        [Header("组件获取")]
        [SerializeField] private Image slotImage;
        [SerializeField] private TextMeshProUGUI amoutText;
        public Image slotHightlight;
        [SerializeField] private Button button;
        public SlotType slotType;
        public bool isSelected;

        private InventoryUI inventoryUI => GetComponentInParent<InventoryUI>();


        /// <summary>
        /// 置空格子
        /// </summary>

        public void UpdataEmptySlot()
        {
            if (isSelected)
            {
                isSelected = !isSelected;
            }
            slotImage.enabled = false;
            amoutText.text = string.Empty;
            button.interactable = false;
        }



        public ItemDetails itemDetails;
        public int itemAmount;

        public int slotIndex;
        /// <summary>
        /// 更新格子
        /// </summary>
        /// 

        public void UpdateSlot(ItemDetails item, int amount)
        {
            itemDetails = item;
            slotImage.sprite = item.itemIcon;
            slotImage.enabled = true;
            itemAmount = amount;
            amoutText.text = amount.ToString();
            button.interactable = true;
        }

        // Start is called before the first frame update
        void Start()
        {
            isSelected = false;
            if (itemDetails.id == 0)
            {
                UpdataEmptySlot();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (itemAmount == 0) return;
            isSelected = !isSelected;
            inventoryUI.UpdateSlotHightLight(slotIndex);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (itemAmount == 0) return;
            inventoryUI.dragImage.enabled = true;
            inventoryUI.dragImage.sprite = slotImage.sprite;
            inventoryUI.dragImage.SetNativeSize();
            isSelected = true;
            inventoryUI.UpdateSlotHightLight(slotIndex);
        }

        public void OnDrag(PointerEventData eventData)
        {
            inventoryUI.dragImage.transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            inventoryUI.dragImage.enabled = false;
            //Debug.Log(eventData.pointerCurrentRaycast.gameObject);
            if(eventData.pointerCurrentRaycast.gameObject != null)
            {
                if (eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotUI>() == null)
                    return;
                var targetSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotUI>();
                var targetIndex = targetSlot.slotIndex;

                if(slotType == SlotType.Bag && targetSlot.slotType == SlotType.Bag)
                {
                    InventoryManager.Instance.SwapItem(slotIndex, targetIndex);
                }

                inventoryUI.UpdateSlotHightLight(-1);
            }
            //else
            //{
            //    if (!itemDetails.canDroped) return;
            //    var pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,-Camera.main.transform.position.z));

            //    EventHandler.CallInstantiateItemInScene(itemDetails.id, pos);
            //}
        }
    }
}
