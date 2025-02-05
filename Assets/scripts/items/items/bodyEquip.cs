using UnityEngine;

public enum bodyEqKind { hazmatSuit };

[CreateAssetMenu(menuName = "itemE_BodyEq")]
public class bodyEquip : gear
{
	[HideInInspector]
	public equipSlot equipSlot = equipSlot.bodyEquip;

	[Header("body equip")]
	public bodyEqKind bodyEqKind;
}
