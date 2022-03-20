using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemCodes:int
{
    HonorLeapstone = 0,
    GreatHonorLeapstone = 1,
    HonorShardPouch = 2,
    SolarGrace = 3,
    SolarBlessing = 4,
    SolarProtection = 5,
    DestructionStoneCrystal = 6,
    GuardianStoneCrystal = 7,
    PowderOfSage = 8,
    MAX = 9
}

public enum ItemRarityTypes
{
    Cammon,
    Normal,
    Rare,
    Epic,
    Legendary,
    Relic
}

[CreateAssetMenu(menuName = "ItemData")]
public class ItemData : ScriptableObject
{
    public ItemCodes ItemCode;
    public ItemRarityTypes ItemRarity;
    public Sprite ItemIcon;
    public string ItemName;   
}
