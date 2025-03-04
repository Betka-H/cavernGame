using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class labItemMenu : itemMenu
{
    [HideInInspector] public labInvItem[] recipeGridSlots;
    public Transform recipeGridTransform;

    new void Awake()
    {
        base.Awake();
        recipeGridSlots = recipeGridTransform.GetComponentsInChildren<labInvItem>(true);
        inventoryScript = FindObjectOfType<inventoryManager>();
    }

    void OnEnable()
    {
        refreshItems(regularSlots, inventoryScript.labInventory);
    }

    public override void refreshItems(invItem[] slots, List<item> itemList)
    {
        inventoryScript.orderInventory(ref itemList);
        List<item> tempInv;
        if (itemList == null)
            tempInv = new List<item>();
        else tempInv = new List<item>(itemList);

        if (slots != null)
        {
            for (int i = 0; i < slots.Length; i++) // for each slot
            {
                labInvItem currentSlot = slots[i] as labInvItem;
                if (tempInv.Count > 0) // as long as there are items left
                {
                    item currentItem = tempInv[0];

                    // count items
                    int amount = tempInv.Count(item => item == currentItem);
                    // assign item and its amount
                    currentSlot.assignItem(currentItem, amount);
                    // remove item
                    tempInv.RemoveAll(item => item == currentItem);
                }
                else currentSlot.assignItem(null, 0);
            }
        }
    }
}