using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public List<item> itemsSaveList;
    public List<item> missionInvSaveList;
    public List<item> rndMissionReqList;
    // public int currentMission;
    // public missionSO currentMission;
    public int currentMission;
    // public missionSO deathMission;
    public int missionCall;
    public int deathCall;
    // public bool hasWarnedAboutJumping;
    // public int jumpCall; //! save warn bool instead
}

// chatgpt
public class saveManager : MonoBehaviour
{
    string saveKeyString = "invAndMissionSave";

    // public inventoryDefinitions inventoryDefinitions;
    menuManager menuManager;
    missionManager missionManager;
    roomController roomController;

    void Awake()
    {
        menuManager = FindObjectOfType<menuManager>();
        missionManager = FindObjectOfType<missionManager>();
        roomController = FindObjectOfType<roomController>();

        //! temp pref clear
        // clearPrefs();
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
        save();
    }

    public void save()
    {
        // Debug.Log("saving lab inv");
        SaveData data = new SaveData();

        // inventories 
        data.itemsSaveList = new List<item>();
        menuManager.inventoryManager.labInventory.AddRange(menuManager.inventoryManager.equippedItems);
        Debug.Log($"merging eq into lab inv");
        data.itemsSaveList.AddRange(menuManager.inventoryManager.labInventory);
        data.missionInvSaveList = new List<item>();
        data.missionInvSaveList.AddRange(menuManager.inventoryManager.missionInventory);
        data.rndMissionReqList = new List<item>();
        data.rndMissionReqList.AddRange(missionManager.allMissions[missionManager.currentMission].requiredItems);
        /*  Debug.LogWarning($"saving cm req items: v v v");
         menuManager.inventoryManager.printInventory(data.rndMissionReqList);
         menuManager.inventoryManager.printInventory(missionManager.allMissions[missionManager.currentMission].requiredItems); */
        // data.hasWarnedAboutJumping = roomController.hasMentionedJumping;

        // missions + calls
        // data.currentMission = missionManager.allMissions[missionManager.currentMission];
        // Debug.LogWarning($"savign current mission: {missionManager.currentMission}");
        data.currentMission = missionManager.currentMission;
        data.missionCall = missionManager.allMissions[missionManager.currentMission].currentCall;
        data.deathCall = missionManager.deathMission.currentCall;
        // data.jumpCall = missionManager.jumpMission.currentCall;

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(saveKeyString, json);

        PlayerPrefs.Save();

        // Debug.LogWarning($"saved: last mission: {missionManager.currentMission} ({missionManager.allMissions[missionManager.currentMission].requiredItems.Count} req items), last mission call: {missionManager.allMissions[missionManager.currentMission].currentCall}, last death mission call: {missionManager.deathMission.currentCall}");
    }

    void load()
    {
        // if (!PlayerPrefs.HasKey(saveKeyString)) return;

        // if there is no save - crate a clear one
        if (!PlayerPrefs.HasKey(saveKeyString))
        {
            Debug.Log("no save present! creating empty save");

            // inventories
            menuManager.inventoryManager.resetInventories();
            // menuManager.inventoryManager.labInventory = new List<item>();
            // menuManager.inventoryManager.missionInventory = new List<item>();

            roomController.hasMentionedJumping = true;
            // missions + calls
            missionManager.restartMissions();
            // missionManager.allMissions[missionManager.currentMission] = missionManager.allMissions[0];
            // missionManager.deathMission.currentCall = 0;
            // missionManager.allMissions[missionManager.currentMission].currentCall = 0;
        }
        else // if has save
        {
            string json = PlayerPrefs.GetString(saveKeyString);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            // missions + calls
            // missionManager.allMissions[missionManager.currentMission] = data.currentMission;
            missionManager.currentMission = data.currentMission;
            // missionManager.deathMission.currentCall = missionManager.allMissions[missionManager.currentMission].currentCall; //! ????????????
            missionManager.allMissions[missionManager.currentMission].currentCall = data.missionCall;
            // missionManager.deathMission = data.deathMission;
            missionManager.deathMission.currentCall = data.deathCall;
            // missionManager.jumpMission.currentCall = data.jumpCall;

            // inventories
            menuManager.inventoryManager.labInventory = new List<item>();
            menuManager.inventoryManager.labInventory.AddRange(data.itemsSaveList);
            menuManager.inventoryManager.missionInventory = new List<item>();
            menuManager.inventoryManager.missionInventory.AddRange(data.missionInvSaveList);
            missionManager.allMissions[missionManager.currentMission].requiredItems = new List<item>();
            missionManager.allMissions[missionManager.currentMission].requiredItems.AddRange(data.rndMissionReqList);
            /* Debug.LogWarning($"loading cm req items: v v v");
            menuManager.inventoryManager.printInventory(data.rndMissionReqList);
            menuManager.inventoryManager.printInventory(missionManager.allMissions[missionManager.currentMission].requiredItems); */
            // roomController.hasMentionedJumping = data.hasWarnedAboutJumping;
        }

        Debug.LogWarning($"loaded: last mission: {missionManager.currentMission} ({missionManager.allMissions[missionManager.currentMission].requiredItems.Count} req items), call {missionManager.allMissions[missionManager.currentMission].currentCall}, death mission call: {missionManager.deathMission.currentCall}");
        PlayerPrefs.Save();

        // Debug.LogWarning($"loaded: last mission: {missionManager.currentMission}, call {missionManager.allMissions[missionManager.currentMission].currentCall}, death mission call: {missionManager.deathMission.currentCall}, jump mission call: {missionManager.jumpMission.currentCall}");
        // FindObjectOfType<announcerManager>().announceMessage($"loaded: last mission: {missionManager.currentMission}, call {missionManager.allMissions[missionManager.currentMission].currentCall}, death mission call: {missionManager.deathMission.currentCall}");
        //! check Debug.LogWarning("what about eq inv?");
    }
}

/* using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public List<float> itemIDs; // Stores item names/IDs
}

public class saveManager : MonoBehaviour
{
    List<item> allItems; // Reference to all ScriptableObjects
    public inventoryManager inventoryManager; // Holds current inventory
    public inventoryDefinitions inventoryDefinitions;

    void Awake()
    {
        allItems = inventoryDefinitions.allItems;
    }

    void Start()
    {
        // save();
        load();
    }

    void OnApplicationQuit()
    {
        save();
    }

    void save()
    {
        Debug.Log("saving");
        SaveData data = new SaveData();
        data.itemIDs = inventoryManager.labInventory.Select(item => item.itemId).ToList();

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("InventorySave", json);
        PlayerPrefs.Save();
    }

    void load()
    {
        Debug.Log("loading");
        if (!PlayerPrefs.HasKey("InventorySave")) return;

        string json = PlayerPrefs.GetString("InventorySave");
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        inventoryManager.labInventory = allItems.Where(item => data.itemIDs.Contains(item.itemId)).ToList();
    }
}
 */
