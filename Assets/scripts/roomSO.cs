using System.Linq;
using UnityEngine;

public enum roomBiome { };

[CreateAssetMenu(menuName = "room")]
public class roomSO : ScriptableObject
{
	public GameObject roomPrefab;
	[HideInInspector]
	public int[] chosenItemSpawnLocations;
	private Transform[] allSpawnLocations;
	private Transform[] traderSpawnLocations;
	[HideInInspector]
	public int[] itemsThatSpawn;
	[HideInInspector]
	public bool isDark;
	[HideInInspector]
	public bool hasTrader;
	[HideInInspector]
	public Transform chosenTraderSpawn;

	[HideInInspector]
	public int orderOnMap;

	public void setItemSpawnLocations()
	{
		allSpawnLocations = roomPrefab.GetComponent<roomObj>().getItemSpawnpoints().ToArray();
		// Debug.LogWarning($"{roomPrefab.name} has {allSpawnLocations.Count()} item spawn locations");
		System.Random rndForLocations = new System.Random();

	}
	public void getTraderSpawnLocation()
	{
		traderSpawnLocations = roomPrefab.GetComponent<roomObj>().getTraderSpawnpoints().ToArray();
		if (traderSpawnLocations.Count() > 0)
		{
			// Debug.Log($"trader spawn locations for {name}: {string.Join(", ", traderSpawnLocations.)}");
			System.Random rndForTrader = new System.Random();
			// Debug.LogWarning($"0-{traderSpawnLocations.Length}");
			// return traderSpawnLocations[rndForTrader.Next(traderSpawnLocations.Length)];
			chosenTraderSpawn = traderSpawnLocations[rndForTrader.Next(traderSpawnLocations.Length)];
		}
	}

	public void setItems()
	{
		itemsThatSpawn = new int[chosenItemSpawnLocations.Length];
	}
}