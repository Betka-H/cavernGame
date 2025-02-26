using UnityEngine;

[CreateAssetMenu(menuName = "item_loot")]
public class loot : item
{
    [Header("loot")]

    [Tooltip("the larger the number, the more common the item is")]
    [Range(1, 100)]
    public int rarity;
}