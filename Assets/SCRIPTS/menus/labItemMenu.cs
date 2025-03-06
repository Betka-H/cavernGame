using UnityEngine;

public class labItemMenu : MonoBehaviour
{
    public labItemGrid itemGrid;

    void OnEnable()
    {
        itemGrid.refreshItems(itemGrid.regularSlots, itemGrid.inventoryScript.labInventory);
    }
}