using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using System;

public enum itemDispType { all, loot, scrap, gear };
public class labItemMenu : MonoBehaviour
{
    inventoryManager inventoryScript;
    private labInvItem[] gridSlots;
    [HideInInspector] public item selectedItem;
    [HideInInspector] public itemDispType itemDispType;

    void Awake()
    {
        gridSlots = GetComponentsInChildren<labInvItem>();
        inventoryScript = FindObjectOfType<inventoryManager>();
    }

    void OnEnable()
    {
        selectedItem = null;
        showInfo();
        refreshItems();
    }

    public void refreshItems()
    {

        // inventoryScript.sortInventory(ref inventoryScript.labInventory);
        // List<item> cullingInv = new List<item>(inventoryScript.labInventory);
        List<item> cullingInv = groupItems(itemDispType);
        // Debug.Log("item refresh: ");
        // inventoryScript.printInventory(cullingInv);

        if (gridSlots != null)
        {
            for (int i = 0; i < gridSlots.Length; i++) // for each slot
            {
                // Debug.Log("culling inv below v v v ");
                // inventoryScript.printInventory(cullingInv);

                labInvItem currentItemSlot = gridSlots[i];

                if (cullingInv.Count > 0) // as long as there are items left
                {
                    item currentItem = cullingInv[0];

                    // count how many of the item there is in the inventory
                    int amount = cullingInv.Count(item => item == currentItem);
                    // Debug.Log($"there is {amount} of {currentItem} in the inventory");

                    // remove all instances of the item
                    cullingInv.RemoveAll(item => item == currentItem);

                    // for the current slot, assign the item & how much of it there was
                    currentItemSlot.assignItem(currentItem, amount);
                    // Debug.Log($"ci: {currentItem}");
                }
                else currentItemSlot.assignItem(null, 0);
            }
        }
    }

    private List<item> groupItems(itemDispType dt)
    {
        inventoryScript.sortInventory(ref inventoryScript.labInventory);
        List<item> groupingInv = new List<item>(inventoryScript.labInventory);
        // Debug.Log("before grouped");
        // inventoryScript.printInventory(groupingInv);

        Type type = null;
        switch (dt)
        {
            case itemDispType.all:
                type = typeof(item);
                break;
            case itemDispType.loot:
                type = typeof(item);
                // type = typeof(loot);
                break;
            case itemDispType.scrap:
                type = typeof(item);
                // type = typeof(scrap);
                break;
        }
        groupingInv = groupingInv.Where(it => type.IsAssignableFrom(it.GetType())).ToList();
        // Debug.Log("grouped");
        // inventoryScript.printInventory(groupingInv);
        return groupingInv;
    }

    [Header("item info section")]
    public SpriteRenderer invSprite_itemSprite;
    public TMP_Text invTxt_itemName;
    public TMP_Text invTxt_itemDescription;

    public Sprite placeholderItemSprite;
    public string placeholderItemName;
    [TextArea] public string placeholderItemDescription;
    public void showInfo()
    {
        if (selectedItem != null)
        {
            invSprite_itemSprite.sprite = selectedItem.itemSprite;
            invTxt_itemName.SetText(selectedItem.itemName);
            invTxt_itemDescription.SetText(selectedItem.itemDescription);
        }
        else
        {
            invSprite_itemSprite.sprite = placeholderItemSprite;
            invTxt_itemName.SetText(placeholderItemName);
            invTxt_itemDescription.SetText(placeholderItemDescription);
        }
    }
}