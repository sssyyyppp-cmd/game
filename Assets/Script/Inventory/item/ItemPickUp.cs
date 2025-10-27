using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MFarm.Invrntory
{
    public class ItemPickUp : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Item item = collision.GetComponent<Item>();

            if(item != null)
            {
                if(item.itemDetails.canPickUp)
                {
                    Debug.Log(1);
                    InventoryManager.Instance.PickUpItem(item, true);
                }
            }
        }
    }
}
