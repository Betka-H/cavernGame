using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class caveRoomObj : MonoBehaviour
{
	public Transform itemSpawnpointParent;
	public Transform traderSpawnpointParent;

	public Transform[] getLootSpawnpoints()
	{
		List<Transform> sps = itemSpawnpointParent.GetComponentsInChildren<Transform>().ToList();
		sps.Remove(itemSpawnpointParent);
		return sps.ToArray();
	}
	public Transform[] getTraderSpawnpoints()
	{
		List<Transform> tsps = traderSpawnpointParent.GetComponentsInChildren<Transform>().ToList();
		tsps.Remove(traderSpawnpointParent);
		return tsps.ToArray();
	}
}