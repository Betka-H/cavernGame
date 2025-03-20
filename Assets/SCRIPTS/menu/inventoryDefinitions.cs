using System.Collections.Generic;
using UnityEngine;

public class inventoryDefinitions : MonoBehaviour
{
    [Header("mission items")]
    public List<item> missionItems;
    [Header("loot items")]
    public List<item> lootItems;

    [Header("gems")]
    public item gemRed;
    public item gemOrange;
    public item gemYellow;
    public item gemGreen;
    public item gemBlue;
    public item gemPurple;
    public item gemPink;
    public item gemClear;

    [Header("gear")]
    public gear nightVisionGoggles;
    public gear vial; // unused
    public gear pickaxe; // unused
    public gear backpack;
    public gear shoes;
    public gear harderHat;

    [Header("scrap")]
    public scrap scrapGoggles;
    public scrap scrapVial; // unused
    public scrap scrapPickaxe; // unused
    public scrap scrapBackpack;
    public scrap scrapShoes;
    public scrap scrapHarderHat;
}