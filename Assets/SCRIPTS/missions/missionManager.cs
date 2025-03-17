using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class missionManager : MonoBehaviour
{
    public missionSO[] allMissions;

    [Header("special missons")] // make sure to set call to 0
    public missionSO deathMission;
    public missionSO jumpMission;
    public missionSO traderMission;

    menuManager menuManager;
    callManager callManager;

    [HideInInspector] public int currentMission;

    void Awake()
    {
        menuManager = FindObjectOfType<menuManager>();
        callManager = FindObjectOfType<callManager>();
    }

    void Start()
    {
        // restartMissions(); //! tmp. testing space mission

        // checkRndMission();
        // Debug.Log($"current mission: {currentMission}");
        // Debug.Log($"current call: {allMissions[currentMission].currentCall}");
        //! tmp v v v
        /* Debug.LogError($"setting call to 13");
        allMissions[0].currentCall = 13; */
    }

    void Update()
    {
        // Debug.Log(currentMission);
    }

    // -1 to not check call
    public bool checkCurrentMission(int wantedMissionID, int wantedCallID)
    {
        // Debug.Log($"checking mission state as {wantedMissionID}, {wantedCallID}");
        missionSO currentMissionSO = allMissions[currentMission];

        // Debug.Log($"current mission: {currentMissionSO} ({currentMissionSO.missionID}), current call: {currentMissionSO.currentCall}");

        if (currentMissionSO.missionID == wantedMissionID && (currentMissionSO.currentCall == wantedCallID || wantedCallID == -1))
        {
            // Debug.Log($"right mission");
            return true;
        }
        else
        {
            // Debug.Log($"not the right mission");
            return false;
        }
    }

    public void checkMissionItems()
    {
        missionSO cm = allMissions[currentMission];
        if (menuManager.inventoryManager.checkResources(menuManager.inventoryManager.missionInventory, cm.requiredItems))
        {
            FindObjectOfType<announcerManager>().announceMessage($"all items collected! go to MG for a new mission");
            if (cm.missionID == -1)
            {
                // Debug.LogWarning("collected all items for tutorial mission");
                callManager.startCall(cm);
            }

            if (cm.endOnAllItems)
            {
                cm.endMission();
                newMission();
                // endMission();
            }
        }
    }

    /* public void endMission()
    {

        // runEndEvent();
        newMission();
    } */

    public void newMission()
    {
        FindObjectOfType<inventoryManager>().missionInventory.Clear();

        if (currentMission + 1 < allMissions.Length)
        {
            FindObjectOfType<announcerManager>().announceMessage("you have a new mission!");
            currentMission++;

            checkRndMission();
        }
        else
        {
            Debug.LogError($"all missions completed!");
            FindObjectOfType<announcerManager>().announceMessage("all missions completed!");
        }
        allMissions[currentMission].currentCall = 0;
    }

    public void restartMissions()
    {
        Debug.LogError($"restarting missions");
        currentMission = 0;
        foreach (missionSO mission in allMissions)
        {
            mission.currentCall = 0;
            if (mission.missionID != -1) // not for tutorial
            {
                // mission.requiredItems = new List<item>();
                // Debug.LogWarning($"clearing req list");
                mission.requiredItems.Clear();
                // Debug.LogWarning($"req list length: {mission.requiredItems.Count}");
            }
        }

        // Debug.Log($"dm: {deathMission}");
        deathMission.currentCall = 0;
        jumpMission.currentCall = 0;
        traderMission.currentCall = 0;
        Debug.Log($"restarting missions: deathm: {deathMission.currentCall}, jumpm: {jumpMission.currentCall}, traderm: {traderMission.currentCall}");

        menuManager.toggleCallScreen();
    }

    public void skipTutorial() // called by skip tutorial btn
    {
        FindObjectOfType<announcerManager>().announceMessage($"skipping the tutorial");

        missionSO tutorialMission = allMissions[0];

        callManager.endCall(false);

        restartMissions();
        currentMission = 1;

        Debug.LogError($"ending tutorial mission");
        tutorialMission.endMission();

        // FindObjectOfType<missionManager>().m_afterTutorialInv();
        FindObjectOfType<inventoryManager>().m_afterTutorialInv();

        checkRndMission();

        // PlayerPrefs.SetInt("jumpWarn", 1);
        // PlayerPrefs.Save();
    }

    public void checkRndMission()
    {
        if (allMissions[currentMission] is randomMissionSO rndMission)
        {
            Debug.Log($"random mission");
            // generate random items
            // rndMission.requiredItems.Clear();

            // if (rndMission.requiredItems.Count > 0) // fucking hell....
            if (rndMission.requiredItems.Count == 0)
            {
                Debug.LogWarning($"req items empty - generating new");

                List<item> allMissionItems = menuManager.inventoryManager.inventoryDefinitions.missionItems;
                for (int i = 0; i < rndMission.howManyItems; i++)
                {
                    int rndIndex = Random.Range(0, allMissionItems.Count);
                    item rndItem = allMissionItems[rndIndex];

                    rndMission.requiredItems.Add(rndItem);

                    // Debug.Log($"generating item {i}: {rndItem}");
                }
                menuManager.inventoryManager.sortInventory(ref rndMission.requiredItems);
                Debug.Log($"req items generated:");
                menuManager.inventoryManager.printInventory(rndMission.requiredItems);
            }
            else
            {
                Debug.LogWarning($"req items not empty - keeping:");
                menuManager.inventoryManager.printInventory(rndMission.requiredItems);
            }
        }
    }

    /* void runEndEvent()
    {
        foreach (var even in allMissions[currentMission].endEventValuesList)
        {
            // Debug.Log("found event");
            GameObject gameObj = GameObject.Find(even.objName);
            gameObj.SendMessage(even.methodName);
        }
    } */
}