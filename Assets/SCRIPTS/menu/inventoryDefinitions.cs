using System.Collections.Generic;
using UnityEngine;

public class inventoryDefinitions : MonoBehaviour
// enables checking for specific items
{
    [Header("mission items")]
    public List<item> missionItems; // mission item pool
    [Header("loot items")]
    public List<item> lootItems; // cave loot item pool

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