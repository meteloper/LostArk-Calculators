using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;


     

public class MainManager : Singleton<MainManager>
{
    [SerializeField] private UIPalenItemList UIPanelItemList;
    [SerializeField] private UIItemListItem _listItemPrefab;

    [SerializeField]  private List<int> _marktePriceList = new List<int>();

    [SerializeField] private List<int> _itemsVendorCountList = new List<int>();
    [SerializeField] private List<int> _itemsVendorPriceList = new List<int>();
    [SerializeField] private List<int> _itemsProfitList = new List<int>();

    [SerializeField] private int _totalVenderPrice = 0;
    [SerializeField] private int _totalProfit = 0;
    [SerializeField] private float _lastRate = 0;


    void Start()
    {
        LoadMarketPrice();
        ResetPanel();
        CreateListItems();
    }

    private void CalculateBest()
    {
        if(float.TryParse(UIPanelItemList.Input_1.text.ToString(), out float expectedRate))
        {
            int itemCount = (int)ItemCodes.MAX;
            _itemsVendorCountList = new List<int>(new int[itemCount]);
            _itemsVendorPriceList = new List<int>(new int[itemCount]);
            _itemsProfitList = new List<int>(new int[itemCount]);
            _lastRate = 0;
            _totalVenderPrice = 0;
            _totalProfit = 0;

            while (true)
            {
                int bestItemIndex = -1;
                float bestRate = 0;
                int bestMarketPrice = 0;
                int bestVendorPrice= 0;

                for (int i = 0; i < itemCount; i++)
                {
                    ItemCodes currentItem = (ItemCodes)i;
                    Debug.Log($"{i}");
                    int currentVendorPrice = ResourceManager.GetCurrentRankVendorPrice(currentItem, _itemsVendorCountList[i]);
                    int currentMarketPrice = GetMarketValue(currentItem);
                    float currentRate = (float)currentMarketPrice / currentVendorPrice;
                    if (currentRate > bestRate)
                    {
                        bestRate = currentRate;
                        bestItemIndex = i;
                        bestMarketPrice = currentMarketPrice;
                        bestVendorPrice = currentVendorPrice;
                    }
                }

                if (bestItemIndex == -1 || ((float)(_totalProfit + bestMarketPrice) / (_totalVenderPrice + bestVendorPrice)) < expectedRate)
                    break;

                _totalVenderPrice += bestVendorPrice;
                _totalProfit += bestMarketPrice;
                _lastRate = (float)_totalProfit / _totalVenderPrice;

                _itemsVendorCountList[bestItemIndex] += 1;
                _itemsVendorPriceList[bestItemIndex] += bestVendorPrice;
                _itemsProfitList[bestItemIndex] += bestMarketPrice;
            }
        }
        else
        {
            Debug.Log("Float not valid: " + UIPanelItemList.Input_1.ToString());
        }
    }



    private void SetTables()
    {
        var allItems = UIPanelItemList.AllItems;

        for (int i = 0; i < allItems.Count; i++)
        {
            if (i < _itemsVendorCountList.Count)
                allItems[i].Output_1.text = _itemsVendorCountList[i].ToString() + "x";
            else
                allItems[i].Output_1.text = "0x";

            if (i < _itemsVendorPriceList.Count)
                allItems[i].Output_2.text = _itemsVendorPriceList[i].ToString() + ResourceManager.CS_ICON;
            else
                allItems[i].Output_2.text = $"0{ResourceManager.CS_ICON}";

            if (i < _itemsProfitList.Count)
                allItems[i].Output_3.text = _itemsProfitList[i].ToString() + ResourceManager.GOLD_ICON;
            else
                allItems[i].Output_3.text = $"0{ResourceManager.GOLD_ICON}";
        }

        UIPanelItemList.ResultOutput_1.text = _totalVenderPrice.ToString() + ResourceManager.CS_ICON;
        UIPanelItemList.ResultOutput_2.text = _totalProfit.ToString() + ResourceManager.GOLD_ICON;
        UIPanelItemList.ResultOutput_3.text = $"1{ResourceManager.CS_ICON} = {_lastRate}{ResourceManager.GOLD_ICON}";
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

        if (_marktePriceList!=null && c < _marktePriceList.Count)
        {
            if(code == ItemCodes.SolarGrace)
                return _marktePriceList[c] * 7;
            else if(code == ItemCodes.SolarBlessing)
                return _marktePriceList[c] * 2;

            return _marktePriceList[c];
        }
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
        CalculateBest();
        SetTables();
    }

}
