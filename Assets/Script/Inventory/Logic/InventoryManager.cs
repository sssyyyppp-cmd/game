using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

namespace MFarm.Invrntory
{
    public class InventoryManager : singloten<InventoryManager>
    {

        [Header("全部物品数据")]
        public ItemDataList_SO itemData;
        [Header("人物背包数据")]
        public InventoryBag_SO playerBag;

        private void Start()
        {
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player , playerBag.itemList);
        }

        public ItemDetails GetItemDetails(int ID)
        {
            return itemData.itemDataList.Find(item => item.id == ID);
        }


        public void PickUpItem(Item item , bool toDestory)
        {
            var index = FindItemIndexInBag(item.itemID);
            AddItemAtIndex(item.itemID, index, 1);

            if (toDestory)
            {
                Destroy(item.gameObject);
            }

            //更新UI
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player , playerBag.itemList);
        }

        public bool CheckBagCapacity()
        {
            for(int i = 0; i < playerBag.itemList.Count; i++)
            {
                if (playerBag.itemList[i].ItemID == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public int FindItemIndexInBag(int ID)
        {
            for (int i = 0; i < playerBag.itemList.Count; i++)
            {
                if (playerBag.itemList[i].ItemID == ID)
                {
                    return i;
                }
            }
            return -1;
        }

        public void　AddItemAtIndex(int id, int index, int amount)
        {
            if(index == -1 && CheckBagCapacity())
            {
                var item = new InventoryItem { ItemID = id ,itemAmount = amount };
                for (int i = 0; i < playerBag.itemList.Count; i++)
                {
                    if (playerBag.itemList[i].ItemID == 0) 
                    {
                        playerBag.itemList[i] = item;
                        break;
                    }
                }
            }
            else
            {
                int currentAmount = playerBag.itemList[index].itemAmount + amount;
                var item = new InventoryItem { ItemID = id, itemAmount = currentAmount };
                playerBag.itemList[index] = item;

            }
        }

        public void SwapItem(int from,int to)
        {
            InventoryItem fromItem = playerBag.itemList[from];
            InventoryItem toItem = playerBag.itemList[to];

            if(toItem.ItemID != 0)
            {
                playerBag.itemList[to] = fromItem;
                playerBag.itemList[from] = toItem;
            }
            else
            {
                playerBag.itemList[to] = fromItem;
                playerBag.itemList[from] = new InventoryItem();
            }

            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.itemList);
        }
    }

}