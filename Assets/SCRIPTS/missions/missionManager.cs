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

        checkRndMission();
        // Debug.Log($"current mission: {currentMission}");
        // Debug.Log($"current call: {allMissions[currentMission].currentCall}");
    }

    void Update()
    {
        // Debug.Log(currentMission);
    }

    // -1 to not check call
    public bool checkCurrentMission(int wantedMissionID, int wantedCallID)
    {
        Debug.Log($"checking mission state as {wantedMissionID}, {wantedCallID}");
        missionSO currentMissionSO = allMissions[currentMission];

        Debug.Log($"current mission: {currentMissionSO} ({currentMissionSO.missionID}), current call: {currentMissionSO.currentCall}");

        if (currentMissionSO.missionID == wantedMissionID && (currentMissionSO.currentCall == wantedCallID || wantedCallID == -1))
        {
            Debug.Log($"right mission");
            return true;
        }
        else
        {
            Debug.Log($"not the right mission");
            return false;
        }
    }

    public void checkMissionItems()
    {
        missionSO cm = allMissions[currentMission];
        if (menuManager.inventoryManager.checkResources(menuManager.inventoryManager.missionInventory, cm.requiredItems))
        {
            FindObjectOfType<announcerManager>().announceMessage($"all items collected");
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

    void newMission()
    {
        if (currentMission + 1 < allMissions.Length)
        {
            // Debug.Log("new mission!");
            FindObjectOfType<announcerManager>().announceMessage("you have a new mission!");
            currentMission++;

            checkRndMission();
        }
        else
        {
            // Debug.Log("no more missions!");
            FindObjectOfType<announcerManager>().announceMessage("there are no more missions!");
        }
        allMissions[currentMission].currentCall = 0;
    }

    public void restartMissions()
    {
        // Debug.LogError($"restarting missions");
        currentMission = 0;
        foreach (missionSO mission in allMissions)
        {
            mission.currentCall = 0;
        }

        // Debug.Log($"dm: {deathMission}");
        deathMission.currentCall = 0;
        jumpMission.currentCall = 0;
        traderMission.currentCall = 0;
        // Debug.Log($"restarting missions");

        menuManager.toggleCallScreen();
    }

    public void skipTutorial() // called by skip tutorial btn
    {
        FindObjectOfType<announcerManager>().announceMessage($"skipping the tutorial");

        callManager.endCall(false);

        missionSO tutorialMission = allMissions[0];

        restartMissions();
        currentMission = 1;

        tutorialMission.endMission();

        // PlayerPrefs.SetInt("jumpWarn", 1);
        // PlayerPrefs.Save();
    }

    void checkRndMission()
    {
        if (allMissions[currentMission] is randomMissionSO rndMission)
        {
            Debug.Log($"random mission");
            // generate random items
            rndMission.requiredItems.Clear();

            List<item> allMissionItems = menuManager.inventoryManager.inventoryDefinitions.missionItems;
            for (int i = 0; i < rndMission.howManyItems; i++)
            {
                int rndIndex = Random.Range(0, allMissionItems.Count);
                item rndItem = allMissionItems[rndIndex];

                rndMission.requiredItems.Add(rndItem);

                Debug.Log($"generating item {i}: {rndItem}");
            }
            menuManager.inventoryManager.sortInventory(ref rndMission.requiredItems);
            Debug.Log($"req items generated:");
            menuManager.inventoryManager.printInventory(rndMission.requiredItems);
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