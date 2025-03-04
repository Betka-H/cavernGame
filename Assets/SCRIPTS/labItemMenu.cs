using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using System;

public enum itemDispType { all, loot, scrap, gear };
public class labItemMenu : MonoBehaviour
{
    inventoryManager inventoryScript;
    [HideInInspector] public labInvItem[] regularGridSlots;
    [HideInInspector] public labInvItem[] recipeGridSlots;
    public Transform recipeGridTransform;
    [HideInInspector] public item selectedItem;
    [HideInInspector] public itemDispType itemDispType;

    void Awake()
    {
        regularGridSlots = GetComponentsInChildren<labInvItem>();
        recipeGridSlots = recipeGridTransform.GetComponentsInChildren<labInvItem>();
        inventoryScript = FindObjectOfType<inventoryManager>();
    }

    void OnEnable()
    {
        selectedItem = null;
        showInfo();
        refreshItems(inventoryScript.labInventory, regularGridSlots, itemDispType.all);
    }

    public void refreshItems(List<item> itemList, labInvItem[] slots, itemDispType dt)
    {
        List<item> tempInv = groupItems(itemList, dt);

        if (slots != null)
        {
            for (int i = 0; i < slots.Length; i++) // for each slot
            {
                labInvItem currentSlot = slots[i];

                if (tempInv.Count > 0) // as long as there are items left
                {
                    item currentItem = tempInv[0];

                    // count how many of the item there is in the inventory
                    int amount = tempInv.Count(item => item == currentItem);
                    // Debug.Log($"there is {amount} of {currentItem} in the inventory");

                    // for the current slot, assign the item & how much of it there was
                    currentSlot.assignItem(currentItem, amount);

                    // remove all instances of the item in the temp inv
                    tempInv.RemoveAll(item => item == currentItem);
                }
                else
                {
                    currentSlot.assignItem(null, 0);
                    // Debug.Log("empty");
                }
            }
        }
    }

    private List<item> groupItems(List<item> itemList, itemDispType dt)
    {
        inventoryScript.sortInventory(ref itemList);
        List<item> groupingInv = new List<item>(itemList);
        // Debug.Log("before grouped");
        // inventoryScript.printInventory(groupingInv);

        Type type = null;
        switch (dt)
        {
            case itemDispType.all:
                type = typeof(item);
                break;
            case itemDispType.loot:
                // type = typeof(item);
                type = typeof(loot);
                break;
            case itemDispType.scrap:
                // type = typeof(item);
                type = typeof(scrap);
                break;
        }
        groupingInv = groupingInv.Where(it => type.IsAssignableFrom(it.GetType())).ToList();
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