using Codice.Client.BaseCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemEditor : EditorWindow
{
    private ItemDataList_SO dataBase;

    private List<ItemDetails> itemList = new List<ItemDetails>();

    private VisualTreeAsset itemRowTemplate;

    private ListView itemListView;

    private ScrollView itemDetailsSection;

    private ItemDetails activeItem;

    private VisualElement iconPerview;

    private Sprite defaultIcon;

    private Sprite defaultImage;

    private Button addButton;

    private Button deleteButton;

    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    [MenuItem("M STUDIO/ItemEditor")]
    public static void ShowExample()
    {
        ItemEditor wnd = GetWindow<ItemEditor>();
        wnd.titleContent = new GUIContent("ItemEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        //VisualElement label = new Label("Hello World! From C#");
        //root.Add(label);

        //// Instantiate UXML
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);

        defaultIcon = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Player/PNG/Vector Parts/Sword.png");
        defaultImage = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Player/PNG/Vector Parts/Sword.png");
        //获取数据源
        itemRowTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UI Builder/ItemRowTemplate.uxml");
        //获取加载位置
        itemListView = root.Q<VisualElement>("ItemList").Q<ListView>("ListView");

        itemDetailsSection = root.Q<ScrollView>("ItemDetails");
        iconPerview = itemDetailsSection.Q<VisualElement>("icon");

        addButton = root.Q<VisualElement>("ItemList").Q<Button>("Button");

        //加载数据
        LoadDataBase();
        //生成listView
        ProductListView();
        //添加ListItem
        addListItem();

    }

    void LoadDataBase() 
    {
        var dataArray = AssetDatabase.FindAssets("ItemDataList_SO");
        if(dataArray.Length > 1)
        {
            var path = AssetDatabase.GUIDToAssetPath(dataArray[0]);
            dataBase = AssetDatabase.LoadAssetAtPath(path, typeof(ItemDataList_SO)) as ItemDataList_SO;

            itemList = dataBase.itemDataList;

            //不进行标记就不能保存数据
            EditorUtility.SetDirty(dataBase);
        }
    }

    void ProductListView()
    {
        Func<VisualElement> makeItem = () => itemRowTemplate.CloneTree();
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            if(i < itemList.Count)
            {
                if (itemList[i].itemIcon != null)
                   e.Q<VisualElement>("icon").style.backgroundImage = itemList[i].itemIcon.texture;
                if (itemList[i].itemName != null)
                    e.Q<Label>("name").text = itemList[i].itemName;
            }
        };

        itemListView.fixedItemHeight = 60;
        itemListView.itemsSource = itemList;
        itemListView.makeItem = makeItem;
        itemListView.bindItem = bindItem;

        itemListView.selectionChanged += OnListSelectionChange;

        itemDetailsSection.visible = false;
    }

    private void OnListSelectionChange(IEnumerable<object> enumerable)
    {
        itemDetailsSection.visible = true;

        activeItem = (ItemDetails)enumerable.First();
        GetItemDetails();
    }

    void GetItemDetails()
    {
        itemDetailsSection.MarkDirtyRepaint(); // 确保重绘

        itemDetailsSection.Q<IntegerField>("ItemId").value = activeItem.id;

        itemDetailsSection.Q<IntegerField>("ItemId").RegisterValueChangedCallback(evt =>
        {
            activeItem.id = evt.newValue;
        });

        itemDetailsSection.Q<TextField>("ItemName").value = activeItem.itemName;

        itemDetailsSection.Q<TextField>("ItemName").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemName = evt.newValue;
            itemListView.Rebuild();
        });


        iconPerview.style.backgroundImage = activeItem.itemIcon == null ? defaultIcon.texture : activeItem.itemIcon.texture;
        itemDetailsSection.Q<ObjectField>("ItemIcon").value = activeItem.itemIcon;
        itemDetailsSection.Q<ObjectField>("ItemIcon").RegisterValueChangedCallback(evt =>
        {
            Sprite newIcon = evt.newValue as Sprite;
            activeItem.itemIcon = newIcon;
            if (newIcon != null)
                iconPerview.style.backgroundImage = newIcon == null ? defaultIcon.texture : newIcon.texture;
            itemListView.Rebuild();
        });

        itemDetailsSection.Q<ObjectField>("ItemImage").value = activeItem.itemOnWordSprite;
        itemDetailsSection.Q<ObjectField>("ItemImage").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemOnWordSprite = evt.newValue as Sprite;
            itemListView.Rebuild();
        });

        itemDetailsSection.Q<TextField>("itemDescription").value = activeItem.itemDescription;

        itemDetailsSection.Q<TextField>("itemDescription").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemDescription = evt.newValue;
            itemListView.Rebuild();
        });

        itemDetailsSection.Q<IntegerField>("useRadis").value = activeItem.itemUseRadius;

        itemDetailsSection.Q<IntegerField>("useRadis").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemUseRadius = evt.newValue;
            itemListView.Rebuild();
        });

        itemDetailsSection.Q<Toggle>("CanPickedup").value = activeItem.canPickUp;

        itemDetailsSection.Q<Toggle>("CanPickedup").RegisterValueChangedCallback(evt =>
        {
            activeItem.canPickUp = evt.newValue;
            itemListView.Rebuild();
        });

        itemDetailsSection.Q<Toggle>("CanDroppod").value = activeItem.canDroped;

        itemDetailsSection.Q<Toggle>("CanDroppod").RegisterValueChangedCallback(evt =>
        {
            activeItem.canDroped = evt.newValue;
            itemListView.Rebuild();
        });

        itemDetailsSection.Q<Toggle>("CanCarried").value = activeItem.canCarried;

        itemDetailsSection.Q<Toggle>("CanCarried").RegisterValueChangedCallback(evt =>
        {
            activeItem.canCarried = evt.newValue;
            itemListView.Rebuild();
        });

        itemDetailsSection.Q<IntegerField>("buy").value = activeItem.itemPrice;

        itemDetailsSection.Q<IntegerField>("buy").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemPrice = evt.newValue;
            itemListView.Rebuild();
        });

        itemDetailsSection.Q<Slider>("percent").value = activeItem.sellPercentage;

        itemDetailsSection.Q<Slider>("percent").RegisterValueChangedCallback(evt =>
        {
            activeItem.sellPercentage = evt.newValue;
            itemListView.Rebuild();
        });

        deleteButton = itemDetailsSection.Q<Button>("Button");
        deleteButton.RegisterCallback<MouseUpEvent>((evt) =>
        {
            itemList.Remove(activeItem);
            itemListView.Rebuild();
            itemDetailsSection.visible = false;
        });

    }

    void addListItem()
    {
        addButton.RegisterCallback<MouseUpEvent>((evt) =>
        {
            var newItem = new ItemDetails{
                id = itemList.Count > 0 ? itemList.Max(item => item.id) + 1 : 1001,
                itemName = "New Item",
                itemIcon = defaultIcon,
                itemOnWordSprite = defaultImage,
                itemDescription = "",
                itemUseRadius = 0,
                canPickUp = false,
                canDroped = false,
                canCarried = false,
                itemPrice = 0,
                sellPercentage = 0f
            };
            itemList.Add(newItem);
            itemListView.Rebuild();
        });
    }
}
