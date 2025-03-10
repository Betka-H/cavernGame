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
            Debug.Log("new mission?");
            // newMission();
            endMission();
        }
    }

    public void endMission()
    {
        //! temp
        allMissions[currentMission].missionEndEvent.Invoke();
        newMission();
    }

    void newMission()
    {
        if (currentMission + 1 < allMissions.Length)
        {
            Debug.Log("new mission!");
            //! todo check for max missions! // or idk do something. at least it does not throw errors now
            currentMission++;
        }
        else
        {
            Debug.Log("no more missions!");
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
}