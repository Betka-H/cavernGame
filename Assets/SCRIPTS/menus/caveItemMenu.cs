using System.Collections.Generic;
using UnityEngine;

public class caveItemMenu : itemMenu
{
	private bool _hasAllSlots;
	public bool hasAllSlots
	{
		get { return _hasAllSlots; }
		set
		{
			// if (_hasAllSlots != value)
			{
				_hasAllSlots = value;

				//! temp slot limit values
				if (hasAllSlots)
				{
					slotLimit = 20;
					slotCover.gameObject.SetActive(false);
				}
				else
				{
					slotLimit = 12;
					slotCover.gameObject.SetActive(true);
				}
				// Debug.Log($"sl: {slotLimit}");
			}
		}
	}
	[HideInInspector] public int slotLimit;
	public Transform slotCover;

	void OnEnable()
	{
		refreshItems(regularSlots, inventoryScript.caveInventory);
	}

	public override void refreshItems(invItem[] cat, List<item> dog, List<item> rat)
	{
		// bwomp
	}
	public override void refreshItems(invItem[] slots, List<item> itemList)
	{
		inventoryScript.orderInventory(ref itemList);

		if (regularSlots != null)
		{
			for (int i = 0; i < regularSlots.Length; i++) // for each slot
			{
				caveInvItem currentItem = regularSlots[i] as caveInvItem;
				if (i < itemList.Count)
				{
					currentItem.assignItem(itemList[i]);
				}
				else currentItem.assignItem(null);
			}
		}
	}
}
