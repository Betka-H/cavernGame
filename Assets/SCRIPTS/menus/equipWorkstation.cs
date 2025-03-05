using UnityEngine;

public class equipWorkstation : MonoBehaviour
{
    public SpriteRenderer[] slotRenderers;
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
            if (menuManager.inventoryManager.equippedItems.Count > i)
                slotRenderers[i].sprite = menuManager.inventoryManager.equippedItems[i].itemSprite;
            else
                slotRenderers[i].sprite = null;
        }
    }

    public void equip()
    {
        item equippingItem = menuManager.itemInfoDisplay.selectedItem;
        if (equippingItem is gear gear && menuManager.inventoryManager.equippedItems.Count + 1 <= slotRenderers.Length)
        {
            menuManager.inventoryManager.addItem(gear, menuManager.inventoryManager.equippedItems);
            menuManager.inventoryManager.removeItem(gear, menuManager.inventoryManager.labInventory);

            // menuManager.itemInfoDisplay.selectedItem = null;
            menuManager.itemInfoDisplay.setInfo(null);

            // refresh item displays
            refreshItemDisplays();
        }
        else Debug.Log("item not gear or not enough space");
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
        else Debug.Log("no item to remove");
    }

    void refreshItemDisplays()
    {
        menuManager.labItemMenu.refreshItems(menuManager.labItemMenu.regularSlots, menuManager.inventoryManager.labInventory);
        displayEquipment();
    }
}
