using UnityEngine;

public class inventoryContents : MonoBehaviour
{
    [Header("assorted list of all existing items by id")]
    public item[] allItemList;

    [Header("specific gear")]
    public gear nightVisionGoggles;
    public gear vial;
    public gear pickaxe;
}