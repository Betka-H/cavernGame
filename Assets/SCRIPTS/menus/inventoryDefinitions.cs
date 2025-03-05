using System.Collections.Generic;
using UnityEngine;

public class inventoryDefinitions : MonoBehaviour
{
    public List<item> allItems;

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
    public gear vial;
    public gear pickaxe;
    public gear backpack;

    [Header("scrap")]
    public scrap scrapGoggles;
    public scrap scrapVial;
    public scrap scrapPickaxe;
    public scrap scrapBackpack;
}