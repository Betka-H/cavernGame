using System.Collections.Generic;
using UnityEngine;

public abstract class itemMenu : MonoBehaviour
{
    [HideInInspector] public inventoryManager inventoryManager;
    public Transform gridSlotsParent;
    [HideInInspector] public invItem[] regularSlots;
    [HideInInspector] public item selectedItem;

    [HideInInspector] public bool isAwake;
    protected void Awake()
    {
        regularSlots = gridSlotsParent.GetComponentsInChildren<invItem>(true);
        inventoryManager = FindObjectOfType<inventoryManager>();
        isAwake = true;
    }
    public void wakeUp()
    {
        Awake();
    }
    void OnEnable()
    {
        selectedItem = null;
    }

    // for simple display
    public abstract void refreshItems(invItem[] slots, List<item> itemList);
    // for out of display
    public abstract void refreshItems(invItem[] slots, List<item> hasList, List<item> neededList);
}