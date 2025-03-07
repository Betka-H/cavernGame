using UnityEngine;
using UnityEngine.SceneManagement;

public class labItemMenu : MonoBehaviour
{
    public labItemGrid itemGrid;

    // bool isStarted;
    void Start()
    {
        // isStarted = true;
    }

    void OnEnable()
    {
        // if (isStarted)
        if (itemGrid.isAwake)
        {
            // Debug.Log($"item grid enabled {itemGrid.regularSlots}, {itemGrid.inventoryScript.labInventory}");
            itemGrid.refreshItems(itemGrid.regularSlots, itemGrid.inventoryScript.labInventory);
        }
    }
}