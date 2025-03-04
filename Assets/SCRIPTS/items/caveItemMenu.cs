using System.Collections.Generic;
using UnityEngine;

public class caveItemMenu : itemMenu
{
	void OnEnable()
	{
		refreshItems(regularSlots, inventoryScript.caveInventory);
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
