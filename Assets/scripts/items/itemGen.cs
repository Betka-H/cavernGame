using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class itemGen : MonoBehaviour
{
	/* public Transform spawnpointObj;
	private List<Transform> spawnpoints;
	public item[] itemPool;

	void Start() // temp. cannot be start, would run every time room appears
	{
		// spawnItems();
	}

	void getSpawnPoints()
	{
		spawnpoints = spawnpointObj.gameObject.GetComponentsInChildren<Transform>().ToList();
		spawnpoints.Remove(spawnpointObj);
	}

	public void spawnItems()
	{
		getSpawnPoints();

		System.Random rnd = new System.Random();
		int pointIndex = rnd.Next(spawnpoints.Count);
		int itemIndex = rnd.Next(itemPool.Length);

		// Debug.Log($"pi: {pointIndex}, ii: {itemIndex} ({itemPool[itemIndex].name})");

		Instantiate(itemPool[itemIndex], spawnpoints[pointIndex]);
	} */
}
