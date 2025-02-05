using UnityEngine;

public enum biomatKind { soil, plant, liquid, animal }

[CreateAssetMenu(menuName = "itemL_BioMat")]
public class bioMat : loot
{
	[HideInInspector]
	public lootKind lootKind = lootKind.biomaterial;

	[Header("biomaterial")]
	public biomatKind biomatKind;
}
