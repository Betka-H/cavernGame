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
        Debug.Log($"cm: {currentMission}");
    }

    public void checkMissionItems()
    {
        /* List<item> tmpList = new List<item>(allMissions[currentMission].requiredItems);
        foreach (item it in menuManager.inventoryManager.missionInventory)
        {
            if (tmpList.Contains(it))
            {

            }
        } */

        Debug.Log("checking mission items");

        Debug.Log("req items:");
        menuManager.inventoryManager.printInventory(allMissions[currentMission].requiredItems);
        Debug.Log("have items:");
        menuManager.inventoryManager.printInventory(menuManager.inventoryManager.missionInventory);
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
    }
}