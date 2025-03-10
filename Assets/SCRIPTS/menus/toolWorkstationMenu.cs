using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class toolWorkstationMenu : MonoBehaviour
{
    public Transform recipeGridTransform;
    [HideInInspector] public labInvItem[] recipeGridSlots;

    scrap assignedScrap;
    public SpriteRenderer scrapSpriteRenderer;
    public SpriteRenderer resultSpriteRenderer;
    menuManager menuManager;
    public Sprite placeholderScrapItemSprite;
    public Sprite placeholderResultItemSprite;

    void Awake()
    {
        menuManager = FindObjectOfType<menuManager>();
        recipeGridSlots = recipeGridTransform.GetComponentsInChildren<labInvItem>(true);
    }

    void OnEnable()
    {
        assignScrap(null);
    }

    public void assignScrap(scrap scrap)
    {
        assignedScrap = scrap;
        if (assignedScrap != null)
        {
            scrapSpriteRenderer.sprite = assignedScrap.itemSprite;
            resultSpriteRenderer.sprite = assignedScrap.wholeGear.itemSprite;
            menuManager.labItemMenu.itemGrid.refreshItems(recipeGridSlots, assignedScrap.wholeGear.cost.ToList());
        }
        else
        {
            scrapSpriteRenderer.sprite = placeholderScrapItemSprite;
            resultSpriteRenderer.sprite = placeholderResultItemSprite;
            menuManager.labItemMenu.itemGrid.refreshItems(recipeGridSlots, new List<item>());
        }
    }

    public void createWorkshopGear()
    {
        if (assignedScrap != null && menuManager.inventoryManager.checkResources(menuManager.inventoryManager.labInventory, assignedScrap.wholeGear.cost.ToList()))
        {
            // add crafted item
            menuManager.inventoryManager.addItem(assignedScrap.wholeGear, menuManager.inventoryManager.labInventory);
            // remove scrap item
            menuManager.inventoryManager.removeItem(assignedScrap, menuManager.inventoryManager.labInventory);
            // remove all recipe resources
            foreach (item it in assignedScrap.wholeGear.cost)
            {
                menuManager.inventoryManager.removeItem(it, menuManager.inventoryManager.labInventory);
            }

            // clear scrap and recipe displays
            assignScrap(null);
            menuManager.labItemMenu.itemGrid.refreshItems(menuManager.labItemMenu.itemGrid.regularSlots, menuManager.inventoryManager.labInventory);
            menuManager.labItemMenu.itemGrid.refreshItems(recipeGridSlots, null);
            // menuManager.itemInfoDisplay.selectedItem = null;
            menuManager.itemInfoDisplay.setInfo(null);
        }
        else Debug.Log("no offer or not enough resources");
    }
}
