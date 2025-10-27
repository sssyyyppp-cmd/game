using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI typeText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Text valueText;
    [SerializeField] private GameObject bottomPart;

    public void setupToolTip(ItemDetails item , SlotType slot) 
    {
        nameText.text = item.itemName;

        typeText.text = GetItemString(item.itemType);

        descriptionText.text = item.itemDescription;

        if(item.itemType == ItemType.Seed || item.itemType == ItemType.Commodity || item.itemType == ItemType.Furniture)
        {
            bottomPart.SetActive(true);
            var price = item.itemPrice;
            if(slot == SlotType.Bag)
            {
                price = (int)(price * item.sellPercentage);
            }

            valueText.text = price.ToString();
        }
        else
        {
            bottomPart.SetActive(false);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

    private string GetItemString(ItemType itemType)
    {
        return itemType switch
        {
            ItemType.Seed => "种子",
            ItemType.CollectTool => "种子",
            ItemType.ChopTool => "种子",
            ItemType.HoeTool => "种子",
            ItemType.Commodity => "种子",
            ItemType.Furniture => "种子",



        };
    }
}
