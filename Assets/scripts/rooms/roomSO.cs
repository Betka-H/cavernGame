using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public enum roomBiome { };

[CreateAssetMenu(menuName = "room")]
public class roomSO : ScriptableObject
{
	public GameObject roomPrefab;

	[HideInInspector]
	public bool isDark;

	private roomController roomController;

	public List<item> itemsThatCanSpawnInThisRoom;
	private Transform[] allItemSpawnLocations;
	[HideInInspector]
	public List<Transform> chosenItemSpawnLocations;

	[HideInInspector]
	public bool hasTrader;
	private Transform[] allPossibleTraderSpawnLocationsInThisRoom;
	[HideInInspector]
	public Transform chosenTraderSpawn;

	[HideInInspector]
	public int orderOnMap;

	private void Start()
	{
		// roomController = FindObjectOfType<roomController>();
	}

	public void setItemsAndTheirSpawnLocations(int chance)
	{
		/* allSpawnLocations = roomPrefab.GetComponent<roomObj>().getItemSpawnpoints().ToArray();
		// Debug.LogWarning($"{roomPrefab.name} has {allSpawnLocations.Count()} item spawn locations");
		System.Random rndForLocations = new System.Random(); */

		// Debug.Log("-----------------------");

		allItemSpawnLocations = roomPrefab.GetComponent<roomObj>().getItemSpawnpoints().ToArray();

		chosenItemSpawnLocations.Clear();

		Debug.Log($"all spawn locs: {allItemSpawnLocations.Count()}, all items: {itemsThatCanSpawnInThisRoom.Count()}");
		if (allItemSpawnLocations.Count() > 0 && itemsThatCanSpawnInThisRoom.Count() > 0)
		{
			// Debug.Log($"all spawn locs: {string.Join<Transform>(", ", allItemSpawnLocations)}");
			System.Random rnd = new System.Random();

			if (rnd.Next(100) <= chance) // chance that any items will generate
			{
				foreach (Transform itemLocation in allItemSpawnLocations)
				{
					if (rnd.Next(100) <= chance) // choose locations
					{
						chosenItemSpawnLocations.Add(itemLocation);
						// Debug.Log($"adding {itemLocation} to loc list");
					}
				}
				// Debug.Log($"chosen spawn locs: {string.Join<Transform>(", ", allItemSpawnLocations)}");
			}
		}

		// if (itemsThatCanSpawnInThisRoom.Count() > 0 && chosenItemSpawnLocations.Count() > 0)
		assignItems();
	}

	[HideInInspector]
	public List<item> itemsForThisRoom;
	void assignItems()
	{
		Debug.Log("assigning items to room spawn locations");

		// indexedItemsForThisRoom = new item[chosenItemSpawnLocations.Count()];
		itemsForThisRoom.Clear();
		// Debug.Log($"locations: {chosenItemSpawnLocations.Count()}, items: {indexedItemsForThisRoom.Count()}");

		System.Random rndForItems = new System.Random();

		string debug = "";
		for (int i = 0; i < chosenItemSpawnLocations.Count(); i++) // for each location
		{
			if (itemsThatCanSpawnInThisRoom.Count() > 0)
			{
				// indexedItemsForThisRoom[i] = itemsThatCanSpawnInThisRoom[rndForitems.Next(itemsThatCanSpawnInThisRoom.Count())];
				itemsForThisRoom.Add(itemsThatCanSpawnInThisRoom[rndForItems.Next(itemsThatCanSpawnInThisRoom.Count())]);
				debug += $"{itemsForThisRoom[i]} ({chosenItemSpawnLocations[i]}), ";
			}
			else
			{
				itemsForThisRoom[i] = null;
				debug += $"null, ";
			}
		}
		Debug.Log($"room {name} assigned items: {debug}");
	}/* 
	public void spawnItems()
	{
		//!
		Debug.LogWarning("get itemprefab elsehow");
		roomController = FindObjectOfType<roomController>();

		for (int i = 0; i < chosenItemSpawnLocations.Count(); i++)
		{
			Debug.Log($"rc itemprefab: {roomController.itemPrefab}");
			GameObject spawnedItem = Instantiate(roomController.itemPrefab, chosenItemSpawnLocations[i]);
			spawnedItem.GetComponent<worldItem>().updateItem(assignedItems[i]);
			Debug.Log($"spawned item: {spawnedItem}");
		}
	} */
	public void spawnItems()
	{

	}

	public void setTraderSpawnLocation()
	{
		allPossibleTraderSpawnLocationsInThisRoom = roomPrefab.GetComponent<roomObj>().getTraderSpawnpoints().ToArray();
		if (allPossibleTraderSpawnLocationsInThisRoom.Count() > 0)
		{
			// Debug.Log($"trader spawn locations for {name}: {string.Join(", ", traderSpawnLocations.)}");
			System.Random rndForTrader = new System.Random();
			// Debug.LogWarning($"0-{traderSpawnLocations.Length}");
			// return traderSpawnLocations[rndForTrader.Next(traderSpawnLocations.Length)];
			chosenTraderSpawn = allPossibleTraderSpawnLocationsInThisRoom[rndForTrader.Next(allPossibleTraderSpawnLocationsInThisRoom.Length)];
		}
	}

	/* public void setItems()
	{
		itemsThatSpawn = new int[chosenItemSpawnLocations.Count()];
	} */
}