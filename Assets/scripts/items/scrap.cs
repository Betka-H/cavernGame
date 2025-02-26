using UnityEngine;

/* public enum scrapKind
{
	// light = headEqKind.flashlight,
	goggles = headEqKind.NVgoggles,
	pick = handEqKind.pickaxe,
	hammer = handEqKind.sledgehammer,
	vial = handEqKind.vial,
	armor = bodyEqKind.hazmatSuit
} */

[CreateAssetMenu(menuName = "item_scrap")]
public class scrap : item
{
	// [HideInInspector]
	// public lootKind lootKind = lootKind.scrap;

	[Header("scrap")]
	//public scrapKind scrapKind;
	public gear wholeGear;
}
