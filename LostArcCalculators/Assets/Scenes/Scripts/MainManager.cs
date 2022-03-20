using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;


     

public class MainManager : Singleton<MainManager>
{
    [SerializeField] private UIPalenItemList UIPanelItemList;
    [SerializeField] private UIItemListItem _listItemPrefab;

    [SerializeField]  private List<int> _marktePriceList = new List<int>();

    void Start()
    {
        LoadMarketPrice();
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

        SetLastMarketValue();
    }

    private void SetLastMarketValue()
    {
        var allItems = UIPanelItemList.AllItems;
        for (int i = 0; i < allItems.Count; i++)
        {
            allItems[i].Input_1.text = _marktePriceList[i].ToString();
        }
    }

    public void ReadMarketPrice()
    {
        var allItem = UIPanelItemList.AllItems;
        _marktePriceList.Clear();
        for (int i = 0; i < allItem.Count; i++)
        {
            string inputText = allItem[i].Input_1.text;

            if (int.TryParse(inputText, out int v))
                _marktePriceList.Add(v);
            else
                _marktePriceList.Add(0);
        }
    }

    private int GetMarketValue(ItemCodes code)
    {
        int c = (int)code;
        if (c < _marktePriceList.Count)
            return _marktePriceList[c];
        else
            return 0;
    }


    private void SaveMarketPrice()
    {
        for(int i=0;i< _marktePriceList.Count; i++)
        {
            PlayerPrefs.SetInt("item_market_price_"+i, _marktePriceList[i]);
        }

        PlayerPrefs.Save();
    }

    private void LoadMarketPrice()
    {
        _marktePriceList.Clear();
        for (int i = 0; i < (int)ItemCodes.MAX; i++)
        {
            _marktePriceList.Add(PlayerPrefs.GetInt("item_market_price_" + i, 0));
        }
    }

    public void OnClickCalculate()
    {
        ReadMarketPrice();
        SaveMarketPrice();
    }

}
