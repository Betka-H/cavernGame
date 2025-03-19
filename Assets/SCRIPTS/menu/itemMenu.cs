using System.Collections.Generic;
using UnityEngine;

public abstract class itemMenu : MonoBehaviour
{
    [HideInInspector] public inventoryManager inventoryScript;
    public Transform gridSlotsParent;
    [HideInInspector] public invItem[] regularSlots;
    [HideInInspector] public item selectedItem;

    [HideInInspector] public bool isAwake;
    protected void Awake()
    {
        regularSlots = gridSlotsParent.GetComponentsInChildren<invItem>(true);
        // Debug.LogWarning($"found slots: {regularSlots.Length}");
        inventoryScript = FindObjectOfType<inventoryManager>();
        isAwake = true;
        // Debug.LogWarning($"awaking 2");
    }
    public void wakeUp()
    {
        Awake();
    }
    void OnEnable()
    {
        // if (isAwake)
        selectedItem = null;
    }

    public abstract void refreshItems(invItem[] slots, List<item> itemList);
    public abstract void refreshItems(invItem[] slots, List<item> hasList, List<item> neededList);

    /* private List<item> groupItems(List<item> itemList, itemDispType dt)
    {
        inventoryScript.orderInventory(ref itemList);
        List<item> groupingInv = new List<item>(itemList);
        // Debug.Log("before grouped");
        // inventoryScript.printInventory(groupingInv);

        Type type = null;
        switch (dt)
        {
            case itemDispType.all:
                type = typeof(item);
                break;
            case itemDispType.loot:
                // type = typeof(item);
                type = typeof(loot);
                break;
            case itemDispType.scrap:
                // type = typeof(item);
                type = typeof(scrap);
                break;
        }
        groupingInv = groupingInv.Where(it => type.IsAssignableFrom(it.GetType())).ToList();
        return groupingInv;
    } */
}