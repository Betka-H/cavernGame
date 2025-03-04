using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class toolWorkstationMenu : MonoBehaviour
{
    scrap assignedScrap;
    public SpriteRenderer scrapSpriteRenderer;
    public SpriteRenderer resultSpriteRenderer;
    inventoryManager inventoryManager;
    menuManager menuManager;
    public Sprite placeholderScrapItemSprite;
    public Sprite placeholderResultItemSprite;

    void Awake()
    {
        inventoryManager = FindObjectOfType<inventoryManager>();
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
            inventoryManager.addItem(assignedScrap.wholeGear, inventoryManager.labInventory);
            // remove scrap item
            inventoryManager.removeItem(assignedScrap, inventoryManager.labInventory);
            // remove all recipe resources
            foreach (item it in assignedScrap.wholeGear.cost)
            {
                inventoryManager.removeItem(it, inventoryManager.labInventory);
            }

            // clear scrap and recipe displays
            assignScrap(null);
            menuManager.labItemMenu.refreshItems(menuManager.labItemMenu.regularSlots, inventoryManager.labInventory);
            menuManager.labItemMenu.refreshItems(menuManager.labItemMenu.recipeGridSlots, null);
            menuManager.itemInfoDisplay.showInfo(null);
        }
        else Debug.Log("no offer or not enough resources");
    }
    bool checkResources()
    {
        List<item> checkInv = new List<item>(inventoryManager.labInventory);
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
