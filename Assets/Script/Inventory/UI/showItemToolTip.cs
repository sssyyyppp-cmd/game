using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
 

namespace MFarm.Invrntory
{
    [RequireComponent(typeof(SlotUI))]
    public class showItemToolTip : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
    {
        private SlotUI slotUI;

        private InventoryUI inventoryUI => GetComponentInParent<InventoryUI>();

        private void Awake()
        {
            slotUI = GetComponent<SlotUI>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (slotUI.itemAmount == 0)
            {
                inventoryUI.itemToolTip.gameObject.SetActive(false);
            }
            else
            {
                inventoryUI.itemToolTip.gameObject.SetActive(true);
                inventoryUI.itemToolTip.setupToolTip(slotUI.itemDetails, slotUI.slotType);


                inventoryUI.itemToolTip.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0f);
                inventoryUI.itemToolTip.transform.position = transform.position + Vector3.up * 60;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log(inventoryUI);
            inventoryUI.itemToolTip.gameObject.SetActive(false);
        }
    }
}
