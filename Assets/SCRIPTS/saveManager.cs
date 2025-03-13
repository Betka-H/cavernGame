using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public List<item> itemsSaveList;
    public List<item> missionSaveList;
    // public int currentMission;
    // public missionSO currentMission;
    public int currentMission;
    // public missionSO deathMission;
    // public int missionCall;
    public int deathCall;
    // public int jumpCall; //! save warn bool instead
}

// chatgpt
public class saveManager : MonoBehaviour
{
    string saveKeyString = "invAndMissionSave";

    // public inventoryDefinitions inventoryDefinitions;
    menuManager menuManager;
    missionManager missionManager;

    void Awake()
    {
        menuManager = FindObjectOfType<menuManager>();
        missionManager = FindObjectOfType<missionManager>();

        //! temp pref clear
        clearPrefs();
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
        data.itemsSaveList = menuManager.inventoryManager.labInventory;
        data.missionSaveList = menuManager.inventoryManager.missionInventory;

        // missions + calls
        // data.currentMission = missionManager.allMissions[missionManager.currentMission];
        data.currentMission = missionManager.currentMission;
        // data.missionCall = missionManager.allMissions[missionManager.currentMission].currentCall;
        data.deathCall = missionManager.deathMission.currentCall;
        // data.jumpCall = missionManager.jumpMission.currentCall;

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(saveKeyString, json);

        PlayerPrefs.Save();

        Debug.LogWarning($"saved: last mission: {missionManager.currentMission}, last mission call: {missionManager.allMissions[missionManager.currentMission].currentCall}, last death mission call: {missionManager.deathMission.currentCall}");
    }

    void load()
    {
        // if (!PlayerPrefs.HasKey(saveKeyString)) return;

        string json = PlayerPrefs.GetString(saveKeyString);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        // if there is no save - crate a clear one
        if (!PlayerPrefs.HasKey(saveKeyString))
        {
            Debug.Log("no save present! creating empty save");

            // inventories
            menuManager.inventoryManager.resetInventories();
            // menuManager.inventoryManager.labInventory = new List<item>();
            // menuManager.inventoryManager.missionInventory = new List<item>();

            // missions + calls
            missionManager.restartMissions();
            // missionManager.allMissions[missionManager.currentMission] = missionManager.allMissions[0];
            // missionManager.deathMission.currentCall = 0;
            // missionManager.allMissions[missionManager.currentMission].currentCall = 0;
        }
        else
        {
            // inventories
            menuManager.inventoryManager.labInventory = data.itemsSaveList;
            menuManager.inventoryManager.missionInventory = data.missionSaveList;

            // missions + calls
            // missionManager.allMissions[missionManager.currentMission] = data.currentMission;
            missionManager.currentMission = data.currentMission;
            // missionManager.deathMission.currentCall = missionManager.allMissions[missionManager.currentMission].currentCall; //! ????????????
            // missionManager.allMissions[missionManager.currentMission].currentCall = data.missionCall;
            // missionManager.deathMission = data.deathMission;
            missionManager.deathMission.currentCall = data.deathCall;
            // missionManager.jumpMission.currentCall = data.jumpCall;
        }

        Debug.LogWarning($"loaded: last mission: {missionManager.currentMission}, call {missionManager.allMissions[missionManager.currentMission].currentCall}, death mission call: {missionManager.deathMission.currentCall}");
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
