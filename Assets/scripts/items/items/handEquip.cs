using UnityEngine;

public enum handEqKind { pickaxe, sledgehammer, vial };

[CreateAssetMenu(menuName = "itemE_HandEq")]
public class handEquip : gear
{
	[HideInInspector]
	public equipSlot equipSlot = equipSlot.handEquip;

	[Header("hand equip")]
	public handEqKind handEqKind;
}
