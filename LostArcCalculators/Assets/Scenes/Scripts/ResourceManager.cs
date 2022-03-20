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



}
