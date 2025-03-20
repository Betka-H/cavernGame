using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public List<item> itemsSaveList;
    public List<item> missionInvSaveList;
    public List<item> missionReqList; //* unnecessary with non-random missions

    public int currentMainMission;
    public int mainMissionCall;
    public int deathCall;
}

// chatgpt
public class saveManager : MonoBehaviour
{
    string saveKeyString = "mainSave";

    menuManager menuManager;
    missionManager missionManager;
    roomController roomController;

    void Awake()
    {
        menuManager = FindObjectOfType<menuManager>();
        missionManager = FindObjectOfType<missionManager>();
        roomController = FindObjectOfType<roomController>();

        load();
    }

    void clearPrefs()
    {
        Debug.Log($"clearing save");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    void OnApplicationQuit()
    {
        //* dont save on quit
        save();
    }

    public void save()
    {
        SaveData data = new SaveData();

        saveInventories();
        saveMissions();

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(saveKeyString, json);
        PlayerPrefs.Save();

        void saveInventories()
        {
            // lab inventory
            data.itemsSaveList = new List<item>(menuManager.inventoryManager.labInventory);
            // add equipped items to it (effectively unequips them)
            data.itemsSaveList.AddRange(menuManager.inventoryManager.equippedItems);

            // mission inventory
            data.missionInvSaveList = new List<item>(menuManager.inventoryManager.missionInventory);

            // current mission required items
            data.missionReqList = new List<item>(missionManager.allMissions[missionManager.currentMission].requiredItems);
        }
        void saveMissions()
        {
            // current main mission id
            data.currentMainMission = missionManager.currentMission;

            // current main mission call
            data.mainMissionCall = missionManager.allMissions[missionManager.currentMission].currentCall;

            // current death call
            data.deathCall = missionManager.deathMission.currentCall;
        }
    }

    void load()
    {
        if (PlayerPrefs.HasKey(saveKeyString))
        {
            string json = PlayerPrefs.GetString(saveKeyString);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            roomController.hasMentionedJumping = true; //* why?

            loadInventories();
            loadMissions();

            void loadInventories()
            {
                // lab inventory
                menuManager.inventoryManager.labInventory = new List<item>(data.itemsSaveList);

                // mission inventory
                menuManager.inventoryManager.missionInventory = new List<item>();
                menuManager.inventoryManager.missionInventory.AddRange(data.missionInvSaveList);

                // current mission required items
                missionManager.allMissions[missionManager.currentMission].requiredItems = new List<item>(data.missionReqList);
            }
            void loadMissions()
            {
                // current main mission id
                missionManager.currentMission = data.currentMainMission;

                // current main mission call
                missionManager.allMissions[missionManager.currentMission].currentCall = data.mainMissionCall;

                // current death call
                missionManager.deathMission.currentCall = data.deathCall;
            }
        }
        else
        {
            Debug.Log("no save present! creating empty save");

            menuManager.inventoryManager.resetInventories();
            missionManager.restartMissions();
        }

        PlayerPrefs.Save();
    }
}
