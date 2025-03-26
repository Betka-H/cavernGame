using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class toolWorkstationMenu : MonoBehaviour
{
    [HideInInspector] public labInvItem[] recipeGridSlots;

    [Header("item animation")]
    public Transform[] recipeSpawnPoints;
    public GameObject physicalPrefab;

    [Header("craft button")]
    public SpriteRenderer craftBtnBg;
    public Color craftBtnUnableColor;
    public Color craftBtnAbleColor;

    [Header("displays")]
    public itemMenu requiredItemsGrid;
    scrap assignedScrap;
    public SpriteRenderer scrapInputSpriteRenderer;
    public SpriteRenderer resultSpriteRenderer;
    public Sprite placeholderScrapItemSprite;
    public Sprite placeholderResultItemSprite;

    public menuManager menuManager;
    public callManager callManager;
    public announcerManager announcerManager;

    void Awake()
    {
        isAnimatingRecipeDrop = false;
    }

    void OnEnable()
    {
        menuManager.itemInfoDisplay.setInfo(null);
        assignScrap(null);
        craftBtnBg.color = craftBtnUnableColor;
    }

    public void assignScrap(scrap scrap)
    {
        craftBtnBg.color = craftBtnUnableColor;
        if (!isAnimatingRecipeDrop)
        {
            assignedScrap = scrap;
            if (assignedScrap != null)
            {
                scrapInputSpriteRenderer.sprite = assignedScrap.itemSprite;
                resultSpriteRenderer.sprite = assignedScrap.wholeGear.itemSprite;

                requiredItemsGrid.refreshItems(requiredItemsGrid.regularSlots, menuManager.inventoryManager.labInventory, assignedScrap.wholeGear.cost.ToList());

                if (FindObjectOfType<missionManager>().checkCurrentMission(-1, 8))
                // for tutorial
                {
                    callManager.startCall(callManager.currentMainMission());
                }

                if (menuManager.inventoryManager.checkResources(menuManager.inventoryManager.labInventory, assignedScrap.wholeGear.cost.ToList()))
                {
                    // highlight craft button
                    craftBtnBg.color = craftBtnAbleColor;
                }
            }
            else
            {
                scrapInputSpriteRenderer.sprite = placeholderScrapItemSprite;
                resultSpriteRenderer.sprite = placeholderResultItemSprite;
                requiredItemsGrid.refreshItems(requiredItemsGrid.regularSlots, menuManager.inventoryManager.labInventory, new List<item>());
            }
            // FindObjectOfType<spriteSizeController>().setFixedSize(scrapInputSpriteRenderer, 100); // chatgpt
            // FindObjectOfType<spriteSizeController>().setFixedSize(resultSpriteRenderer, 100); // chatgpt
        }
    }

    public void createWorkshopGear()
    {
        if (!isAnimatingRecipeDrop)
        {
            if (assignedScrap != null)
            {
                if (menuManager.inventoryManager.checkResources(menuManager.inventoryManager.labInventory, assignedScrap.wholeGear.cost.ToList()))
                {
                    // add crafted item
                    menuManager.inventoryManager.addItem(assignedScrap.wholeGear, menuManager.inventoryManager.labInventory);
                    // remove scrap item
                    menuManager.inventoryManager.removeItem(assignedScrap, menuManager.inventoryManager.labInventory);

                    StartCoroutine(dropRecipe(assignedScrap.wholeGear.cost));
                }
                else announcerManager.announceMessage($"you do not have the required materials!");
            }
            else announcerManager.announceMessage($"no item selected!");
        }
    }
    bool isAnimatingRecipeDrop; // prevents actions during crafting animation
    IEnumerator dropRecipe(item[] recipeItems)
    {
        // animate recipe items dropping
        isAnimatingRecipeDrop = true;
        int i = recipeItems.Length + 1;
        foreach (item it in recipeItems)
        {
            i--;
            yield return new WaitForSecondsRealtime(Random.Range(0.5f / i, 0.75f / i)); // drop intervals get longer the less items there are
            GameObject ingredient = Instantiate(physicalPrefab, recipeSpawnPoints[Random.Range(0, recipeSpawnPoints.Length)]);
            ingredient.GetComponent<SpriteRenderer>().sprite = it.itemSprite;
            // FindObjectOfType<spriteSizeController>().setFixedSize(ingredient.GetComponent<SpriteRenderer>(), 100); // chatgpt

            menuManager.inventoryManager.removeItem(it, menuManager.inventoryManager.labInventory);
        }
        yield return new WaitForSecondsRealtime(Random.Range(0.25f, 1f));
        isAnimatingRecipeDrop = false;

        // clear scrap and recipe displays
        assignScrap(null);
        menuManager.labItemGrid.itemGrid.refreshItems(menuManager.labItemGrid.itemGrid.regularSlots, menuManager.inventoryManager.labInventory);
        requiredItemsGrid.refreshItems(requiredItemsGrid.regularSlots, menuManager.inventoryManager.labInventory, new List<item>());

        menuManager.itemInfoDisplay.setInfo(null);

        if (FindObjectOfType<missionManager>().checkCurrentMission(-1, 9))
        // for tutorial
        {
            callManager.startCall(callManager.currentMainMission());
            menuManager.toggleToolWorkstationMenuScreen();
        }
    }
}
