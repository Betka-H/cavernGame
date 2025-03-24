using UnityEngine;
using UnityEngine.SceneManagement;

public class labItemMenu : MonoBehaviour
{
    public labItemGrid itemGrid;

    void OnEnable()
    {
        if (itemGrid.isAwake)
            itemGrid.refreshItems(itemGrid.regularSlots, itemGrid.inventoryManager.labInventory);
    }
}