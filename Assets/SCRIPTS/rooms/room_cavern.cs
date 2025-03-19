using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "room_cavern")]
public class room_cavern : roomSO
{
    [HideInInspector] public bool isDark;
    public bool isEntryRoom;

    public List<loot> lootThatCanSpawnInThisRoom = new List<loot>();
    List<loot> chosenLoot = new List<loot>();
    Transform[] allLootSpawnLocations;
    List<Transform> chosenLootSpawnLocations = new List<Transform>();

    [HideInInspector] public bool hasTrader;
    [HideInInspector] public Transform chosenTraderSpawn;

    public void setLoot(int chance)
    // called when generating cavern
    {
        if (!isEntryRoom && allLootSpawnLocations.Length > 0 && lootThatCanSpawnInThisRoom.Count > 0)
        // spawn nothing in entry room and if the room has no item spawnpoints or loot to spawn 
        {
            Debug.LogError($"assign room loot in inv definitions inspector");

            inventoryDefinitions inventoryDefinitions = FindObjectOfType<inventoryDefinitions>();
            allLootSpawnLocations = roomPrefab.GetComponent<caveRoomObj>().getLootSpawnpoints();

            // set loot pool to definition
            lootThatCanSpawnInThisRoom.Clear();
            foreach (item it in inventoryDefinitions.lootItems)
                lootThatCanSpawnInThisRoom.Add(it as loot);

            assignSpawnpoints(chance);
            assignLoot();
        }

        void assignSpawnpoints(int chance)
        {
            chosenLootSpawnLocations.Clear();

            foreach (Transform loc in allLootSpawnLocations)
            {
                int rnd = new System.Random().Next(100);
                if (rnd <= chance)
                    chosenLootSpawnLocations.Add(loc);
            }
        }
        void assignLoot()
        {
            chosenLoot.Clear();

            loot[] lbr = lootByRarity();
            for (int i = 0; i < chosenLootSpawnLocations.Count; i++)
            // for each spawnpoint
            {
                int rnd = new System.Random().Next(lbr.Length);
                chosenLoot.Add(lbr[rnd]);
            }
        }
        loot[] lootByRarity()
        {
            List<loot> lbr = new List<loot>();

            // adds each loot as many times as it is rare
            foreach (loot loot in lootThatCanSpawnInThisRoom)
                for (int i = 1; i <= loot.rarity; i++)
                    lbr.Add(loot);

            return lbr.ToArray();
        }
    }

    public void spawnItems(GameObject prefab, Transform parent)
    // called on room change
    {
        if (chosenLootSpawnLocations.Count > 0 && chosenLoot.Count > 0)
            for (int i = 0; i < chosenLootSpawnLocations.Count; i++)
                spawn(prefab, parent, i);

        void spawn(GameObject prefab, Transform parent, int i)
        {
            worldItem spawnedItem = Instantiate(prefab, parent).GetComponent<worldItem>();

            spawnedItem.assignedRoomSO = this;
            spawnedItem.assignedSpawnPoint = chosenLootSpawnLocations[i];
            spawnedItem.updateItem(chosenLoot[i]);

            spawnedItem.gameObject.transform.localPosition = spawnedItem.assignedSpawnPoint.position;
        }
    }

    public void setTraderSpawn()
    // called when generating cavern
    {
        Transform[] allTraderSpawnLocations = roomPrefab.GetComponent<caveRoomObj>().getTraderSpawnpoints();
        if (allTraderSpawnLocations.Length > 0)
        {
            int rnd = new System.Random().Next(allTraderSpawnLocations.Length);
            chosenTraderSpawn = allTraderSpawnLocations[rnd];
        }
    }

    public void removeItemSpawn(loot item, Transform spawn)
    // called on world item pickup
    {
        chosenLootSpawnLocations.Remove(spawn);
        chosenLoot.Remove(item);
    }
}