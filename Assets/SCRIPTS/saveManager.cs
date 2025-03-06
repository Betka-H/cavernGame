using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public List<item> itemsSaveList;
    public int currentMission;
}

// chatgpt
public class saveManager : MonoBehaviour
{
    string saveKeyString = "invAndMissionSave";

    public inventoryDefinitions inventoryDefinitions;
    menuManager menuManager;
    missionManager missionManager;

    void Awake()
    {
        menuManager = FindObjectOfType<menuManager>();
        missionManager = FindObjectOfType<missionManager>();

        // PlayerPrefs.DeleteAll();
        // PlayerPrefs.Save();
        load();
    }

    void OnApplicationQuit()
    {
        save();
    }

    void save()
    {
        Debug.Log("saving lab inv");
        SaveData data = new SaveData();

        data.itemsSaveList = menuManager.inventoryManager.labInventory;
        data.currentMission = missionManager.currentMission;

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(saveKeyString, json);


        PlayerPrefs.Save();
    }

    void load()
    {
        Debug.Log("loading lab inv");
        if (!PlayerPrefs.HasKey(saveKeyString)) return;

        string json = PlayerPrefs.GetString(saveKeyString);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        menuManager.inventoryManager.labInventory = data.itemsSaveList;
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
