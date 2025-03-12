using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class missionManager : MonoBehaviour
{
    public missionSO[] allMissions;
    public missionSO deathMission;
    menuManager menuManager;

    [HideInInspector] public int currentMission;

    void Awake()
    {
        menuManager = FindObjectOfType<menuManager>();
    }

    void Start()
    {
        checkRndMission();
        // Debug.Log($"current mission: {currentMission}");
        // Debug.Log($"current call: {allMissions[currentMission].currentCall}");
    }

    void Update()
    {
        // Debug.Log(currentMission);
    }

    public void checkMissionItems()
    {
        if (menuManager.inventoryManager.checkResources(menuManager.inventoryManager.missionInventory, allMissions[currentMission].requiredItems))
        {
            // Debug.Log("new mission?");
            // newMission();
            endMission();
        }
    }

    public void endMission()
    {
        FindObjectOfType<announcerManager>().announceMessage($"invoke mission end", true);
        // allMissions[currentMission].missionEndEvent.Invoke();
        newMission();
    }

    void newMission()
    {
        if (currentMission + 1 < allMissions.Length)
        {
            // Debug.Log("new mission!");
            FindObjectOfType<announcerManager>().announceMessage("you have a new mission!");
            //! todo check for max missions! // or idk do something. at least it does not throw errors now
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
}