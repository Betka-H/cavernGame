using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class labItemGrid : itemMenu
{
    new void Awake()
    {
        isAwake = true;
        base.Awake();
    }

    public override void refreshItems(invItem[] slots, List<item> hasList, List<item> neededList)
    {
        Debug.LogError($"invalid item refresh request");
    }
    public override void refreshItems(invItem[] slots, List<item> itemList)
    {
        if (isAwake)
        {
            inventoryManager.sortInventory(ref itemList);
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
}