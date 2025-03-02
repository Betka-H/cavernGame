using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class labItemMenu : MonoBehaviour
{
    inventoryManager inventoryScript;
    private labInvItem[] gridSlots;

    void Awake()
    {
        gridSlots = GetComponentsInChildren<labInvItem>();
        inventoryScript = FindObjectOfType<inventoryManager>();
    }

    void OnEnable()
    {
        refreshItems();
    }

    void refreshItems()
    {
        inventoryScript.sortInventory(ref inventoryScript.labInventory);
        List<item> cullingInv = new List<item>(inventoryScript.labInventory);

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
                    Debug.Log($"there is {amount} of {currentItem} in the inventory");

                    // remove all instances of the item
                    cullingInv.RemoveAll(item => item == currentItem);

                    // for the current slot, assign the item & how much of it there was
                    currentItemSlot.assignItem(currentItem, amount);
                    Debug.Log($"ci: {currentItem}");
                }
                else currentItemSlot.assignItem(null, 0);
            }
        }
    }
}