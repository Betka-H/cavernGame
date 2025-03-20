using UnityEngine;

public class roomSO : ScriptableObject
{
	public Transform roomPrefab;
	public Color roomBgColor;

	[HideInInspector] public int orderOnMap;
}

