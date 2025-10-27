using UnityEngine;
[System.Serializable]//��֤�౻unityʶ�����л���
public class ItemDetails 
{
    //��Ʒid
    public int id;
    //��Ʒ����
    public string itemName;
    //��Ʒ����
    public ItemType itemType;
    //��ƷͼƬ��������
    public Sprite itemIcon;
    //��ƷͼƬ�����磩
    public Sprite itemOnWordSprite;
    //��Ʒ����
    public string itemDescription;
    //ʹ�÷�Χ
    public int itemUseRadius;
    //��Ʒ�Ƿ������������
    public bool canCarried;
    //��Ʒ�Ƿ���Զ���
    public bool canDroped;
    //��Ʒ�Ƿ����ʰ��
    public bool canPickUp;
    //��Ʒ��ֵ
    public int itemPrice;
    [Range(0,1)]
    //�����ۿ۰ٷֱ�
    public float sellPercentage;
}
[System.Serializable]
public struct InventoryItem
{
    public int ItemID;

    public int itemAmount;
}