using UnityEngine;

public class equipWorkstation : MonoBehaviour
{
    public SpriteRenderer[] slotRenderers;
    menuManager menuManager;

    void Awake()
    {
        menuManager = FindObjectOfType<menuManager>();
        displayEquipment();
    }

    void displayEquipment()
    {
        for (int i = 0; i < slotRenderers.Length; i++)
        {
            if (menuManager.inventoryManager.equippedItems.Count > i)
                slotRenderers[i].sprite = menuManager.inventoryManager.equippedItems[i].itemSprite;
            else slotRenderers[i].sprite = null;
        }
    }

    public void equip()
    {
        item equippingItem = menuManager.itemInfoDisplay.selectedItem;
        if (equippingItem is gear gear)
        {
            menuManager.inventoryManager.addItem(gear, menuManager.inventoryManager.equippedItems);
            displayEquipment();
            // menuManager.itemInfoDisplay.selectedItem = null;
            menuManager.itemInfoDisplay.showInfo(null);
        }
        else Debug.Log("item not gear");
    }
    public void unEquip(int pos)
    {
        if (menuManager.inventoryManager.equippedItems.Count > pos)
        {
            menuManager.inventoryManager.removeItem(menuManager.inventoryManager.equippedItems[pos], menuManager.inventoryManager.equippedItems);
            displayEquipment();
        }
        else Debug.Log("no item to remove");
    }
}
