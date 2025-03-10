using TMPro;
using UnityEngine;

public class missionMenu : MonoBehaviour
{
    menuManager menuManager;
    public itemMenu requiredItemsMenu;

    public TMP_Text txtTitle;
    public TMP_Text txtDescription;

    missionManager missionManager;

    void Awake()
    {
        menuManager = FindObjectOfType<menuManager>();
        missionManager = FindObjectOfType<missionManager>();

        displayMission(missionManager.allMissions[missionManager.currentMission]);
    }

    void OnEnable()
    {
        // Debug.Log($"cm: {missionManager.allMissions[missionManager.currentMission]}, {missionManager.currentMission}");
        displayMission(missionManager.allMissions[missionManager.currentMission]);
    }

    public void displayMission(missionSO mission)
    {
        // Debug.Log($"new mission: {mission.missionID}, {mission.missionTitle}, {mission.missionDescription}");

        txtTitle.text = mission.missionTitle;
        txtDescription.text = mission.missionDescription;
        requiredItemsMenu.refreshItems(requiredItemsMenu.regularSlots, menuManager.inventoryManager.missionInventory);
        // requiredItemsMenu.refreshItems(requiredItemsMenu.regularSlots, missionManager.allMissions[missionManager.currentMission].requiredItems);
    }
}
