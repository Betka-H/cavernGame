using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class labItemOutOfGrid : itemMenu
{
    new void Awake()
    {
        isAwake = true;
        base.Awake();
        Debug.LogWarning($"awaking 1");
        Debug.LogWarning($"got slots: {regularSlots.Length}");
    }

    

    public override void refreshItems(invItem[] cat, List<item> dog)
    {
        //uhhhh. yea ok
    }
    public override void refreshItems(invItem[] slots, List<item> hasList, List<item> neededList)
    {
        if (isAwake)
        {
            Debug.LogWarning($"found slots: {regularSlots.Length}???????");

            inventoryScript.sortInventory(ref hasList);
            List<item> tempHasInv;
            if (hasList == null)
                tempHasInv = new List<item>();
            else tempHasInv = new List<item>(hasList);

            inventoryScript.sortInventory(ref neededList);
            List<item> tempNeedsInv;
            if (neededList == null)
                tempNeedsInv = new List<item>();
            else tempNeedsInv = new List<item>(neededList);

            /* Debug.Log("what have:");
            inventoryScript.printInventory(tempHasInv);
            Debug.Log("what need:");
            inventoryScript.printInventory(tempNeedsInv); */

            if (slots != null)
            {
                for (int i = 0; i < slots.Length; i++) // for each slot
                {
                    labInvItemOutOf currentSlot = slots[i] as labInvItemOutOf;
                    if (tempNeedsInv.Count > 0) // as long as there are items left
                    {
                        item currentItem = tempNeedsInv[0];

                        // count items
                        int neededAmount = tempNeedsInv.Count(item => item == currentItem);
                        int hasAmount = tempHasInv.Count(item => item == currentItem);
                        // assign item and its amount
                        if (hasAmount <= neededAmount)
                            currentSlot.assignItem(currentItem, hasAmount, neededAmount);
                        else currentSlot.assignItem(currentItem, neededAmount, neededAmount);
                        // remove item
                        tempNeedsInv.RemoveAll(item => item == currentItem);
                        tempHasInv.RemoveAll(item => item == currentItem);
                    }
                    else currentSlot.assignItem(null, 0, 0);
                }
            }
        }
        else
        {
            Debug.LogWarning($"FALSSSSE");
            Awake();
        }
    }
}