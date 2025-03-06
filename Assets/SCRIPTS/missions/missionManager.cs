using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class missionManager : MonoBehaviour
{
    public missionSO[] allMissions;
    menuManager menuManager;

    [HideInInspector] public int currentMission = 0;

    void Awake()
    {
        menuManager = FindObjectOfType<menuManager>();
    }

    void Start()
    {
        Debug.Log($"current mission: {currentMission}");
    }

    public void checkMissionItems()
    {
        if (menuManager.inventoryManager.checkResources(menuManager.inventoryManager.missionInventory, allMissions[currentMission].requiredItems))
        {
            Debug.Log("new mission?");
            newMission();
        }
    }

    void newMission()
    {
        Debug.Log("new mission!");
        currentMission++;
        allMissions[currentMission].currentCall = 0;
    }

    public void restartMissions()
    {
        currentMission = 0;
        foreach (missionSO mission in allMissions)
            mission.currentCall = 0;
    }
}