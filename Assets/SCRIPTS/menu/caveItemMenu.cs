using System.Collections.Generic;
using UnityEngine;

public class caveItemGrid : itemMenu
{
	// for backpack item
	private bool _hasAllSlots;
	public bool hasAllSlots
	{
		get { return _hasAllSlots; }
		set
		{
			// if (_hasAllSlots != value)
			{
				_hasAllSlots = value;

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
			}
		}
	}
	[HideInInspector] public int slotLimit;
	public Transform slotCover;

	void OnEnable()
	{
		refreshItems(regularSlots, inventoryScript.caveInventory);
	}

	public override void refreshItems(invItem[] slots, List<item> hasList, List<item> neededList)
	{
		Debug.LogError($"invalid request");
	}
	public override void refreshItems(invItem[] slots, List<item> itemList)
	{
		inventoryScript.sortInventory(ref itemList);

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
