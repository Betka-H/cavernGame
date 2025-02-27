using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "room_cavern")]
public class room_cavern : roomSO
{
    [HideInInspector]
    public bool isDark;

    public List<loot> lootThatCanSpawnInThisRoom;
    private List<loot> lootThatCanSpawnInThisRoomAssortedByRarity = new List<loot>();
    private Transform[] allLootSpawnLocations;
    [HideInInspector]
    public List<Transform> chosenLootSpawnLocations;

    [HideInInspector]
    public bool hasTrader;
    private Transform[] allPossibleTraderSpawnLocationsInThisRoom;
    [HideInInspector]
    public Transform chosenTraderSpawn;

    public void setLootAndTheirSpawnLocations(int chance)
    {
        /* allSpawnLocations = roomPrefab.GetComponent<roomObj>().getItemSpawnpoints().ToArray();
		// Debug.LogWarning($"{roomPrefab.name} has {allSpawnLocations.Count()} item spawn locations");
		System.Random rndForLocations = new System.Random(); */

        // Debug.Log("-----------------------");

        allLootSpawnLocations = roomPrefab.GetComponent<caveRoomObj>().getLootSpawnpoints().ToArray();

        chosenLootSpawnLocations.Clear();

        // Debug.Log($"all spawn locs: {allItemSpawnLocations.Count()}, all items: {itemsThatCanSpawnInThisRoom.Count()}");
        if (allLootSpawnLocations.Count() > 0 && lootThatCanSpawnInThisRoom.Count() > 0)
        {
            // Debug.Log($"all spawn locs: {string.Join<Transform>(", ", allItemSpawnLocations)}");
            System.Random rnd = new System.Random();

            // if (rnd.Next(100) <= chance) // chance that any items will generate
            {
                foreach (Transform lootLocation in allLootSpawnLocations)
                {
                    if (rnd.Next(100) <= chance) // choose locations
                    {
                        chosenLootSpawnLocations.Add(lootLocation);
                        // Debug.Log($"adding {itemLocation} to loc list");
                    }
                }
                // Debug.Log($"chosen spawn locs: {string.Join<Transform>(", ", allItemSpawnLocations)}");
            }
        }

        // if (itemsThatCanSpawnInThisRoom.Count() > 0 && chosenItemSpawnLocations.Count() > 0)
        organiseLootByRarity();
        assignLoot();
    }
    void organiseLootByRarity()
    {
        // Debug.Log($"loot that can spawn: {string.Join(", ", lootThatCanSpawnInThisRoom)}");
        lootThatCanSpawnInThisRoomAssortedByRarity.Clear();
        foreach (loot loot in lootThatCanSpawnInThisRoom)
        {
            // Debug.Log($"rarity thing?????");
            for (int i = 1; i <= loot.rarity; i++)
            {
                // adds the loot as many times as it is rare
                lootThatCanSpawnInThisRoomAssortedByRarity.Add(loot);

                // Debug.Log($"rarity thing {i}");
            }
        }
    }
    [HideInInspector]
    public List<loot> lootForThisRoom;
    void assignLoot()
    {
        // Debug.Log("assigning items to room spawn locations");

        // indexedItemsForThisRoom = new item[chosenItemSpawnLocations.Count()];
        lootForThisRoom.Clear();
        // Debug.Log($"locations: {chosenItemSpawnLocations.Count()}, items: {indexedItemsForThisRoom.Count()}");

        System.Random rndForLoot = new System.Random();

        string debug = "";
        for (int i = 0; i < chosenLootSpawnLocations.Count(); i++) // for each location
        {
            if (lootThatCanSpawnInThisRoomAssortedByRarity.Count() > 0)
            {
                // indexedItemsForThisRoom[i] = itemsThatCanSpawnInThisRoom[rndForitems.Next(itemsThatCanSpawnInThisRoom.Count())];
                loot randomLoot = lootThatCanSpawnInThisRoomAssortedByRarity[rndForLoot.Next(lootThatCanSpawnInThisRoomAssortedByRarity.Count())];
                lootForThisRoom.Add(randomLoot);
                debug += $"{lootForThisRoom[i]} ({chosenLootSpawnLocations[i]}), ";
            }
            else
            {
                // lootForThisRoom[i] = null;
                debug += $"null, ";
            }
        }
        // Debug.Log($"room {name} assigned items: {debug}");
    }

    public void setTraderSpawnLocation()
    {
        allPossibleTraderSpawnLocationsInThisRoom = roomPrefab.GetComponent<caveRoomObj>().getTraderSpawnpoints().ToArray();
        if (allPossibleTraderSpawnLocationsInThisRoom.Count() > 0)
        {
            // Debug.Log($"trader spawn locations for {name}: {string.Join(", ", traderSpawnLocations.)}");
            System.Random rndForTrader = new System.Random();
            // Debug.LogWarning($"0-{traderSpawnLocations.Length}");
            // return traderSpawnLocations[rndForTrader.Next(traderSpawnLocations.Length)];
            chosenTraderSpawn = allPossibleTraderSpawnLocationsInThisRoom[rndForTrader.Next(allPossibleTraderSpawnLocationsInThisRoom.Length)];
        }
    }

}