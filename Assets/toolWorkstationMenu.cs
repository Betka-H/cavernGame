using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
        menuManager.labItemMenu.refreshItems(new List<item>(), menuManager.labItemMenu.recipeGridSlots, itemDispType.all);
    }

    public void assignScrap(scrap scrap)
    {
        assignedScrap = scrap;
        if (assignedScrap != null)
        {
            scrapSpriteRenderer.sprite = assignedScrap.itemSprite;
            resultSpriteRenderer.sprite = assignedScrap.wholeGear.itemSprite;
            menuManager.labItemMenu.refreshItems(assignedScrap.wholeGear.cost.ToList(), menuManager.labItemMenu.recipeGridSlots, itemDispType.all);
        }
        else
        {
            scrapSpriteRenderer.sprite = placeholderScrapItemSprite;
            resultSpriteRenderer.sprite = placeholderResultItemSprite;
        }
    }

    public void createWorkshopGear()
    {
        if (assignedScrap != null && checkResources())
        {
            inventoryManager.addLabItem(assignedScrap.wholeGear);
            inventoryManager.removeLabItem(assignedScrap);
            foreach (item it in assignedScrap.wholeGear.cost)
            {
                inventoryManager.removeLabItem(it);
            }
            menuManager.labItemMenu.refreshItems(new List<item>(), menuManager.labItemMenu.recipeGridSlots, itemDispType.all);
            assignScrap(null);
        }
        else Debug.Log("no offer or enough resources");
        menuManager.labItemMenu.refreshItems(inventoryManager.labInventory, menuManager.labItemMenu.regularGridSlots, itemDispType.all);

        menuManager.labItemMenu.selectedItem = null;
        menuManager.labItemMenu.showInfo();
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
