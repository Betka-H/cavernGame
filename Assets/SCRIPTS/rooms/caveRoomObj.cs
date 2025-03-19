using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class caveRoomObj : MonoBehaviour
{
	public Transform itemSpawnpointParent;
	public Transform traderSpawnpointParent;

	public Transform[] getLootSpawnpoints()
	{
		List<Transform> spawnPoints = itemSpawnpointParent.GetComponentsInChildren<Transform>().ToList();
		spawnPoints.Remove(itemSpawnpointParent);
		return spawnPoints.ToArray();
	}
	public Transform[] getTraderSpawnpoints()
	{
		List<Transform> spawnPoints = traderSpawnpointParent.GetComponentsInChildren<Transform>().ToList();
		spawnPoints.Remove(traderSpawnpointParent);
		return spawnPoints.ToArray();
	}
}