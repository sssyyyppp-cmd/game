using UnityEngine;
[System.Serializable]//保证类被unity识别（序列化）
public class ItemDetails 
{
    //物品id
    public int id;
    //物品名称
    public string itemName;
    //物品类型
    public ItemType itemType;
    //物品图片（背包）
    public Sprite itemIcon;
    //物品图片（世界）
    public Sprite itemOnWordSprite;
    //物品详情
    public string itemDescription;
    //使用范围
    public int itemUseRadius;
    //物品是否可以拿在手里
    public bool canCarried;
    //物品是否可以丢掉
    public bool canDroped;
    //物品是否可以拾起
    public bool canPickUp;
    //物品价值
    public int itemPrice;
    [Range(0,1)]
    //售卖折扣百分比
    public float sellPercentage;
}
[System.Serializable]
public struct InventoryItem
{
    public int ItemID;

    public int itemAmount;
}