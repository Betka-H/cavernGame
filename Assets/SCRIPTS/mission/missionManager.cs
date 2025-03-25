using System.Collections.Generic;
using UnityEngine;

public class missionManager : MonoBehaviour
{
    public missionSO[] allMissions;

    [Header("special missons")] // make sure to include in mission restart
    public missionSO deathMission;
    public missionSO jumpMission;
    public missionSO traderMission;

    menuManager menuManager;
    callManager callManager;
    gameController gameController;
    inventoryManager inventoryManager;

    [HideInInspector] public int currentMission;

    void Awake()
    {
        menuManager = FindObjectOfType<menuManager>();
        callManager = FindObjectOfType<callManager>();
        gameController = FindObjectOfType<gameController>();
        inventoryManager = FindObjectOfType<inventoryManager>();

        if (currentMission > allMissions.Length)
        {
            Debug.LogError($"got invalid mission id!");
            currentMission = 0;
        }
    }

    public bool checkCurrentMission(int wantedMissionID, int wantedCallID)
    // -1 to not check call
    {
        // Debug.Log($"checking mission state as {wantedMissionID}, {wantedCallID}");
        missionSO currentMissionSO = allMissions[0];
        try
        {
            currentMissionSO = allMissions[currentMission];
        }
        catch (System.Exception) { Debug.LogWarning($"invalid check"); }

        // Debug.Log($"current mission: {currentMissionSO} ({currentMissionSO.missionID}), current call: {currentMissionSO.currentCall}");

        if (currentMissionSO.missionID == wantedMissionID && (currentMissionSO.currentCall == wantedCallID || wantedCallID == -1))
            return true;
        else return false;
    }

    public void checkMissionItems()
    {
        missionSO cm = allMissions[currentMission];
        if (inventoryManager.checkResources(inventoryManager.missionInventory, cm.requiredItems))
        {
            if (cm.missionID == -1) // if in tutorial
            {
                gameController.m_lockCavern();
                callManager.startCall(cm);
            }

            if (cm.endOnAllItems)
            {
                cm.endMission();
                newMission();
            }
        }
    }

    public void newMission()
    {
        FindObjectOfType<inventoryManager>().missionInventory.Clear();

        currentMission++;
        if (currentMission + 1 < allMissions.Length)
        // if (currentMission < allMissions.Length)
        {
            FindObjectOfType<announcerManager>().announceMessage("you have a new mission!");
            // currentMission++;
            allMissions[currentMission].currentCall = 0;

            FindObjectOfType<outsideMissionInfo>().show();
        }
        else
        {
            FindObjectOfType<announcerManager>().announceMessage("all missions completed! go talk to MG");
            // Debug.LogWarning($"cm: {currentMission}, {allMissions[currentMission].missionID}");
            // gameController.finishMissions();
        }

    }

    public void restartMissions()
    {
        // Debug.LogError($"restarting missions");
        currentMission = 0;
        foreach (missionSO mission in allMissions)
        {
            mission.currentCall = 0;
            if (mission.missionID != -1) // not for tutorial
                mission.requiredItems.Clear();
        }

        deathMission.currentCall = 0;
        jumpMission.currentCall = 0;
        traderMission.currentCall = 0;

        // menuManager.toggleCallScreen();
        menuManager.callScreen.gameObject.SetActive(false);
    }

    // called by skip tutorial btn
    public void skipTutorial()
    {
        FindObjectOfType<announcerManager>().announceMessage($"skipping the tutorial");

        missionSO tutorialMission = allMissions[0];

        callManager.endCall(false);

        restartMissions();
        currentMission = 1;

        tutorialMission.endMission();

        FindObjectOfType<inventoryManager>().m_afterTutorialInv();

        checkRndMission();
    }

    public void checkRndMission()
    {
        // Debug.LogWarning($"checking mission (cm: {currentMission}, mid: {allMissions[currentMission].missionID}) for rnd");
        if (allMissions[currentMission] is randomMissionSO rndMission)
        {
            // Debug.LogWarning($"current mission is rnd ({rndMission.howManyItems} items)");
            if (rndMission.requiredItems.Count == 0 || rndMission.requiredItems.Count > rndMission.howManyItems)
            // the mission has no items - generating new items
            {
                // Debug.LogWarning($"the missions has no or wrong req items - generating new items");
                List<item> allMissionItems = inventoryManager.inventoryDefinitions.missionItems;
                for (int i = 0; i < rndMission.howManyItems; i++)
                {
                    int rndIndex = Random.Range(0, allMissionItems.Count);
                    item rndItem = allMissionItems[rndIndex];
                    rndMission.requiredItems.Add(rndItem);
                }
                inventoryManager.sortInventory(ref rndMission.requiredItems);
            }
        }
    }

}