using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "room_cavern")]
public class room_cavern : roomSO
{
    [HideInInspector]
    public bool isDark;

    public List<loot> lootThatCanSpawnInThisRoom;
    List<Transform> chosenLootSpawnLocations;
    List<loot> chosenLoot;

    [HideInInspector]
    public bool hasTrader;
    [HideInInspector]
    public Transform chosenTraderSpawn;

    public void setLoot(int chance)
    {
        assignLootSpawnpoints(chance);
        assignLoot();
    }
    void assignLootSpawnpoints(int chance)
    {
        Transform[] allLootSpawnLocations = roomPrefab.GetComponent<caveRoomObj>().getLootSpawnpoints();
        chosenLootSpawnLocations.Clear();

        if (allLootSpawnLocations.Count() > 0 && lootThatCanSpawnInThisRoom.Count() > 0)
        {
            System.Random rnd = new System.Random();
            foreach (Transform loc in allLootSpawnLocations)
            {
                if (rnd.Next(100) <= chance)
                {
                    chosenLootSpawnLocations.Add(loc);
                }
            }
        }
    }
    void assignLoot()
    {
        chosenLoot.Clear();

        System.Random rnd = new System.Random();

        for (int i = 0; i < chosenLootSpawnLocations.Count(); i++) // for each location
        {
            if (lootByRarity().Length > 0)
            {
                loot randomLoot = lootByRarity()[rnd.Next(lootByRarity().Length)];
                chosenLoot.Add(randomLoot);
            }
        }
    }
    loot[] lootByRarity()
    {
        List<loot> lbr = new List<loot>();

        foreach (loot loot in lootThatCanSpawnInThisRoom)
        {
            for (int i = 1; i <= loot.rarity; i++)
            {
                // adds the loot as many times as it is rare
                lbr.Add(loot);
            }
        }
        return lbr.ToArray();
    }

    public void spawnItems(GameObject prefab, Transform parent)
    {
        if (chosenLootSpawnLocations.Count() > 0 && chosenLoot.Count() > 0)
        {
            for (int i = 0; i < chosenLootSpawnLocations.Count(); i++)
            {
                spawnItem(prefab, parent, i);
            }
        }
    }
    void spawnItem(GameObject prefab, Transform parent, int i)
    {
        worldItem spawnedItem = Instantiate(prefab, parent).GetComponent<worldItem>();
        spawnedItem.assignedRoomSO = this;
        spawnedItem.updateItem(chosenLoot[i]);
        spawnedItem.assignedSpawnTransform = chosenLootSpawnLocations[i];
        if (spawnedItem.checkItem() != null)
        {
            spawnedItem.gameObject.transform.localPosition = spawnedItem.assignedSpawnTransform.position;
        }
        else Destroy(spawnedItem);
    }

    public void setTraderSpawn()
    {
        Transform[] allTraderSpawnLocations = roomPrefab.GetComponent<caveRoomObj>().getTraderSpawnpoints();
        if (allTraderSpawnLocations.Count() > 0)
        {
            System.Random rnd = new System.Random();
            chosenTraderSpawn = allTraderSpawnLocations[rnd.Next(allTraderSpawnLocations.Length)];
        }
    }

    public void removeItemSpawn(loot item, Transform spawn)
    {
        chosenLoot.Remove(item);
        chosenLootSpawnLocations.Remove(spawn);
    }
}