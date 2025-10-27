using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHandler
{
    public static event Action<InventoryLocation, List<InventoryItem>> updateInventoryUI;

    public static void CallUpdateInventoryUI(InventoryLocation inventoryLocation, List<InventoryItem> list)
    {
        updateInventoryUI?.Invoke(inventoryLocation,list);
    }
    public static event Action<int, Vector3> InstantiateItemInScene;

    public static void CallInstantiateItemInScene(int ID, Vector3 pos)
    {
        InstantiateItemInScene?.Invoke(ID, pos);
    }
}


