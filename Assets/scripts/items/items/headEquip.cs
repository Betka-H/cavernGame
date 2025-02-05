using UnityEngine;

public enum headEqKind { flashlight, NVgoggles };

[CreateAssetMenu(menuName = "itemE_HeadEq")]
public class headEquip : gear
{
	[HideInInspector]
	public equipSlot equipSlot = equipSlot.headEquip;

	[Header("head equip")]
	public headEqKind headEqKind;
}
