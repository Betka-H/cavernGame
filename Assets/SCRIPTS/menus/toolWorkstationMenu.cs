using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class toolWorkstationMenu : MonoBehaviour
{
    // public Transform recipeGridTransform;
    [HideInInspector] public labInvItem[] recipeGridSlots;

    public itemMenu requiredItemsMenu;

    public Transform[] recipeSpawnPoints;
    public GameObject physicalPrefab;

    public SpriteRenderer craftBtnBg;
    public Color craftBtnUnableColor;
    public Color craftBtnAbleColor;

    scrap assignedScrap;
    public SpriteRenderer scrapSpriteRenderer;
    public SpriteRenderer resultSpriteRenderer;
    menuManager menuManager;
    public Sprite placeholderScrapItemSprite;
    public Sprite placeholderResultItemSprite;

    void Awake()
    {
        menuManager = FindObjectOfType<menuManager>();
        // recipeGridSlots = recipeGridTransform.GetComponentsInChildren<labInvItem>(true);
        isDropping = false;
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
        if (!isDropping)
        {
            assignedScrap = scrap;
            if (assignedScrap != null)
            {
                scrapSpriteRenderer.sprite = assignedScrap.itemSprite;
                resultSpriteRenderer.sprite = assignedScrap.wholeGear.itemSprite;
                //??? menuManager.labItemMenu.itemGrid.refreshItems(recipeGridSlots, assignedScrap.wholeGear.cost.ToList());
                requiredItemsMenu.refreshItems(requiredItemsMenu.regularSlots, menuManager.inventoryManager.labInventory, assignedScrap.wholeGear.cost.ToList());

                if (FindObjectOfType<missionManager>().checkCurrentMission(-1, 8)) //! testing
                {
                    callManager callManager = FindObjectOfType<callManager>();
                    callManager.startCall(callManager.currentMainMission());
                    // startNextMainMissionCall();
                }

                if (menuManager.inventoryManager.checkResources(menuManager.inventoryManager.labInventory, assignedScrap.wholeGear.cost.ToList()))
                {
                    // highlight craft button
                    craftBtnBg.color = craftBtnAbleColor;
                }
            }
            else
            {
                scrapSpriteRenderer.sprite = placeholderScrapItemSprite;
                resultSpriteRenderer.sprite = placeholderResultItemSprite;
                //??? menuManager.labItemMenu.itemGrid.refreshItems(recipeGridSlots, new List<item>());
                requiredItemsMenu.refreshItems(requiredItemsMenu.regularSlots, menuManager.inventoryManager.labInventory, new List<item>());
            }
        }
    }

    public void createWorkshopGear()
    {
        if (!isDropping)
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

                    /*//? // remove all recipe resources
                    foreach (item it in assignedScrap.wholeGear.cost)
                        menuManager.inventoryManager.removeItem(it, menuManager.inventoryManager.labInventory);

                    // clear scrap and recipe displays
                    assignScrap(null);
                    menuManager.labItemMenu.itemGrid.refreshItems(menuManager.labItemMenu.itemGrid.regularSlots, menuManager.inventoryManager.labInventory);
                    //??? menuManager.labItemMenu.itemGrid.refreshItems(recipeGridSlots, null);
                    requiredItemsMenu.refreshItems(requiredItemsMenu.regularSlots, menuManager.inventoryManager.labInventory, new List<item>());

                    // menuManager.itemInfoDisplay.selectedItem = null;
                    menuManager.itemInfoDisplay.setInfo(null);

                    if (FindObjectOfType<missionManager>().checkCurrentMission(-1, 9)) //! testing
                    {
                        callManager callManager = FindObjectOfType<callManager>();
                        callManager.startCall(callManager.currentMainMission());
                        menuManager.toggleToggletoolWorkstationMenuScreen();
                        // startNextMainMissionCall();
                    } */
                }
                else
                    FindObjectOfType<announcerManager>().announceMessage($"you do not have the required materials!");
            }
            else
            {
                // Debug.Log("no offer or not enough resources");
                FindObjectOfType<announcerManager>().announceMessage($"no item selected!");
            }
        }
    }

    bool isDropping;
    IEnumerator dropRecipe(item[] recipeItems)
    {
        isDropping = true;
        int i = recipeItems.Length + 1;
        foreach (item it in recipeItems)
        {
            i--;
            yield return new WaitForSecondsRealtime(Random.Range(0.5f / i, 0.75f / i));
            // yield return new WaitForSecondsRealtime(0.15f);
            GameObject ingredient = Instantiate(physicalPrefab, recipeSpawnPoints[Random.Range(0, recipeSpawnPoints.Length)]);
            ingredient.GetComponent<SpriteRenderer>().sprite = it.itemSprite;

            menuManager.inventoryManager.removeItem(it, menuManager.inventoryManager.labInventory);
        }
        yield return new WaitForSecondsRealtime(Random.Range(0.25f, 1f));
        isDropping = false;

        // remove all recipe resources
        /* foreach (item it in assignedScrap.wholeGear.cost)
        {
            Debug.LogWarning($"removing item {it.itemName} from inv");
            menuManager.inventoryManager.removeItem(it, menuManager.inventoryManager.labInventory);
        } */

        // clear scrap and recipe displays
        assignScrap(null);
        menuManager.labItemMenu.itemGrid.refreshItems(menuManager.labItemMenu.itemGrid.regularSlots, menuManager.inventoryManager.labInventory);
        //??? menuManager.labItemMenu.itemGrid.refreshItems(recipeGridSlots, null);
        requiredItemsMenu.refreshItems(requiredItemsMenu.regularSlots, menuManager.inventoryManager.labInventory, new List<item>());

        // menuManager.itemInfoDisplay.selectedItem = null;
        menuManager.itemInfoDisplay.setInfo(null);

        if (FindObjectOfType<missionManager>().checkCurrentMission(-1, 9)) //! testing
        {
            callManager callManager = FindObjectOfType<callManager>();
            callManager.startCall(callManager.currentMainMission());
            menuManager.toggleToggletoolWorkstationMenuScreen();
            // startNextMainMissionCall();
        }
    }
}
