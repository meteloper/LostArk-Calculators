using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class RarityColors
{
    public Color BackgroundColor;
    public Color BorderColor;
}
public class ResourceManager : Singleton<ResourceManager>
{
    public List<ItemData> ItemDataList;

    public const string CS_ICON = "<sprite name=r1>";
    public const string GOLD_ICON = "<sprite name=r2>";

    private static readonly List<RarityColors> RarityColor = new List<RarityColors>()
    {
        new RarityColors(){ BackgroundColor = new Color(0.22f,0.15f,0.12f), BorderColor = Color.white}, //Cammon
        new RarityColors(){ BackgroundColor = new Color(0.17f,0.25f,0.05f), BorderColor = new Color(0.7f,0.98f,0)}, //Normal
        new RarityColors(){ BackgroundColor = new Color(0.05f,0.27f,0.36f), BorderColor = new Color(0,0.7f,1)}, //Rare
        new RarityColors(){ BackgroundColor = new Color(0.36f,0.05f,0.3f), BorderColor = new Color(0,1,0.78f)}, //Epic
        new RarityColors(){ BackgroundColor = new Color(0.44f,0.29f,0.36f), BorderColor = new Color(1,0.6f,0)}, //Legendary
        new RarityColors(){ BackgroundColor = new Color(), BorderColor = new Color()}, //Relic
    };

    public static RarityColors GetRarityColor(ItemRarityTypes type)
    {
        return RarityColor[(int)type];
    }


    private static readonly int[][] VendorPrices = new int[][]{
        new int[] { 100,130,170,200,230,270,300,340,380,420,460,510,560,610,660,720,int.MaxValue }, //Honor Leapstone 
        new int[] { 130,170,200,240,280,310,360,400,440,490,540,600,650,710,int.MaxValue }, //Great Honor Leapstone
        new int[] { 500,570,650,730,820,910,1010,int.MaxValue }, //Honor Shard Pouch (L)
        new int[] { 550,630,710,800,890,990,1100, int.MaxValue }, //Solar Grace
        new int[] { 570,650,730,820,920,1020,1130, int.MaxValue }, //Solar Blessing
        new int[] { 590,670,760,850,950,1060,1170, int.MaxValue }, //Solar Protection
        new int[] { 60,90,120,140,170,200,230,260,290,330,360,400,430,470,510,560,600,650,int.MaxValue}, //Destruction Stone Crystal
        new int[] { 10,40,60,80,100,120,140,160,190,210,230,250,280,300,320,350,370,400,420,450,470,500,530,int.MaxValue }, //Guartian Stone Crystal
        new int[] { 1470,1640,1820,int.MaxValue }, //Powder of Sage
    };

    public static int GetCurrentRankVendorPrice(ItemCodes itemCode, int rank)
    {
        int length = VendorPrices[(int)itemCode].Length;
        return VendorPrices[(int)itemCode][Mathf.Clamp(rank, 0, length-1)];
    }


}
