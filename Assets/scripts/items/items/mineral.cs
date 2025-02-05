using UnityEngine;

public enum gemColor { red, orange, yellow, green, blue, purple, pink, clear }

[CreateAssetMenu(menuName = "itemL_Mineral")]
public class mineral : loot
{
	[HideInInspector]
	public lootKind lootKind = lootKind.mineral;

	[Header("mineral")]
	[Tooltip("red gem - ruby, orange gem - , yellow gem - , green gem - emerald, blue gem - , purple gem - amethyst, pink gem - , clear gem - diamond")]
	public gemColor gemColor;
}
