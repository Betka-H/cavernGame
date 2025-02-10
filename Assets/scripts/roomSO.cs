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


	public void setItemSpawnLocations()
	{
		allSpawnLocations = roomPrefab.GetComponent<roomObj>().getItemSpawnpoints().ToArray();
		// Debug.LogWarning($"{roomPrefab.name} has {allSpawnLocations.Count()} item spawn locations");
		System.Random rndForLocations = new System.Random();

	}
	public Transform getTraderSpawnLocation()
	{
		if (traderSpawnLocations.Length != 0)
		{
			traderSpawnLocations = roomPrefab.GetComponent<roomObj>().getTraderSpawnpoints().ToArray();

			System.Random rndForTrader = new System.Random();
			// Debug.LogWarning($"0-{traderSpawnLocations.Length}");
			return traderSpawnLocations[rndForTrader.Next(0, traderSpawnLocations.Length - 1)];
		}
		else return null;
	}

	public void setItems()
	{
		itemsThatSpawn = new int[chosenItemSpawnLocations.Length];
	}
}