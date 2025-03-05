using UnityEngine;

public class roomSO : ScriptableObject
{
	public Transform roomPrefab;

	[HideInInspector]
	public int orderOnMap;

	public Color roomBgColor;
}