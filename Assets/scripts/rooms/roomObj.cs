using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class roomObj : MonoBehaviour
{
	public Transform itemSpawnpointParent;
	public Transform traderSpawnpointParent;

	/* [HideInInspector]
	public int orderOnMap; */

	public List<Transform> getLootSpawnpoints()
	{
		List<Transform> sps = itemSpawnpointParent.GetComponentsInChildren<Transform>().ToList();
		sps.Remove(itemSpawnpointParent);
		return sps;
	}
	public List<Transform> getTraderSpawnpoints()
	{
		List<Transform> tsps = traderSpawnpointParent.GetComponentsInChildren<Transform>().ToList();
		tsps.Remove(traderSpawnpointParent);
		// Debug.Log($"trader spawn locations for {name}: {string.Join(", ", tsps)}");
		return tsps;
	}
}