using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class toolWorkstationMenu : MonoBehaviour
{
    scrap assignedScrap;
    public SpriteRenderer scrapSpriteRenderer;
    public SpriteRenderer resultSpriteRenderer;
    menuManager menuManager;
    public Sprite placeholderScrapItemSprite;
    public Sprite placeholderResultItemSprite;

    void Awake()
    {
        menuManager = FindObjectOfType<menuManager>();
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
            menuManager.labItemMenu.refreshItems(menuManager.labItemMenu.recipeGridSlots, assignedScrap.wholeGear.cost.ToList());
        }
        else
        {
            scrapSpriteRenderer.sprite = placeholderScrapItemSprite;
            resultSpriteRenderer.sprite = placeholderResultItemSprite;
            menuManager.labItemMenu.refreshItems(menuManager.labItemMenu.recipeGridSlots, new List<item>());
        }
    }

    public void createWorkshopGear()
    {
        if (assignedScrap != null && checkResources())
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
            menuManager.labItemMenu.refreshItems(menuManager.labItemMenu.regularSlots, menuManager.inventoryManager.labInventory);
            menuManager.labItemMenu.refreshItems(menuManager.labItemMenu.recipeGridSlots, null);
            // menuManager.itemInfoDisplay.selectedItem = null;
            menuManager.itemInfoDisplay.setInfo(null);
        }
        else Debug.Log("no offer or not enough resources");
    }
    bool checkResources()
    {
        List<item> checkInv = new List<item>(menuManager.inventoryManager.labInventory);
        bool hasResources = true;
        while (hasResources)
        {
            foreach (item it in assignedScrap.wholeGear.cost)
            {
                if (checkInv.Contains(it))
                    checkInv.Remove(it);
                else hasResources = false;
            }
            break;
        }
        Debug.Log($"resources? {hasResources}");
        return hasResources;
    }
}
