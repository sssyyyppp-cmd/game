using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MFarm.Invrntory
{
    public class Item : MonoBehaviour
    {

        public int itemID;

        private SpriteRenderer spriteRenderer;

        public ItemDetails itemDetails;

        private void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        void Start()
        {
            if (itemID != 0)
            {
                Init(itemID);
            }
        }

        public void Init(int id)
        {
            itemID = id;

            Debug.Log(InventoryManager.Instance);

            itemDetails = InventoryManager.Instance.GetItemDetails(itemID);

            if(itemDetails != null)
            {
                spriteRenderer.sprite = itemDetails.itemOnWordSprite != null ? itemDetails.itemOnWordSprite : itemDetails.itemIcon;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
