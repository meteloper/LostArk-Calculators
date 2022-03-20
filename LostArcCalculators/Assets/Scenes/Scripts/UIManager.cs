using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;


     

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private UIPalenItemList UIPanelItemList;
    [SerializeField] private UIItemListItem _listItemPrefab;

    void Start()
    {
        ResetPanel();
        CreateListItems();
    }


    private void ResetPanel()
    {
        UIPanelItemList.Input_1.text = "0";
        UIPanelItemList.ResultOutput_1.text = $"0{ResourceManager.CS_ICON}";
        UIPanelItemList.ResultOutput_2.text = $"0{ResourceManager.GOLD_ICON}";
        UIPanelItemList.ResultOutput_3.text = "";

        for (int i = 0; i < UIPanelItemList.ItemParent.childCount; i++)
        {
            Destroy(UIPanelItemList.ItemParent.GetChild(i).gameObject);
        }
        UIPanelItemList.AllItems.Clear();
    }


    private void CreateListItems()
    {
        List<ItemData> itemsData = ResourceManager.Instance.ItemDataList;

        for (int i = 0; i < itemsData.Count; i++)
        {
            var instance = Instantiate(_listItemPrefab, UIPanelItemList.ItemParent);

            instance.Icon.sprite = itemsData[i].ItemIcon;

            RarityColors colors = ResourceManager.GetRarityColor(itemsData[i].ItemRarity);
            instance.Background.color = colors.BackgroundColor;
            instance.Border.color = colors.BorderColor;

            instance.Input_1.text = "0";
            instance.Output_1.text = "0x";
            instance.Output_2.text = $"0{ResourceManager.CS_ICON}";
            instance.Output_3.text = $"0{ResourceManager.GOLD_ICON}";

            UIPanelItemList.AllItems.Add(instance);
        }
    }

}
