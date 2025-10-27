using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MFarm.Invrntory
{
    public class InventoryUI : MonoBehaviour
    {
        [Header("ÍÏ×§Í¼Æ¬")]
        public Image dragImage;
        [Header("Íæ¼Ò±³°üUI")]
        [SerializeField] private GameObject BagUI;

        private bool isOpened;

        [SerializeField] private SlotUI[] playerSlots;


        private void OnEnable()
        {
            EventHandler.updateInventoryUI += OnUpdateInventoryUI;
        }

        private void OnDisable()
        {
            EventHandler.updateInventoryUI -= OnUpdateInventoryUI;
        }

        private void OnUpdateInventoryUI(InventoryLocation location, List<InventoryItem> list)
        {
            switch (location)
            {
                case InventoryLocation.Player:
                    for(int i = 0; i < playerSlots.Length; i++)
                    {
                        if (list[i].itemAmount > 0)
                        {
                            var item = InventoryManager.Instance.GetItemDetails(list[i].ItemID);
                            playerSlots[i].UpdateSlot(item, list[i].itemAmount);
                        } 
                        else
                        {
                            playerSlots[i].UpdataEmptySlot();
                        }
                    }
                    break;
            }
        }

        private void Start()
        {
            for(int i = 0; i < playerSlots.Length; i++)
            {
                playerSlots[i].slotIndex = i;
            }
            isOpened = BagUI.activeInHierarchy;

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                openBagUI();
            }
        }

        public  void openBagUI()
        {
            isOpened = !isOpened;
            BagUI.SetActive(isOpened);
        }

        public void UpdateSlotHightLight(int index)
        {
            foreach(var slot in playerSlots)
            {
                if(slot.isSelected && slot.slotIndex == index)
                {
                    slot.slotHightlight.gameObject.SetActive(true);
                }
                else
                {
                    slot.isSelected = false;
                    slot.slotHightlight.gameObject.SetActive(false);
                }
            }
        }
    }
}
