using UnityEngine;

public class equipWorkstation : MonoBehaviour
{
    public itemInfoDisplay[] infoDisplays;
    public Transform[] unequipButtons;

    public menuManager menuManager;
    public announcerManager announcerManager;

    void OnEnable()
    {
        displayEquipment();
    }

    void displayEquipment()
    {
        for (int i = 0; i < infoDisplays.Length; i++)
        // for all eq slots
        {
            item selItem;
            if (menuManager.inventoryManager.equippedItems.Count > i)
            {
                selItem = menuManager.inventoryManager.equippedItems[i];
                infoDisplays[i].selectedItem = selItem;
                if (unequipButtons.Length > 0)
                    unequipButtons[i].gameObject.SetActive(true);
            }
            else
            {
                selItem = null;
                infoDisplays[i].selectedItem = null;
                if (unequipButtons.Length > 0)
                    unequipButtons[i].gameObject.SetActive(false);
            }
            infoDisplays[i].setInfo(selItem);
        }
    }

    public void equip()
    {
        item equippingItem = menuManager.itemInfoDisplay.selectedItem;
        if (equippingItem != null)
        // item cant be null
        {
            if (equippingItem is gear gear)
            // item has to be gear
            {
                if (menuManager.inventoryManager.equippedItems.Count + 1 <= infoDisplays.Length)
                // there cant be more eq items than there are slots
                {
                    if (!menuManager.inventoryManager.equippedItems.Contains(equippingItem))
                    // cant eq same item twice
                    {
                        menuManager.inventoryManager.addItem(gear, menuManager.inventoryManager.equippedItems);
                        menuManager.inventoryManager.removeItem(gear, menuManager.inventoryManager.labInventory);

                        // deselect
                        menuManager.itemInfoDisplay.setInfo(null);

                        refreshItemDisplays();

                        if (FindObjectOfType<missionManager>().checkCurrentMission(-1, 12))
                        // tutorial
                        {
                            callManager callManager = FindObjectOfType<callManager>();
                            callManager.startCall(callManager.currentMainMission());
                            menuManager.toggleEquipmentWorkstationMenu();
                        }
                    }
                    else announcerManager.announceMessage($"you cannot equip the same item twice!");
                }
                else announcerManager.announceMessage($"not enough space to equip item!");
            }
            else announcerManager.announceMessage($"this item is not gear!");
        }
        else announcerManager.announceMessage($"no item selected!");
    }
    public void unEquip(int pos)
    {
        if (menuManager.inventoryManager.equippedItems.Count > pos)
        {
            item gear = menuManager.inventoryManager.equippedItems[pos];
            menuManager.inventoryManager.addItem(gear, menuManager.inventoryManager.labInventory);
            menuManager.inventoryManager.removeItem(gear, menuManager.inventoryManager.equippedItems);
            refreshItemDisplays();
        }
        else announcerManager.announceMessage($"no item to unequip!");
    }

    void refreshItemDisplays()
    {
        // refresh lab inv item grid
        menuManager.labItemGrid.itemGrid.refreshItems(menuManager.labItemGrid.itemGrid.regularSlots, menuManager.inventoryManager.labInventory);

        // refresh eq disp
        displayEquipment();
    }
}
