using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class roomObj : MonoBehaviour
{
	public Transform itemSpawnpointParent;
	public Transform traderSpawnpointParent;

	public List<Transform> getItemSpawnpoints()
	{
		List<Transform> sps = itemSpawnpointParent.GetComponentsInChildren<Transform>().ToList();
		sps.Remove(itemSpawnpointParent);
		return sps;
	}
	public List<Transform> getTraderSpawnpoints()
	{
		List<Transform> tsps = traderSpawnpointParent.GetComponentsInChildren<Transform>().ToList();
		tsps.Remove(traderSpawnpointParent);
		return tsps;
	}
}