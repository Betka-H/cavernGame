using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class toolWorkstationMenu : MonoBehaviour
{
    scrap assignedScrap;
    public SpriteRenderer scrapSpriteRenderer;
    public SpriteRenderer resultSpriteRenderer;
    inventoryManager inventoryManager;
    menuManager menuManager;
    public Sprite placeholderScrapItemSprite;
    public Sprite placeholderResultItemSprite;

    void Start()
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
            assignScrap(null);
        }
        else Debug.Log("no offer or enough resources");
        menuManager.labItemMenu.refreshItems();
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
