using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public enum roomBiome { };

// [CreateAssetMenu(menuName = "room")]
public class roomSO : ScriptableObject
{
	public GameObject roomPrefab;

	[HideInInspector]
	public int orderOnMap;

	// public gameController.level roomLevel;
}