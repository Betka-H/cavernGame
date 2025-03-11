using UnityEngine;

public class equipWorkstation : MonoBehaviour
{
    public SpriteRenderer[] slotRenderers;
    public itemInfoDisplay[] infoDisplays;
    menuManager menuManager;

    void Awake()
    {
        menuManager = FindObjectOfType<menuManager>();
    }

    /* public void clearSlots()
    {
        for (int i = 0; i < slotRenderers.Length; i++)
            slotRenderers[i].sprite = null;
    } */

    void OnEnable()
    {
        displayEquipment();
    }

    void displayEquipment()
    {
        for (int i = 0; i < slotRenderers.Length; i++)
        {
            item selItem;
            if (menuManager.inventoryManager.equippedItems.Count > i)
            {
                selItem = menuManager.inventoryManager.equippedItems[i];
                slotRenderers[i].sprite = selItem.itemSprite;
            }
            else
            {
                selItem = null;
                slotRenderers[i].sprite = null;
            }
            infoDisplays[i].setInfo(selItem);
        }
    }

    public void equip()
    {
        item equippingItem = menuManager.itemInfoDisplay.selectedItem;
        // Debug.Log($"eq: {equippingItem}");
        if (equippingItem != null)
        {
            if (equippingItem is gear gear)
            {
                if (menuManager.inventoryManager.equippedItems.Count + 1 <= slotRenderers.Length)
                {
                    if (!menuManager.inventoryManager.equippedItems.Contains(equippingItem))
                    {
                        menuManager.inventoryManager.addItem(gear, menuManager.inventoryManager.equippedItems);
                        menuManager.inventoryManager.removeItem(gear, menuManager.inventoryManager.labInventory);

                        // menuManager.itemInfoDisplay.selectedItem = null;
                        menuManager.itemInfoDisplay.setInfo(null);

                        // refresh item displays
                        refreshItemDisplays();
                    }
                    else
                    {
                        FindObjectOfType<announcerManager>().announceMessage($"you cannot equip the same item twice!");
                    }
                }
                else
                {
                    FindObjectOfType<announcerManager>().announceMessage($"not enough space to equip item!");
                }
            }
            else
            {
                FindObjectOfType<announcerManager>().announceMessage($"this item is not gear!");
            }
        }
        else
        {
            FindObjectOfType<announcerManager>().announceMessage($"no item selected!");
        }
        // else Debug.Log("item not gear, not enough space or twice");
        // Debug.LogWarning("show err messages for these cases");
    }
    public void unEquip(int pos)
    {
        Debug.Log($"?!?!?!?!?!?");
        if (menuManager.inventoryManager.equippedItems.Count > pos)
        {
            item gear = menuManager.inventoryManager.equippedItems[pos];
            menuManager.inventoryManager.addItem(gear, menuManager.inventoryManager.labInventory);
            menuManager.inventoryManager.removeItem(gear, menuManager.inventoryManager.equippedItems);
            refreshItemDisplays();
        }
        else
        {
            // Debug.Log("no item to remove");
            FindObjectOfType<announcerManager>().announceMessage($"no item to unequip!");
        }
    }

    void refreshItemDisplays()
    {
        menuManager.labItemMenu.itemGrid.refreshItems(menuManager.labItemMenu.itemGrid.regularSlots, menuManager.inventoryManager.labInventory);
        displayEquipment();
    }
}
